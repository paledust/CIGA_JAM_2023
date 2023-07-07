using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleAudioSystem;

public class Tester : MonoBehaviour
{
    void Start()
    {
        AudioManager.Instance.PlayAmbience("forest");        
    }
}
