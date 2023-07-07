using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand_Grab : MonoBehaviour
{
    [SerializeField] private HandMoving handMoving;
    [SerializeField] private GameObject thumb_obj;
    [SerializeField] private SpriteRenderer handRenderer;
    [SerializeField] private Sprite hand_Front;
    [SerializeField] private Sprite hand_Grab;
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
        handRenderer.sprite = hand_Grab;
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

        handRenderer.sprite = hand_Front;
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
        thrownItem.GetComponent<ItemFlyAway>().FlyAway(handRenderer.transform.right * 20, -1000);

        handRenderer.sprite = hand_Front;
        thumb_obj.SetActive(false);

        yield return new WaitForSeconds(0.5f);
        handMoving.SwitchHandState(HAND_MOVING_STATE.MANUAL);
    }
}
