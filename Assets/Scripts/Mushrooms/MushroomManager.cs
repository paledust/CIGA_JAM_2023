using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomManager : MonoBehaviour
{
    [SerializeField] private Mushroom[] mushrooms;
    void Start()
    {
        for(int i=0; i<mushrooms.Length; i++){
            mushrooms[i].ShrinkToZero();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
