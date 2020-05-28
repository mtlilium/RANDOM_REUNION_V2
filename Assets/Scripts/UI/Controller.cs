using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Controller : MonoBehaviour{
    protected ControllerManager controllerManager;
    protected ControllerManagers.ControllerType type;

    [SerializeField]
    Controller previousController=null;

    Coroutine controllCoroutine;
    public void StartControll() {
        if (gameObject.activeInHierarchy) {
            controllCoroutine = StartCoroutine(Controll());
        }
    }
    public void StopControll() {
        StopCoroutine(controllCoroutine);
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
        //previousController = controllerManager.nowController;
        controllerManager.ChangeController(this);
    }
    protected void ChangeControllToPrevious() {
        if(previousController!=null) controllerManager.ChangeController(previousController); 
    }
}
