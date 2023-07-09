using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mushroom : BasicPointAndClickInteractable
{
    [SerializeField] private float growSec = 2f;
    [SerializeField] private float growShakeAngle = 5f;
    [SerializeField] private float growShakeFreq = 10f;
    [SerializeField] private float growShakeOffset = 0.2f;
    [SerializeField] private string mushroomWords;
    private SpriteRenderer m_sprite;
    private float originalScale;
    private bool shaking = false;
    public void ShrinkToZero(){
        DisableHitbox();
        m_sprite = GetComponent<SpriteRenderer>();
        originalScale = transform.localScale.x;
        transform.localScale = Vector3.zero;
        transform.position -= transform.up*growShakeOffset;
    }
    void Update(){

    }
    public void Grow()=>StartCoroutine(coroutineGrow());
    public override void OnHover(bool isTouching, PointClick_InteractableHandler pointclick_handler){
        base.OnHover(isTouching, pointclick_handler);
        if(isTouching){
            if(!shaking) StartCoroutine(coroutineBoin());
            EventHandler.Call_OnFeelWords(mushroomWords);
        }
    }
    IEnumerator coroutineBoin(){
        shaking = true;
        float expandScale = 1.1f;
        for(float t=0; t<1; t+=Time.deltaTime*5f){
            transform.localScale = Vector3.Lerp(Vector3.one*originalScale, Vector3.one*originalScale*expandScale, EasingFunc.Easing.QuadEaseOut(t));
            yield return null;
        }
        for(float t=0; t<1; t+=Time.deltaTime*2f){
            transform.localScale = Vector3.Lerp(Vector3.one*originalScale*expandScale, Vector3.one*originalScale, EasingFunc.Easing.BounceEaseOut(t));
            yield return null;
        }
        transform.localScale = Vector3.one * originalScale;
        shaking = false;
    }
    IEnumerator coroutineGrow(){
        float growSpeed = 1f/growSec;
        float initRot = transform.localEulerAngles.z;
        Vector3 initPos = transform.position;
        Vector3 finalPos = transform.up*growShakeOffset + initPos;
        for(float t=0; t<1; t+=Time.deltaTime*growSpeed){
            float _t = EasingFunc.Easing.QuadEaseOut(t);
            transform.localScale = Vector3.Lerp(Vector3.zero, Vector3.one*originalScale, _t);
            transform.localRotation = Quaternion.Euler(0,0,initRot+Mathf.Sin(t*Mathf.PI*growShakeFreq)*Mathf.Lerp(growShakeAngle,0,EasingFunc.Easing.QuadEaseOut(t)));
            transform.position = Vector3.Lerp(initPos, finalPos, t);
            m_sprite.color = new Color(1,1,1,Mathf.Sin(t*growShakeFreq*Mathf.PI));
            yield return null;
        }
        m_sprite.color = Color.white;
        transform.localScale = Vector3.one*originalScale;
        transform.localPosition = finalPos;
        EnableHitbox();
    }
}
