using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1_Ending : MonoBehaviour
{
    [SerializeField] private float endingDelay = 5;
    [SerializeField] private PointClick_InteractableHandler interactableHandler;
    private bool detection = false;
    private bool startEnding = false;
    void OnEnable(){EventHandler.E_OnPickUpMonitor += PrepareEnding;}
    void OnDisable(){EventHandler.E_OnPickUpMonitor -= PrepareEnding;}
    void PrepareEnding(){
        detection = true;
    }
    void Update(){
        if(detection){
            if(interactableHandler.IsTouching && !startEnding){
                startEnding = true;
                this.enabled = false;
                StartCoroutine(coroutineStartEnding());
            }
        }
    }
    IEnumerator coroutineStartEnding(){
        yield return new WaitForSeconds(endingDelay);
        GameManager.Instance.SwitchingScene("Level_1", GameManager.loadingScreenScene);
    }
}
