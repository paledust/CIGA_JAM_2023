using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnswerSelect : BasicPointAndClickInteractable
{
    private float selectTime = 3;
    private Vector3 initialPosition;
    private bool selected = false;
    private float timer;
    private Vector3 normalScale;
    private Vector3 center;
    public void Initiate(){
        initialPosition = transform.position;
        normalScale = transform.localScale;
        Vector3 pos = transform.position;
        pos.y = 6.8f;
        transform.position = pos;

        gameObject.SetActive(false);
    }
    public void MoveDown(){
        gameObject.SetActive(true);
        StartCoroutine(CoroutineLandOnPosition());
    }
    public override void OnHover(bool isTouching, PointClick_InteractableHandler pointclick_handler)
    {
        base.OnHover(isTouching, pointclick_handler);
        if(isTouching){
            selected = true;
        }
    }
    public override void OnClick(PointClick_InteractableHandler interactableHandler)
    {
        base.OnClick(interactableHandler);
        interactableHandler.HoldTheInteractable(this);
        selected = true;
    }
    public override void OnExitHover()
    {
        base.OnExitHover();
        selected = false;
    }
    public override void OnRelease(PointClick_InteractableHandler interactableHandler)
    {
        base.OnRelease(interactableHandler);
        selected = false;
    }
    void Update(){
        if(selected){
            if(timer<selectTime){
                timer += Time.deltaTime;
            }
        }
        else{
            if(timer>0){
                timer -= Time.deltaTime*4;
            }
        }

        transform.localScale = Vector3.Lerp(normalScale, normalScale*5, EasingFunc.Easing.QuadEaseOut(timer/selectTime));
        transform.position   = Vector3.Lerp(initialPosition, initialPosition + (center-initialPosition).normalized*0.5f, EasingFunc.Easing.QuadEaseOut(timer/selectTime));

        if(Mathf.Abs(timer-selectTime)<=0.01f){
            timer = selectTime;
            EventHandler.Call_OnSelectAnswer(this);
            this.enabled = false;
        }
    }
    IEnumerator CoroutineLandOnPosition(){
        Vector3 initPos = transform.position;
        for(float t=0; t<1; t+=Time.deltaTime*0.5f){
            transform.position = Vector3.Lerp(initPos, initialPosition, EasingFunc.Easing.QuadEaseOut(t));
            yield return null;
        }
        transform.position = initialPosition;
        yield return null;
    }
}
