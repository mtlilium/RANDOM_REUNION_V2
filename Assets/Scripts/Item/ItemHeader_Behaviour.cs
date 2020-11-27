using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemHeader_Behaviour : MonoBehaviour{
    bool selectable=true;
    public bool Selectable {
        get {
            return selectable;
        }
        set {
            grayMask.SetActive(!value);
            selectable = value;
        }
    }
    GameObject grayMask = null;
    private void Awake() {
        grayMask = transform.Find("grayMask").gameObject;
        Selectable = true;
    }

    private void OnDisable() {
        //デフォルトで選べる状態にしておく　選べない状態にするときは外部からSelectableをfalseにする
        Selectable = true;  
    }
}
