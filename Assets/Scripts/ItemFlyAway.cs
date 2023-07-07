using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemFlyAway : MonoBehaviour
{
    [SerializeField] private float duration;
    [SerializeField] private Vector3 velocity;
    [SerializeField] private float angularSpeed;
    private float time;
    void Awake()=>this.enabled = false;
    void OnEnable()=>time = Time.time;
    void Update(){
        transform.position += velocity*Time.deltaTime;
        transform.rotation *= Quaternion.Euler(0,0,angularSpeed*Time.deltaTime);

        if(time + duration < Time.time){
            Destroy(gameObject);
        }
    }
    public void FlyAway(Vector3 _velocity, float _angularSpeed){
        this.enabled = true;
        angularSpeed = _angularSpeed;
        velocity = _velocity;
    }
}
