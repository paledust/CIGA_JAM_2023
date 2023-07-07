using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class HandMoving : MonoBehaviour
{
    [SerializeField] private Transform robotTarget_Trans;
    public float lerpSpeed = 5;
    private Camera playerCam;
    public Vector2 positionOffset{get; private set;}
    private Vector3 targetPosition;
    private Vector3 resetPosition;
    private bool lockToCenter = false;
    void OnEnable(){
        playerCam = Camera.main;
        targetPosition.z = robotTarget_Trans.position.z;
        positionOffset = Vector2.zero;
        resetPosition = playerCam.ViewportToWorldPoint(Vector3.one*0.6f);
        resetPosition.z = robotTarget_Trans.position.z;
    }
    void Update(){
        if(lockToCenter){
            targetPosition = resetPosition;
        }
        else{
            Vector3 mousePos = Mouse.current.position.ReadValue();
            targetPosition = playerCam.ScreenToWorldPoint(mousePos);
            targetPosition.z = robotTarget_Trans.position.z;
        }

        robotTarget_Trans.position = Vector3.Lerp(robotTarget_Trans.position, targetPosition, Time.deltaTime * lerpSpeed);
        positionOffset = targetPosition - robotTarget_Trans.position;
    }
    public void SwitchLockToCenter(bool isLocked){lockToCenter = isLocked;}
}
