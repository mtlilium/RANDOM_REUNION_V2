using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu_Behaviour : MonoBehaviour {
    [SerializeField]
    public DS.UI.Window menuWindow;

    [SerializeField]
    DS.UI.Window itemWindow;

    [SerializeField]
    ItemMenu.UIs usable;
    [SerializeField]
    ItemMenu.UIs unUsable;
    [SerializeField]
    ItemMenu.UIs forStory;

    private void Start() {
        //Menu.Init(menuWindow);
        Menu.Init(this);
        ItemMenu.Init(itemWindow,usable, unUsable, forStory);
    }
    public void Open() {
        menuWindow.Open();
    }
    public void OpenItemMenuToSelect(string itemName) {
        ItemMenu.OpenToSelect(itemName);
    }
    void Update() {
        if (Input.GetButtonDown("Menu")) {
            if (menuWindow == null) Debug.Log("menuWindowがnull");
            menuWindow.Toggle();
        }
    }
}
