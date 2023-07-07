using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarbageSpawner : MonoBehaviour
{
    [SerializeField] private Transform spawnTrans;
    [SerializeField] private GameObject[] garbages;
    private int spawnIndex;
    void Awake(){
        for(int i=0; i<garbages.Length; i++){
            garbages[i].transform.parent = spawnTrans;
        }
    }
    public Transform Spawn_Garbage(out bool isLastOne){
        garbages[spawnIndex].SetActive(true);
        spawnIndex ++;

        isLastOne = spawnIndex == garbages.Length;
        return garbages[spawnIndex-1].transform;
    }
}
