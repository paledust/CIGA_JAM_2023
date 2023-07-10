using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndLevel_Auto : MonoBehaviour
{
    [SerializeField] private float delay;
    void Start()
    {
        StartCoroutine(coroutineEndLevel());
    }
    IEnumerator coroutineEndLevel(){
        yield return new WaitForSeconds(delay);
        GameManager.Instance.SwitchingScene("EndLevel","Level_1");
        GameManager.Instance.ResetGameState();
    }
}
