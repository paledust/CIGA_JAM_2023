using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1_Ending : MonoBehaviour
{
    [SerializeField] private float endingDelay = 5;
    [SerializeField] private PointClick_InteractableHandler interactableHandler;
    // void OnEnable(){EventHandler.E_OnPickUpMonitor +=}
    void OnDisable(){}
    public void StartEnding(){
        StartCoroutine(coroutineStartEnding());
    }
    IEnumerator coroutineStartEnding(){
        yield return new WaitForSeconds(endingDelay);
        GameManager.Instance.SwitchingScene("Level_1", GameManager.loadingScreenScene);
    }
}
