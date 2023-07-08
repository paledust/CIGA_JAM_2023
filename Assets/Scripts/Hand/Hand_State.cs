using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand_State : MonoBehaviour
{
    [SerializeField] private SpriteRenderer hand_renderer;
    [SerializeField] private HandStateAsset_SO handStateAsset;
    [SerializeField] private GameObject thumb_obj;
    public void SwitchHandState(string stateKey){
        hand_renderer.sprite = handStateAsset.GetStateSprite(stateKey);
        if(stateKey == "grab") thumb_obj.SetActive(true);
        else thumb_obj.SetActive(false);
    }
}
