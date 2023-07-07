using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicPointAndClickInteractable : MonoBehaviour
{
    public INTERACTION_TYPE interaction_Type;
    [SerializeField] private Collider hitbox;
    public virtual void OnHover(){}
    public virtual void OnExitHover(){}
    public virtual void OnClick(PointClick_InteractableHandler interactableHandler){}
    public virtual void OnRelease(){}
    public void DisableHitbox(){hitbox.enabled = false;}
    public void EnableHitbox(){hitbox.enabled = true;}
}
