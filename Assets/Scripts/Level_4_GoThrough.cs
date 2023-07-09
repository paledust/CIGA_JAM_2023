using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level_4_GoThrough : MonoBehaviour
{
    [SerializeField] private Hand_Project_Text project_Text;
    [SerializeField] private Hand_Shaking handShake;
    private void Awake(){
        EventHandler.E_OnHandShake += StartProjectText;
    }
    private void OnDestroy(){
        EventHandler.E_OnHandShake += StartProjectText;
    }
    void StartProjectText(){
        project_Text.enabled = true;
        handShake.StopShake();
    }
}
