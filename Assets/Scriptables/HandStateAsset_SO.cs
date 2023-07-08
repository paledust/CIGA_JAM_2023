using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "MyGame/HandStateAsset_SO")]
public class HandStateAsset_SO : ScriptableObject
{
    [System.Serializable]
    public class handStateAsset{
        public string stateKey;
        public Sprite stateSprite;
    }
    [SerializeField] private List<handStateAsset> handStateAssetList;
    public Sprite GetStateSprite(string stateKey){
        return handStateAssetList.Find(x=>x.stateKey == stateKey).stateSprite;
    }
}
