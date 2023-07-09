using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand_Shaking : MonoBehaviour
{
    [SerializeField] private Transform handTrans;
    [SerializeField] private float maxShakeFreq;
    [SerializeField] private float maxShakeAmp;
[Header("Initiative")]
    [SerializeField] private float delay = 2;
    [SerializeField] private float shakeKickInTime = 1;
    private float timer;
    private float shakeFreq;
    private float shakeAmp;
    void Awake()=>EventHandler.E_OnQuestionAsked += KickInHandShake;
    void OnDestroy()=>EventHandler.E_OnQuestionAsked -= KickInHandShake;
    void LateUpdate()
    {
        timer += Time.deltaTime * shakeFreq;
        handTrans.localRotation = Quaternion.Euler(0,0,Mathf.Sin(timer*Mathf.PI)*shakeAmp);
    }
    void KickInHandShake()=>StartCoroutine(coroutineFadeInShaking());
    IEnumerator coroutineFadeInShaking(){
        this.enabled = true;
        yield return new WaitForSeconds(delay);
        for(float t=0; t<1; t+=Time.deltaTime/shakeKickInTime){
            float _t = EasingFunc.Easing.QuadEaseOut(t);
            shakeFreq = Mathf.Lerp(0, maxShakeFreq, _t);
            shakeAmp  = Mathf.Lerp(0, maxShakeAmp, _t);
            yield return null;
        }
        shakeAmp = maxShakeAmp;
        shakeFreq = maxShakeFreq;
        EventHandler.Call_OnHandShake();
    }
}
