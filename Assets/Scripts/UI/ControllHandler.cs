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

    Controller[] controllers;

    bool controlling = false;
    List<Coroutine> controllCoroutines;
    private void Awake() {
        controllCoroutines = new List<Coroutine>();
        controllers = GetComponents<Controller>();
    }

    public void StartControll() {
        if (gameObject.activeInHierarchy && !controlling) {
            controlling = true;
            foreach (var controller in controllers) {
                controllCoroutines.Add( StartCoroutine(controller.Controll()) );
            }
        }
        if (!staticPreviousControllerSetting) {
            previousController = ControllHandlerManager.previousController;
        }
    }
    public void StopControll() {
        foreach (var coroutine in controllCoroutines) {
            StopCoroutine(coroutine);
        }
        controllCoroutines.Clear();
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
