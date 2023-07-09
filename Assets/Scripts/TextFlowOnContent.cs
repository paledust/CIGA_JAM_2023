using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class TextFlowOnContent : MonoBehaviour
{
    [SerializeField] private TextMeshPro textMesh;
    [SerializeField] private Transform textMesh_trans;
    [SerializeField] private float moveSpeed = 0;
    void OnEnable(){
        EventHandler.E_OnDrawSeveralLines += StartMovement;
        EventHandler.E_OnPickUpMonitor += StartMovement;
        EventHandler.E_OnFeelWords += FillAndStartMovement;
        EventHandler.E_OnHandShake += CrazyMovement;
    }
    void OnDisable(){
        EventHandler.E_OnDrawSeveralLines -= StartMovement;
        EventHandler.E_OnPickUpMonitor -= StartMovement;
        EventHandler.E_OnFeelWords -= FillAndStartMovement;
        EventHandler.E_OnHandShake -= CrazyMovement;
    }
    void FillAndStartMovement(string content){
        textMesh.text = content;
        Vector3 pos = textMesh_trans.localPosition;
        pos.x = -textMesh.preferredWidth-4;
        textMesh_trans.localPosition = pos;
        StartMovement();
    }
    void StartMovement(){
        moveSpeed = 5;
    }
    void CrazyMovement(){
        moveSpeed = 40;
    }
    void Update()
    {
        textMesh_trans.localPosition += Vector3.right * moveSpeed * Time.deltaTime;
        if(textMesh_trans.localPosition.x>4){
            Vector3 pos = textMesh_trans.localPosition;
            pos.x = -textMesh.preferredWidth-4;
            textMesh_trans.localPosition = pos;
        }
    }
}
