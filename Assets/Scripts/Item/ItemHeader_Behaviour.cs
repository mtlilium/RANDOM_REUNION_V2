using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemHeader_Behaviour : MonoBehaviour{
    bool selectable;
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
    }

    private void Start() {
        Selectable = true;
    }
}
