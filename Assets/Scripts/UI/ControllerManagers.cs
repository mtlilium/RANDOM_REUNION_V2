using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ControllerManagers {
    public enum ControllerType {
        DEFAULT,
        VERTICAL_TAB,
        HORIZONTAL_TAB,
        WINDOW_ACTIVATE,
    }
    public static Dictionary<ControllerType,ControllerManager> controllerManagerDict { get; private set; }

    static public bool initiallized { get; private set; }
    public static void Init() {
        if (initiallized) return;
        initiallized = true;
        controllerManagerDict = new Dictionary<ControllerType, ControllerManager>();
        foreach(ControllerType controllerType in Enum.GetValues(typeof(ControllerType))) {
            controllerManagerDict.Add(controllerType, new ControllerManager());
        }       
    }
}

public class ControllerManager{
    public Controller nowController { get; private set; }
    public void ChangeController(Controller newController) {
        nowController?.StopControll();
        nowController = newController;
        newController?.StartControll();
    }
}

