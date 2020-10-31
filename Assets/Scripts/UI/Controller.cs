﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Controller : MonoBehaviour {
    protected ControllerManager controllerManager;
    protected ControllerManagers.ControllerType type;

    public bool dynamicPreviousControllerSetting;
    [SerializeField]
    [HideInInspector]
    Controller previousController = null;

    bool controlling=false;
    Coroutine controllCoroutine;
    public void StartControll() {
        if (gameObject.activeInHierarchy && !controlling) {
            controlling = true;
            controllCoroutine = StartCoroutine(Controll());
        }
        if (dynamicPreviousControllerSetting) {
            previousController = controllerManager.previousController;
        }
    }
    public void StopControll() {
        StopCoroutine(controllCoroutine);
        controlling = false;
    }
    protected abstract IEnumerator Controll();
    protected void OnEnable() {
        if (controllerManager == null) {
            if (type == ControllerManagers.ControllerType.DEFAULT) {
                Debug.LogError("Controllerの派生クラスでtypeが正しく設定されていません");
            }
            if (!ControllerManagers.initiallized) ControllerManagers.Init();
            controllerManager = ControllerManagers.controllerManagerDict[type];
        }
        controllerManager.ChangeController(this);
    }
    protected void ChangeControllToPrevious() {
        if (previousController != null) {
            controllerManager.ChangeController(previousController);
        }
    }
}
