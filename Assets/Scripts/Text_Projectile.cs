using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Text_Projectile : MonoBehaviour
{
    [SerializeField] private TextMeshPro tm;
    [SerializeField] private float lifeTime = 1;
    private float angularSpeed = 3000;
    private Vector3 velocity = Vector3.zero;
    private const string Libiry = "下不个久么乐书争享人什会住你信光军出刺剩动卷历去反发取受可史右味和在圾坚垃堆多大天太字存学将左干平得息意战扭技按文旋是曲有未机束来柔标核植次歌正毁毛气没法派流湿滑灭煤爆物球生用电瘪的知硬社科稠空籍类粘系结绵联能自茸言词语读谁足转软过这连那里键长霸音首魔鼠";
    private float existence;
    public void OnProject(Vector3 pos, Vector3 dir){
        transform.position = pos;
        velocity = dir * 10;
        angularSpeed = Random.Range(2500,3000);
        tm.text = Libiry[Random.Range(0, Libiry.Length)].ToString();
        gameObject.SetActive(true);
    }
    void Update(){
        existence += Time.deltaTime;

        transform.position += velocity*Time.deltaTime;
        transform.rotation *= Quaternion.Euler(0,0,angularSpeed*Time.deltaTime);

        if(existence>lifeTime){
            gameObject.SetActive(false);
        }
    }
}