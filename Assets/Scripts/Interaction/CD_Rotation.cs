using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CD_Rotation : BasicPointAndClickInteractable
{
    [SerializeField, ShowOnly] private CD_STATE cd_state = CD_STATE.IDLE;
    private bool isControlling = false;
    private Vector3 grabPoint;
    void Update(){
        switch(cd_state){
            case CD_STATE.IDLE:
                if(isControlling){
                    Vector3 grabPointDir = transform.TransformPoint(grabPoint) - transform.position;
                    grabPointDir.z = 0;
                    Vector3 handtipDir = PointClick_InteractableHandler.tipPos - transform.position;
                    handtipDir.z = 0;

                    float angle = Vector3.SignedAngle(grabPointDir, handtipDir, transform.forward);
                    transform.rotation *= Quaternion.Euler(0,0,angle);
                }
                break;
            case CD_STATE.ROTATING:
                break;
        }
    }
    public override void OnHover(bool isTouching, PointClick_InteractableHandler interactableHandler)
    {
        base.OnHover(isTouching, interactableHandler);
        if(isTouching){
            interactableHandler.HoldTheInteractable(this);
        }
    }
    public override void OnClick(PointClick_InteractableHandler interactableHandler)
    {
        base.OnClick(interactableHandler);
        isControlling = true;
        interactableHandler.HoldTheInteractable(this);
        grabPoint = transform.InverseTransformPoint(PointClick_InteractableHandler.tipPos);
    }
    public override void OnRelease(PointClick_InteractableHandler interactableHandler)
    {
        base.OnRelease(interactableHandler);
        isControlling = false;
    }
    void OnDrawGizmosSelected(){
        Gizmos.matrix = transform.localToWorldMatrix;
        Gizmos.color  = Color.red;
        Gizmos.DrawSphere(grabPoint, 0.2f);
    }
}
