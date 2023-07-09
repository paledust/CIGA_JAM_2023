using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOnSubtitleSpawnerOnDJ : MonoBehaviour
{
    [SerializeField] private GameObject spawner_obj;
    void OnEnable(){
        EventHandler.E_OnCDPlaying += ActivateSpawner;
    }
    void OnDisable(){
        EventHandler.E_OnCDPlaying -= ActivateSpawner;
    }
    void ActivateSpawner(){
        spawner_obj.SetActive(true);
        Destroy(this);
    }
}
