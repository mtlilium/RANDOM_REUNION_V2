using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class VerticalTabController : MonoBehaviour{
    DS.UI.Tab tab;
    // Update is called once per frame
    private void Start() {
        tab = GetComponent<DS.UI.Tab>();
    }
    void Update(){
        if (Input.GetKeyDown(KeyCode.UpArrow)) {
            tab.PreviousTab();
        }
        if (Input.GetKeyDown(KeyCode.DownArrow)) {
            tab.NextTab();
        }
    }
}
