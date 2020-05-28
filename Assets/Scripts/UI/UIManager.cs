using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    DS.UI.Window menu=null;
    
    void Update(){
        if (Input.GetButtonDown("Menu")) {
            menu.Toggle();
        }
    }
}

/*
public static class TabControllerManager {
    public static VerticalTabController nowController { get; private set; }
    public static void ChangeController(VerticalTabController newController) {
        nowController = newController;
    }    
}

public static class WindowActivatorManager {
    public static WindowActivator nowActivator { get; private set; }
    public static void ChangeActivator(WindowActivator newActivator) {
        nowActivator = newActivator;
    }
}
*/