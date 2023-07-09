using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class SubtitleSpawner : MonoBehaviour
{
    [SerializeField] private GameObject subtitle_Prefab;
    [SerializeField] private float spawnHeight;
    [SerializeField] private float spawnStep = 0;
    [SerializeField] private string[] spawnText;
    private int index = 0;
    private float spawnTime;
    void OnEnable(){
        EventHandler.E_OnDrawNewLine += SpawnASubtitle;
        EventHandler.E_OnFeelWords += SpawnASubtitle;
    }
    void OnDisable(){
        EventHandler.E_OnDrawNewLine -= SpawnASubtitle;
        EventHandler.E_OnFeelWords -= SpawnASubtitle;
    }
    void SpawnASubtitle(string text){
        if(spawnTime+spawnStep>Time.time) return;
        GameObject spawnedText = GameObject.Instantiate(subtitle_Prefab, transform.position + transform.up*Random.Range(-spawnHeight/2, spawnHeight/2), Quaternion.identity);
        spawnedText.transform.parent = transform;
        spawnedText.GetComponent<TextMeshPro>().text = text;
        spawnTime = Time.time;
    }
    void SpawnASubtitle(){
        if(spawnTime+spawnStep>Time.time+spawnStep) return;
        GameObject spawnedText = GameObject.Instantiate(subtitle_Prefab, transform.position + transform.up*Random.Range(-spawnHeight/2, spawnHeight/2), Quaternion.identity);
        spawnedText.transform.SetParent(transform);
        spawnedText.GetComponent<TextMeshPro>().text = spawnText[index];
        index ++;
        index = index%spawnText.Length;
        spawnTime = Time.time;
    }
    void OnDrawGizmosSelected(){
        Gizmos.matrix = transform.localToWorldMatrix;
        Gizmos.color  = Color.green;
        Gizmos.DrawWireCube(Vector3.zero, Vector3.up*spawnHeight);
    }
}
