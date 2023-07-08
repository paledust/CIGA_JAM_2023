using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mushroom : MonoBehaviour
{
    private float originalScale;
    public void ShrinkToZero(){
        originalScale = transform.localScale.x;
        transform.localScale = Vector3.zero;
    }
}
