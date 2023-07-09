using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndLevel_3 : MonoBehaviour
{
    [SerializeField] private float delay = 25;
    void OnEnable(){
        EventHandler.E_OnCDPlaying += EndLevel;
    }
    void OnDisable(){
        EventHandler.E_OnCDPlaying -= EndLevel;
    }
    void EndLevel(){
        StartCoroutine(coroutineEndLevel());
    }
    IEnumerator coroutineEndLevel(){
        yield return new WaitForSeconds(delay);
        GameManager.Instance.SwitchingScene("Level_3", GameManager.loadingScreenScene);        
    }
}
