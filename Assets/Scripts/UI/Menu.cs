using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public static class Menu{
    static MenuInUIScene_Behaviour menuInUIScene;
    public static void Init(MenuInUIScene_Behaviour _menu) {
        menuInUIScene = _menu;
    }
    static public void Open() {
        menuInUIScene.OpenMenuWindow();
    }
    static public void Close() {
        menuInUIScene.CloseMenuWindow();
    }
}
