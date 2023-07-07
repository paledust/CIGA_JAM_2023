using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[AddComponentMenu("Player/PointClick_InteractableHandler")]
public class PointClick_InteractableHandler : MonoBehaviour
{
[Header("Cursor")]
    [SerializeField] private Texture2D interactCursorUI;
    [SerializeField, ShowOnly] private int interactionLock = 0;
    private Camera playerCam;
    private BasicPointAndClickInteractable hoveringInteractable;
    private BasicPointAndClickInteractable holdingInteractable;
    public static Vector2 MouseScrPos{get{return Mouse.current.position.ReadValue();}}
    void Awake(){
        playerCam = Camera.main;
    }
    void Update(){
        if(interactionLock > 0) return;
        Ray ray = playerCam.ScreenPointToRay(MouseScrPos);
        if(Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, Service.interactableLayer)){
            BasicPointAndClickInteractable hit_Interactable = hit.collider.GetComponent<BasicPointAndClickInteractable>();
            if(hit_Interactable!=null){
                if(hoveringInteractable != hit_Interactable) {
                    if(hoveringInteractable!=null) hoveringInteractable.OnExitHover();
                    hoveringInteractable = hit_Interactable;
                    Cursor.SetCursor(interactCursorUI, Vector2.right*32f, CursorMode.Auto);
                    hoveringInteractable.OnHover();
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
            if(hoveringInteractable == null) return;
            if(holdingInteractable != null) return;
            hoveringInteractable.OnClick(this);
        }
        else{
            if(holdingInteractable != null){
                holdingInteractable.OnRelease();
                holdingInteractable = null;
            }
        }
    }
}