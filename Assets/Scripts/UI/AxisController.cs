using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AxisController : Controller{
    [SerializeField]
    protected string axisName=null;//GetAxisRawに渡す引数
    private void Awake() {
        switch (axisName) {
            case "VerticalDigital":
                type = ControllerManagers.ControllerType.VERTICAL_TAB;
                break;
            case "HorizontalDigital":
                type = ControllerManagers.ControllerType.HORIZONTAL_TAB;
                break;
        }
    }
    //DS.UI.Tab tab;
    private void Start() {
        //tab = GetComponent<DS.UI.Tab>();
        if (axisName == null) {
            Debug.LogError(gameObject.name + "のAxisTabControllerで、axisNameが未設定です");
        }
    }
    private void OnDisable() {
        ChangeControllToPrevious();
    }

    [SerializeField]
    UnityEngine.Events.UnityEvent upAxisEvent,downAxisEvent;

    
    override protected IEnumerator Controll() {
        while (true) {
            var axis = Input.GetAxisRaw(axisName);
            if (axis > 0.2f) {
                upAxisEvent?.Invoke();
                //tab.PreviousTab();
                yield return new WaitForSecondsRealtime(0.2f);
            }
            if (axis < -0.2f) {
                downAxisEvent?.Invoke();
                //tab.NextTab();
                yield return new WaitForSecondsRealtime(0.2f);
            }
            yield return null;
        }
    }
}
