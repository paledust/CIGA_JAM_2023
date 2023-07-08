using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextFlowOnContent : MonoBehaviour
{
    [SerializeField] private Transform textMesh_trans;
    [SerializeField] private float moveSpeed = 0;
    void OnEnable(){
        EventHandler.E_OnDrawNewLine += StartMovement;
        EventHandler.E_OnPickUpMonitor += StartMovement;
    }
    void OnDisable(){
        EventHandler.E_OnDrawNewLine -= StartMovement;
        EventHandler.E_OnPickUpMonitor -= StartMovement;
    }
    void StartMovement(){
        moveSpeed = 5;
    }
    void Update()
    {
        textMesh_trans.localPosition += Vector3.right * moveSpeed * Time.deltaTime;
        if(textMesh_trans.localPosition.x>6){
            Vector3 pos = textMesh_trans.localPosition;
            pos.x = -33;
            textMesh_trans.localPosition = pos;
        }
    }
}
