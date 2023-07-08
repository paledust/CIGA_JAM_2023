using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatteryBreath : MonoBehaviour
{
    [SerializeField] private SpriteRenderer batteryGlowSprite;
    [SerializeField] private float breathSpeed = 1;
    private Color color;
    // Start is called before the first frame update
    void Start(){
        color = batteryGlowSprite.color;
    }

    // Update is called once per frame
    void Update()
    {
        color.a = 0.5f*Mathf.Sin(Time.time*breathSpeed)+0.5f;
        batteryGlowSprite.color = color;
    }
}
