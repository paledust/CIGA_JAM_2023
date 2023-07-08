using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleAudioSystem;

public class HandGrabbing_Detection : MonoBehaviour
{
    [SerializeField] private Transform hand;
    [SerializeField] private HandMoving handMoving;
    [SerializeField] private Hand_Grab handGrab;
    [SerializeField, ShowOnly] private HAND_GRAB_STATE handState = HAND_GRAB_STATE.IDLE;
[Header("Garbage Spawn")]
    [SerializeField] private GarbageSpawner garbageSpawner;
    [SerializeField] private float firstTimegarbageSpawnTime = 1.2f;
    [SerializeField] private float garbageSpawnTime = 2f;
    public bool Grabbed{get{return handState == HAND_GRAB_STATE.GRAB;}}
    public bool GrabbedMonitor{get{return grabbedMonitor;}}
    public float GarbageSpawnTimer{get; private set;} = 0;
    public Transform SpawnedGarbage{get; private set;}
    private bool grabbedMonitor = false;
    private bool isFirstTime = true;
    private Collider2D hitbox;
    void Awake()=>hitbox = GetComponent<Collider2D>();
    void OnEnable(){
        GarbageSpawnTimer = 0;
    }
    private void OnTriggerEnter2D(Collider2D other){
        if(other.transform == hand){
            switch(handState){
                case HAND_GRAB_STATE.IDLE:
                    handState = HAND_GRAB_STATE.FINDING;
                    GarbageSpawnTimer = 0;
                    break;
                case HAND_GRAB_STATE.GRAB:
                    break;
                case HAND_GRAB_STATE.FINDING:
                    break;
            }            
        }
    }

    // Update is called once per frame
    void Update()
    {
        switch (handState){
            case HAND_GRAB_STATE.FINDING:
                if(handMoving.positionOffset.magnitude>1){
                    GarbageSpawnTimer += Time.deltaTime;
                    if(GarbageSpawnTimer>(isFirstTime?firstTimegarbageSpawnTime:garbageSpawnTime)){
                        isFirstTime = false;
                        handState = HAND_GRAB_STATE.GRAB;
                        GarbageSpawnTimer = 0;
                        SpawnedGarbage = garbageSpawner.Spawn_Garbage(out grabbedMonitor);
                        handGrab.GrabItem(SpawnedGarbage);

                        AudioManager.Instance.PlaySoundEffect("found", 1);
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
                case HAND_GRAB_STATE.IDLE:
                    GarbageSpawnTimer = 0;
                    break;
                case HAND_GRAB_STATE.FINDING:
                    GarbageSpawnTimer = 0;
                    handState = HAND_GRAB_STATE.IDLE;
                    break;
                case HAND_GRAB_STATE.GRAB:
                    handMoving.lerpSpeed = 5f;
                    break;
            }
        }
    }
    public void ResetGrab(){
        handState = HAND_GRAB_STATE.IDLE;
        SpawnedGarbage = null;
    }
}
