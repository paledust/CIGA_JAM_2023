using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubtitleSpawner : MonoBehaviour
{
    [SerializeField] private GameObject subtitle_Prefab;
    void OnEnable(){
        EventHandler.E_OnDrawNewLine += SpawnASubtitle;
    }
    void OnDsiable(){
        EventHandler.E_OnDrawNewLine -= SpawnASubtitle;
    }
    void SpawnASubtitle(){

    }
}
