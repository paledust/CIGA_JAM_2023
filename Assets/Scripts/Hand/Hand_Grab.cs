using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand_Grab : MonoBehaviour
{
    [SerializeField] private Hand_State handState;
    [SerializeField] private HandMoving handMoving;
    [SerializeField] private GameObject thumb_obj;
    [SerializeField] private Transform handTrans;

    [SerializeField] private Animation m_anime;
    [SerializeField] private GameObject monitor_obj; 
    private const string throw_anime_name = "Throw";
    public void ThrowItem(Transform thrownItem){
        StartCoroutine(coroutineThrowItem(thrownItem));
    }
    public void AttachMonitor(Transform monitor){
        StartCoroutine(coroutineAttachMonitor(monitor));
    }
    public void GrabItem(Transform grabbedItem){
        handState.SwitchHandState("grab");
        thumb_obj.SetActive(true);
    }
    IEnumerator coroutineAttachMonitor(Transform monitor){
        handMoving.SwitchHandState(HAND_MOVING_STATE.AUTO_CENTER);
        yield return new WaitForSeconds(1f);
        handMoving.SwitchHandState(HAND_MOVING_STATE.AUTO_HIDE);
        handMoving.lerpSpeed = 2f;
        yield return new WaitForSeconds(3f);
        monitor_obj.SetActive(true); 
        Destroy(monitor.gameObject);

        handState.SwitchHandState("idle");
        thumb_obj.SetActive(false);
        handMoving.lerpSpeed = 5f;
        handMoving.SwitchHandState(HAND_MOVING_STATE.MANUAL);
    }
    IEnumerator coroutineThrowItem(Transform thrownItem){
        handMoving.SwitchHandState(HAND_MOVING_STATE.AUTO_CENTER);
        yield return new WaitForSeconds(1f);
        m_anime.Play(throw_anime_name);
        yield return new WaitForSeconds(0.6f);

        thrownItem.transform.parent = null;
        thrownItem.GetComponent<SpriteRenderer>().maskInteraction = SpriteMaskInteraction.None;
        thrownItem.GetComponent<ItemFlyAway>().FlyAway(handTrans.right * 20, -1000);

        handState.SwitchHandState("idle");
        thumb_obj.SetActive(false);

        yield return new WaitForSeconds(0.5f);
        handMoving.SwitchHandState(HAND_MOVING_STATE.MANUAL);
        GetComponent<PointClick_InteractableHandler>().AllowTouch();
        EventHandler.Call_OnPickUpMonitor();
    }
}
