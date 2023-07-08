using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EBoardDrawPoint : BasicPointAndClickInteractable
{
    [SerializeField] private EBoardDrawer boardDrawer;
    public override void OnHover(bool isTouching)
    {
        base.OnHover(isTouching);

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
        boardDrawer.EndLine();
    }
}
