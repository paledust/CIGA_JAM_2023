using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class BubblingUp : MonoBehaviour
{
    [SerializeField] private TextMeshPro tm;
    [SerializeField] private float movingSpeed = 3;
    [SerializeField] private float dieDown = 2;
    [SerializeField] private float noiseAmp = 1;
    [SerializeField] private float noiseFreq=3;
    private float xPos;
    private float seed;
    private float timer;
    void Start(){
        movingSpeed += Random.Range(-0.5f,2f);
        seed = Random.Range(0f,1f);
        xPos = transform.position.x;
        timer= 0;
    }
    // Update is called once per frame
    void Update()
    {
        if(timer<1){
            timer += Time.deltaTime*dieDown;
            timer = Mathf.Min(1, timer);
            Vector3 pos = transform.position;
            pos += Vector3.up * Mathf.Lerp(movingSpeed, 0, (timer)) * Time.deltaTime;
            pos.x = xPos + Mathf.Lerp(noiseAmp,0,(timer))*(Mathf.PerlinNoise(Time.time*noiseFreq, seed)*2-1);
            transform.position = pos;
            if(timer == 1){
                this.enabled = false;
                StartCoroutine(CoroutineFadeBubble());
            }
        }
    }
    IEnumerator CoroutineFadeBubble(){
        yield return new WaitForSeconds(2);
        Color initCol, targetCol;
        targetCol = initCol = tm.color;
        targetCol.a = 0;
        for(float t=0; t<1; t+=Time.deltaTime*0.5f){
            tm.color = Color.Lerp(initCol, targetCol, EasingFunc.Easing.SmoothInOut(t));
            yield return null;
        }
        tm.color = targetCol;
        Destroy(gameObject);
    }

}
