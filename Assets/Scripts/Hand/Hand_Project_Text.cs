using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Hand_Project_Text : MonoBehaviour
{
    [SerializeField] private GameObject textProjectilePrefab;
    [SerializeField] private Transform tipTrans;
    [SerializeField] private float spreadAngle = 10;
    [SerializeField] private float projectStep;
    private bool isProjecting = false;
    private float projectTime;
    void FixedUpdate(){
        if(isProjecting) {
            if(projectTime+projectStep<Time.time){
                Text_Projectile projectile = GameObject.Instantiate(textProjectilePrefab, tipTrans.position, Quaternion.identity).GetComponent<Text_Projectile>();
                projectile.OnProject(tipTrans.position, Quaternion.Euler(0,0,Random.Range(-spreadAngle,spreadAngle)) * tipTrans.up);
                projectTime = Time.time;
            }
        }

    }
    void OnInteract(InputValue value){
        if(value.isPressed){
            projectTime = Time.time;
            isProjecting = true;
        }
        else isProjecting = false;
    }
}
