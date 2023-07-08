using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class MovingRight : MonoBehaviour
{
    [SerializeField] private float movingSpeed;
    void Update()
    {
        transform.position += Vector3.left * movingSpeed * Time.deltaTime;
        if(transform.position.x<-23){
            Destroy(gameObject);
        }
    }
}
