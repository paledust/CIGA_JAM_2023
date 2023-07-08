using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EBoardDrawPoint : BasicPointAndClickInteractable
{
    [SerializeField] private EBoardDrawer boardDrawer;
    public override void OnHover(bool isTouching, PointClick_InteractableHandler pointclick_handler)
    {
        base.OnHover(isTouching, pointclick_handler);
        if(isTouching && !boardDrawer.IsDrawing){
            boardDrawer.StartLine(this);
            pointclick_handler.HoldTheInteractable(this);
        }
    }
    public override void OnClick(PointClick_InteractableHandler interactableHandler)
    {
        base.OnClick(interactableHandler);
        boardDrawer.StartLine(this);
        interactableHandler.HoldTheInteractable(this);
    }
    public override void OnRelease()
    {
        base.OnRelease();
        if(boardDrawer.IsDrawing) boardDrawer.BreakLine();
    }
}
