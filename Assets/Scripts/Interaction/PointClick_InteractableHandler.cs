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
    [SerializeField] private Transform grabTrans;
    [SerializeField] private SpriteRenderer monitorEmission;
    [SerializeField] private bool canTouch = true;
[Header("Audio")]
    [SerializeField] private AudioSource m_audio;
    [SerializeField] private AudioClip swichClip;
[Header("Cursor")]
    [SerializeField] private CursorLockMode cursorLockMode = CursorLockMode.Confined;
    [SerializeField, ShowOnly] private int interactionLock = 0;
    private Camera playerCam;
    private HandMoving handMoving;
    private BasicPointAndClickInteractable hoveringInteractable;
    private BasicPointAndClickInteractable holdingInteractable;
    private IEnumerator coroutineMonitor;
    // public static Vector2 MouseScrPos{get; private set;}
    public static Vector3 tipPos;
    public static Vector3 grabPos;
    private bool isTouching = false;
    public bool IsTouching{get{return isTouching;}}
    void Awake(){
        playerCam = Camera.main;
        // MouseScrPos = new Vector2(Screen.width, Screen.height);
        handMoving = GetComponent<HandMoving>();
        Cursor.lockState = cursorLockMode;
        Cursor.visible = false;
        tipPos = tipTrans.position;
        grabPos= grabTrans.position;
    }
    void Update(){
        tipPos = tipTrans.position;
        grabPos= grabTrans.position;
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
    public void AllowTouch(){canTouch = true;}
    void OnInteract(InputValue value){
        if(interactionLock>0){
            Debug.LogAssertion("InteractableHandler is being locked!!!!");
            return;
        }
        Cursor.lockState = cursorLockMode;
        Cursor.visible = false;
        if(!canTouch) return;

        m_audio.PlayOneShot(swichClip);

        if(value.isPressed){
            m_audio.Play();

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
            m_audio.Stop();

            hand_state.SwitchHandState("idle");
            electrictParticle_obj.SetActive(false);
            isTouching = false;
            if(coroutineMonitor!=null) StopCoroutine(coroutineMonitor);
            coroutineMonitor = coroutineFadeMonitor();
            StartCoroutine(coroutineMonitor);

            if(holdingInteractable != null){
                holdingInteractable.OnRelease(this);
                holdingInteractable = null;
            }
        }
    }
    public void GrabOnToPoint(Vector3 center, float grabRadius){
        hand_state.SwitchHandState("grab");
        handMoving.EnterGrabMove(center, grabRadius);
    }
    public void ReleaseGrab(){
        handMoving.EnterFreeMove();
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