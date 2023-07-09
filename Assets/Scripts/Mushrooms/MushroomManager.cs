using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomManager : MonoBehaviour
{
    [SerializeField] private Mushroom[] mushrooms;
    [SerializeField] private Vector2 delayRange;
    [SerializeField] private float growCycle = 2;
    private int growIndex = 0;
    private float nextGrowTimer;
    void Awake(){
        EventHandler.E_OnCDPlaying+=WakeUpMushroomCycle;
    }
    void OnDestroy(){
        EventHandler.E_OnCDPlaying-=WakeUpMushroomCycle;
    }
    void WakeUpMushroomCycle(){
        nextGrowTimer = Time.time;
        this.enabled = true;
    }
    void Start(){
        nextGrowTimer = Time.time;
        Service.Shuffle<Mushroom>(ref mushrooms);
        for(int i=0; i<mushrooms.Length; i++){
            mushrooms[i].ShrinkToZero();
        }
        this.enabled = false;
    }
    void Update(){
        if(nextGrowTimer+growCycle<Time.time){
            nextGrowTimer = Time.time;
            StartCoroutine(coroutineGrowMushrooms(Random.Range(2, 5)));
            if(growIndex==mushrooms.Length){
                this.enabled = false;
            }
        }
    }
    IEnumerator coroutineGrowMushrooms(int amount){
        int startIndex= growIndex;
        int lastIndex = growIndex + amount-1;
        lastIndex = Mathf.Min(lastIndex, mushrooms.Length);
        growIndex = lastIndex;
        for(int i=startIndex; i<lastIndex; i++){
            mushrooms[i].Grow();
            yield return new WaitForSeconds(Random.Range(delayRange.x, delayRange.y));
        }
    }
}
