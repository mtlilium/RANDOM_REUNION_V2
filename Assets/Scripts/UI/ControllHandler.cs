using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class ControllHandler : MonoBehaviour {
    //protected ControllerManagers.ControllerType type;

    public bool staticPreviousControllerSetting;

    [ShowIf("staticPreviousControllerSetting")]
    [SerializeField]
    ControllHandler previousController = null;

    Controller controller;

    bool controlling = false;
    Coroutine controllCoroutine;
    private void Awake() {
        controller = GetComponent<Controller>();
    }

    public void StartControll() {
        if (gameObject.activeInHierarchy && !controlling) {
            controlling = true;
            controllCoroutine = StartCoroutine(controller.Controll());
        }
        if (!staticPreviousControllerSetting) {
            previousController = ControllHandlerManager.previousController;
        }
    }
    public void StopControll() {
        StopCoroutine(controllCoroutine);
        controlling = false;
    }
    
    protected void OnEnable() {
        ControllHandlerManager.ChangeController(this);
    }
    public void ChangeControllToPrevious() {
        if (previousController != null) {
            ControllHandlerManager.ChangeController(previousController);
        }
    }
}

