using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OuterMap_Behaviour : MonoBehaviour{
    public Dictionary<string, InnerMap_Behaviour> innerMapDict { get; private set; }
    void Awake(){
        innerMapDict = new Dictionary<string, InnerMap_Behaviour>();
        foreach(var innerMap in transform.GetComponentsInChildren<InnerMap_Behaviour>(true)) {
            innerMapDict[innerMap.gameObject.name] = innerMap;
        }
        if (SystemClass.OuterMapDict == null) SystemClass.InitOuterMapDict();
        SystemClass.OuterMapDict.Add(gameObject.name, this);
    }
}
