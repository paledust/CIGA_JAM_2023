using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandInHole_Detection : MonoBehaviour
{
    [SerializeField] private Transform hand;
    [SerializeField] private HandMoving handMoving;
    [SerializeField] private Hand_Grab handGrab;
    [SerializeField] private GameObject handMask;
    [SerializeField, ShowOnly] private HAND_IN_HOLE_STATE handState = HAND_IN_HOLE_STATE.ON_TOP;
[Header("Garbage Spawn")]
    [SerializeField] private HandGrabbing_Detection handGrabbing_Detection;
    private Collider2D hitbox;
    void Awake(){
        handGrabbing_Detection.gameObject.SetActive(false);
        hitbox = GetComponent<Collider2D>();
    }
    private void OnTriggerEnter2D(Collider2D other){
        if(other.transform == hand){
            switch(handState){
                case HAND_IN_HOLE_STATE.ON_TOP:
                    break;
                case HAND_IN_HOLE_STATE.OUT_BOUND:
                    handGrabbing_Detection.gameObject.SetActive(true);
                    handState = HAND_IN_HOLE_STATE.IN_HOLE;
                    handMoving.lerpSpeed = 1.5f;
                    break;
                case HAND_IN_HOLE_STATE.IN_HOLE:
                    break;
            }            
        }
    }
    private void OnTriggerExit2D(Collider2D other){
        if(other.transform == hand){
            switch(handState){
                case HAND_IN_HOLE_STATE.ON_TOP:
                    handState = HAND_IN_HOLE_STATE.OUT_BOUND;
                    handMask.SetActive(true);
                    break;
                case HAND_IN_HOLE_STATE.OUT_BOUND:
                    break;
                case HAND_IN_HOLE_STATE.IN_HOLE:
                    handGrabbing_Detection.gameObject.SetActive(false);
                    handState = HAND_IN_HOLE_STATE.OUT_BOUND;
                    handMoving.lerpSpeed = 5f;
                    if(handGrabbing_Detection.Grabbed){
                        if(!handGrabbing_Detection.GrabbedMonitor){
                            // grabbed = false;
                            // spawnedGarbage = null;
                            StartCoroutine(coroutineReset());
                            handMask.SetActive(false);
                            handGrab.ThrowItem(handGrabbing_Detection.SpawnedGarbage);
                            handGrabbing_Detection.ResetGrab();
                        }
                        else{
                            handMask.SetActive(false);
                            hitbox.enabled = false;
                            this.enabled = false;
                            handGrab.AttachMonitor(handGrabbing_Detection.SpawnedGarbage);
                        }
                    }
                    break;
            }
        }
    }
    IEnumerator coroutineReset(){
        hitbox.enabled = false;
        handState = HAND_IN_HOLE_STATE.ON_TOP;
        yield return new WaitForSeconds(2.5f);
        hitbox.enabled = true;
    }
}
