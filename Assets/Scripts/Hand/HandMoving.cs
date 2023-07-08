using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class HandMoving : MonoBehaviour
{
    [SerializeField] private Transform robotTip_Trans;
    [SerializeField] private Transform hand_trans;
    [SerializeField] private Transform robotTarget_Trans;
    [SerializeField] private Transform robotHide_Trans;
    [SerializeField, ShowOnly] private HAND_MOVING_STATE handMoveState = HAND_MOVING_STATE.MANUAL;
[Header("Audiosource")]
    [SerializeField, Range(0,1)] private float maxVolume = 0.5f;
    [SerializeField] private AudioSource sfx_audio;
    [SerializeField] private AudioClip moveClip;
    public float lerpSpeed = 5;
    private Camera playerCam;
    public Vector2 positionOffset{get; private set;}
    private Vector3 targetPosition;
    private Vector3 resetPosition;
    private Vector3 tipToHandOffset;
    void OnEnable(){
        playerCam = Camera.main;
        targetPosition.z = robotTarget_Trans.position.z;
        positionOffset = Vector2.zero;
        resetPosition = playerCam.ViewportToWorldPoint(Vector3.one*0.6f);
        resetPosition.z = robotTarget_Trans.position.z;

        tipToHandOffset = hand_trans.position - robotTip_Trans.position;
        sfx_audio.clip = moveClip;
    }
    void Update(){
        switch(handMoveState){
            case HAND_MOVING_STATE.MANUAL:
                Vector3 mousePos = Mouse.current.position.ReadValue();
                targetPosition = playerCam.ScreenToWorldPoint(mousePos);
                targetPosition.z = robotTarget_Trans.position.z;
                break;
            case HAND_MOVING_STATE.AUTO_CENTER:
                targetPosition = resetPosition;
                break;
            case HAND_MOVING_STATE.AUTO_HIDE:
                targetPosition = robotHide_Trans.position;
                break;
        }

        robotTarget_Trans.position = Vector3.Lerp(robotTarget_Trans.position, targetPosition, Time.deltaTime * lerpSpeed);
        positionOffset = targetPosition - robotTarget_Trans.position;
        if(positionOffset.magnitude>1f){
            if(!sfx_audio.isPlaying) sfx_audio.Play();
            sfx_audio.volume = Mathf.Lerp(sfx_audio.volume, maxVolume, Time.deltaTime * 5);
        }
        else{
            sfx_audio.volume = Mathf.Lerp(sfx_audio.volume, 0, Time.deltaTime * 5);
        }
    }
    public void SwitchHandState(HAND_MOVING_STATE moveState)=>handMoveState = moveState;
}
