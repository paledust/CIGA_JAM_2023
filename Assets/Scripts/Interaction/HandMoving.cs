using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandMoving : MonoBehaviour
{
    [SerializeField] private Transform robotHand_Trans;
    private float hand_depth;
    private Camera playerCam;

    void OnEnable(){
        playerCam = Camera.main;
        hand_depth = playerCam.WorldToScreenPoint(robotHand_Trans.position).z;
    }

    void Update(){
        Vector3 mousePos = PointClick_InteractableHandler.MouseScrPos;
        mousePos.z = hand_depth;
        robotHand_Trans.position = playerCam.ScreenToWorldPoint(mousePos);
    }
}
