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
    [SerializeField] private GarbageSpawner garbageSpawner;
    [SerializeField] private float garbageSpawnTime = 2f;
    private bool grabbed = false;
    private bool grabbedMonitor = false;
    private float garbageSpawnTimer = 0;
    private Transform spawnedGarbage;
    private Collider2D hitbox;
    void Awake()=>hitbox = GetComponent<Collider2D>();
    private void OnTriggerEnter2D(Collider2D other){
        if(other.transform == hand){
            switch(handState){
                case HAND_IN_HOLE_STATE.ON_TOP:
                    break;
                case HAND_IN_HOLE_STATE.OUT_BOUND:
                    garbageSpawnTimer = 0;
                    handState = HAND_IN_HOLE_STATE.IN_HOLE;
                    handMoving.lerpSpeed = 1;
                    break;
                case HAND_IN_HOLE_STATE.IN_HOLE:
                    break;
            }            
        }
    }
    void Update(){
        switch (handState){
            case HAND_IN_HOLE_STATE.IN_HOLE:
                if(handMoving.positionOffset.magnitude>1){
                    garbageSpawnTimer += Time.deltaTime;
                    if(!grabbed && garbageSpawnTimer>garbageSpawnTime){
                        grabbed = true;
                        garbageSpawnTimer = 0;
                        spawnedGarbage = garbageSpawner.Spawn_Garbage(out grabbedMonitor);
                        handGrab.GrabItem(spawnedGarbage);
                    }
                }
                break;
            default:
                break;
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
                    handState = HAND_IN_HOLE_STATE.OUT_BOUND;
                    handMoving.lerpSpeed = 5f;
                    if(grabbed){
                        if(!grabbedMonitor){
                            grabbed = false;
                            StartCoroutine(coroutineReset());
                            handMask.SetActive(false);
                            handGrab.ThrowItem(spawnedGarbage);
                            spawnedGarbage = null;
                        }
                        else{
                            handMask.SetActive(false);
                            hitbox.enabled = false;
                            this.enabled = false;
                            handGrab.KeepItem(spawnedGarbage);
                        }
                    }
                    break;
            }
        }
    }
    IEnumerator coroutineReset(){
        hitbox.enabled = false;
        handState = HAND_IN_HOLE_STATE.ON_TOP;
        yield return new WaitForSeconds(3f);
        hitbox.enabled = true;
    }
}
