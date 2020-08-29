using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager_Behaviour : MonoBehaviour {
    [SerializeField]
    ItemBag_Behaviour itemBag = null; //ItemManager初期化用
    void Start(){
        ItemManager.Init(itemBag);
    }
}
