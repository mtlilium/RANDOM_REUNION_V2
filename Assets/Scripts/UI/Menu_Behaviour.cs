using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu_Behaviour : MonoBehaviour {
    [SerializeField]
    ItemMenu.UIs usable;
    [SerializeField]
    ItemMenu.UIs unUsable;
    [SerializeField]
    ItemMenu.UIs forStory;

    private void Awake() {
        Menu.Init(this.GetComponent<DS.UI.Window>());
        ItemMenu.Init(usable, unUsable, forStory);
    }
}
