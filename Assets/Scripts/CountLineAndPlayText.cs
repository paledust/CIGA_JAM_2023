using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountLineAndPlayText : MonoBehaviour
{
    [SerializeField] private int count = 8;
    private int counting = 0;
    void OnEnable(){
        EventHandler.E_OnDrawNewLine += checkLineAmount;
    }
    void OnDisable(){
        EventHandler.E_OnDrawNewLine -= checkLineAmount;
    }
    void checkLineAmount(){
        counting ++;
        if(counting == count){
            EventHandler.Call_OnDrawSeveralLines();
            Destroy(this);
        }
    }
}
