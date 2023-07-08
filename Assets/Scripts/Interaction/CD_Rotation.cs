using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CD_Rotation : BasicPointAndClickInteractable
{
    [SerializeField, ShowOnly] private CD_STATE cd_state = CD_STATE.IDLE;
    [SerializeField] private float state_transitionTime = 0.25f;
    [SerializeField] private float state_transitionSpeed = 5f;
    private bool isControlling = false;
    private float angluarSpeed = 0;
    private float transitionTimer = 0;
    private Vector3 grabPoint;
    void Update(){
        switch(cd_state){
            case CD_STATE.IDLE:
                if(isControlling){
                    transitionTimer = 0;
                    Vector3 grabPointDir = transform.TransformPoint(grabPoint) - transform.position;
                    grabPointDir.z = 0;
                    Vector3 handtipDir = PointClick_InteractableHandler.tipPos - transform.position;
                    handtipDir.z = 0;

                    float angle = Vector3.SignedAngle(grabPointDir, handtipDir, transform.forward);
                    angluarSpeed = angle/Time.deltaTime;
                    transform.rotation *= Quaternion.Euler(0,0,angle);
                }
                else{
                    transform.rotation *= Quaternion.Euler(0,0,angluarSpeed*Time.deltaTime);

                    if(Mathf.Abs(angluarSpeed)>state_transitionSpeed) {
                        transitionTimer += Time.deltaTime;
                        if(transitionTimer>state_transitionTime){
                            Debug.Log(angluarSpeed);
                            cd_state = CD_STATE.ROTATING;
                        }
                    }
                    else{
                        transitionTimer = 0;
                    }
                }
                break;
            case CD_STATE.ROTATING:
                transform.rotation *= Quaternion.Euler(0,0,angluarSpeed*Time.deltaTime);
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
}
