using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class SubtitleSpawner : MonoBehaviour
{
    [SerializeField] private GameObject subtitle_Prefab;
    [SerializeField] private float spawnHeight;
    [SerializeField] private string[] spawnText;
    private int index = 0;
    void OnEnable(){
        EventHandler.E_OnDrawNewLine += SpawnASubtitle;
    }
    void OnDsiable(){
        EventHandler.E_OnDrawNewLine -= SpawnASubtitle;
    }
    void SpawnASubtitle(){
        GameObject spawnedText = GameObject.Instantiate(subtitle_Prefab, transform.position + Vector3.up*Random.Range(-spawnHeight/2, spawnHeight/2), Quaternion.identity);
        spawnedText.transform.parent = transform;
        spawnedText.GetComponent<TextMeshPro>().text = spawnText[index];
        index ++;
        index = index%spawnText.Length;
    }
    void OnDrawGizmosSelected(){
        Gizmos.matrix = transform.localToWorldMatrix;
        Gizmos.color  = Color.green;
        Gizmos.DrawWireCube(Vector3.zero, Vector3.up*spawnHeight);
    }
}
