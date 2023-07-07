using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandMoving : MonoBehaviour
{
    [SerializeField] private Transform robotTarget_Trans;
    [SerializeField] private SpriteRenderer handRenderer;
    [SerializeField] private GameObject thumb_obj;
    [SerializeField] private Sprite hand_Front;
    [SerializeField] private Sprite hand_Grab;
    public float lerpSpeed = 5;
    private Camera playerCam;
    public Vector2 positionOffset{get; private set;}
    private Vector3 targetPosition;
    void OnEnable(){
        playerCam = Camera.main;
        targetPosition.z = robotTarget_Trans.position.z;
        positionOffset = Vector2.zero;
    }
    void Update(){
        Vector3 mousePos = PointClick_InteractableHandler.MouseScrPos;
        targetPosition = playerCam.ScreenToWorldPoint(mousePos);
        targetPosition.z = robotTarget_Trans.position.z;

        robotTarget_Trans.position = Vector3.Lerp(robotTarget_Trans.position, targetPosition, Time.deltaTime * lerpSpeed);
        positionOffset = targetPosition - robotTarget_Trans.position;
    }
    public void ThrowItem(Transform thrownItem){
        handRenderer.sprite = hand_Front;
        thumb_obj.SetActive(false);
    }
    public void KeepItem(Transform keptItem){

    }
    public void GrabItem(Transform grabbedItem){
        Debug.Log("Grab");
        handRenderer.sprite = hand_Grab;
        thumb_obj.SetActive(true);
    }
}
