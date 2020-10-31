using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ControllHandlerManager{
    public static ControllHandler nowHandler { get; private set; }
    public static ControllHandler previousController { get; private set; }
    public static void ChangeController(ControllHandler newHandler) {
        previousController = nowHandler;
        nowHandler?.StopControll();
        nowHandler = newHandler;
        newHandler?.StartControll();
    }
}
