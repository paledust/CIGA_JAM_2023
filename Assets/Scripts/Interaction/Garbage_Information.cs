using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Garbage_Information : BasicPointAndClickInteractable
{
    [SerializeField] private string garbage_info;
    public override void OnHover(bool isTouching, PointClick_InteractableHandler pointclick_handler)
    {
        base.OnHover(isTouching, pointclick_handler);
        if(isTouching){
            EventHandler.Call_OnFeelWords(garbage_info);
        }
    }
    public override void OnClick(PointClick_InteractableHandler interactableHandler)
    {
        base.OnClick(interactableHandler);
        Debug.Log("???");
        EventHandler.Call_OnFeelWords(garbage_info);
    }
}
