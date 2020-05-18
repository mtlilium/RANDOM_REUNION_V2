using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    DS.UI.Window menu;
    // Update is called once per frame
    void Update(){
        if (Input.GetButtonDown("Menu")) {
            menu.Toggle();
        }
    }
}
