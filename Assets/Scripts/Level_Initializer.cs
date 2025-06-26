using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class Level_Initializer : MonoBehaviour
{
    [SerializeField] private HandMoving handMoving;
    [SerializeField] private int start_Delay = 1;
    async void Start(){
        // await Task.Delay(start_Delay*1000);
        handMoving.enabled = true;
    }
}
