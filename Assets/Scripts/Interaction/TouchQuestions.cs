using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class TouchQuestions : BasicPointAndClickInteractable
{
    [SerializeField] private TextMeshPro tm;
    [SerializeField] private string[] content;
    [SerializeField] private int[] TyrTime;
    private int TouchTrying;
    private int index = 0;
    void TeleportToAPos(){
        Vector3 pos = transform.position;
        pos.x = Random.Range(-3.8f,3.8f);
        pos.y = Random.Range(-3.4f,3.4f);
        transform.position = pos;
        TouchTrying ++;
        if(TouchTrying>=TyrTime[index]){
            TouchTrying = 0;
            index ++;
            tm.text = content[index];
            if(index>=TyrTime.Length){
                DisableHitbox();
                transform.position = Vector3.up * 4f;
                tm.fontSize = 11.5f;
                EventHandler.Call_OnQuestionAsked();
                Destroy(this);
            }
        }
    }
    public override void OnClick(PointClick_InteractableHandler interactableHandler)
    {
        base.OnClick(interactableHandler);
        TeleportToAPos();
    }
    public override void OnHover(bool isTouching, PointClick_InteractableHandler pointclick_handler)
    {
        base.OnHover(isTouching, pointclick_handler);
        if(isTouching){
            TeleportToAPos();
        }
    }
}
