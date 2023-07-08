using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[AddComponentMenu("Player/PointClick_InteractableHandler")]
public class PointClick_InteractableHandler : MonoBehaviour
{
    [SerializeField] private GameObject electrictParticle_obj;
    [SerializeField] private ParticleSystem booomParticle;
    [SerializeField] private Hand_State hand_state;
    [SerializeField] private Transform tipTrans;
    [SerializeField] private SpriteRenderer monitorEmission;
[Header("Cursor")]
    [SerializeField] private Texture2D interactCursorUI;
    [SerializeField] private CursorLockMode cursorLockMode = CursorLockMode.Confined;
    [SerializeField, ShowOnly] private int interactionLock = 0;
    private Camera playerCam;
    private BasicPointAndClickInteractable hoveringInteractable;
    private BasicPointAndClickInteractable holdingInteractable;
    private IEnumerator coroutineMonitor;
    // public static Vector2 MouseScrPos{get; private set;}
    public static Vector3 tipPos;
    private bool isTouching = false;
    void Awake(){
        playerCam = Camera.main;
        // MouseScrPos = new Vector2(Screen.width, Screen.height);
        Cursor.lockState = cursorLockMode;
        tipPos = tipTrans.position;
    }
    void Update(){
        tipPos = tipTrans.position;
        // MouseScrPos = Mouse.current.position.ReadValue();
        if(interactionLock > 0) return;
        Ray ray = playerCam.ScreenPointToRay(playerCam.WorldToScreenPoint(tipTrans.position));
        Debug.DrawRay(ray.origin, ray.direction, Color.green);
        if(Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, Service.interactableLayer)){
            BasicPointAndClickInteractable hit_Interactable = hit.collider.GetComponent<BasicPointAndClickInteractable>();
            if(hit_Interactable!=null){
                if(hoveringInteractable != hit_Interactable) {
                    if(hoveringInteractable!=null) hoveringInteractable.OnExitHover();
                    hoveringInteractable = hit_Interactable;
                    Cursor.SetCursor(interactCursorUI, Vector2.right*32f, CursorMode.Auto);
                    hoveringInteractable.OnHover(isTouching, this);
                    if(isTouching){
                        booomParticle.transform.position = tipTrans.position;
                        booomParticle.Play();
                    }
                }
            }
            else{
                ClearCurrentInteractable();
            }
        }
        else{
            ClearCurrentInteractable();
        }
    }
    void ClearCurrentInteractable(){
        if(hoveringInteractable != null){
            hoveringInteractable.OnExitHover();
            hoveringInteractable = null;
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        }
    }
    public void LockInteracting(){
        interactionLock ++;
        if(hoveringInteractable!=null){
            hoveringInteractable.OnExitHover();
            hoveringInteractable = null;
        }
    }
    public void UnlockInteracting()=>interactionLock --;
    public void HoldTheInteractable(BasicPointAndClickInteractable interactable)=>holdingInteractable = interactable;
    void OnInteract(InputValue value){
        if(interactionLock>0){
            Debug.LogAssertion("InteractableHandler is being locked!!!!");
            return;
        }

        if(value.isPressed){
            hand_state.SwitchHandState("point");
            electrictParticle_obj.SetActive(true);
            isTouching = true;
            if(coroutineMonitor!=null) StopCoroutine(coroutineMonitor);
            coroutineMonitor = coroutineBlinkMonitor();
            StartCoroutine(coroutineMonitor);

            if(hoveringInteractable == null) return;
            if(holdingInteractable != null) return;
            hoveringInteractable.OnClick(this);
        }
        else{
            hand_state.SwitchHandState("idle");
            electrictParticle_obj.SetActive(false);
            isTouching = false;
            if(coroutineMonitor!=null) StopCoroutine(coroutineMonitor);
            coroutineMonitor = coroutineFadeMonitor();
            StartCoroutine(coroutineMonitor);

            if(holdingInteractable != null){
                holdingInteractable.OnRelease();
                holdingInteractable = null;
            }
        }
    }
    IEnumerator coroutineBlinkMonitor(){
        Color initColor = monitorEmission.color;
        Color targetColor = initColor;
        targetColor.a = 0.25f;
        for(float t=0; t<1; t+=Time.deltaTime*4){
            monitorEmission.color = Color.Lerp(initColor, targetColor, EasingFunc.Easing.BounceEaseIn(t));
            yield return null;
        }
        monitorEmission.color = targetColor;
    }
    IEnumerator coroutineFadeMonitor(){
        Color initColor = monitorEmission.color;
        Color targetColor = initColor;
        targetColor.a = 0f;
        for(float t=0; t<1; t+=Time.deltaTime*4){
            monitorEmission.color = Color.Lerp(initColor, targetColor, EasingFunc.Easing.QuadEaseOut(t));
            yield return null;
        }
        monitorEmission.color = targetColor;
    }
}