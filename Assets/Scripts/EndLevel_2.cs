using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndLevel_2 : MonoBehaviour
{
    [SerializeField] private float delay = 3;
    void OnEnable(){
        EventHandler.E_OnFinishDrawLine += EndLevel;
    }
    void OnDisable(){
        EventHandler.E_OnFinishDrawLine -= EndLevel;
    }
    void EndLevel()=>StartCoroutine(coroutineEndLevel());
    IEnumerator coroutineEndLevel(){
        yield return new WaitForSeconds(delay);
        GameManager.Instance.SwitchingScene("Level_2", GameManager.loadingScreenScene);
    }
}
