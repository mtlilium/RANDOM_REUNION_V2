using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class VerticalTabController : Controller{
    private void Awake() {
        type = ControllerManagers.ControllerType.VERTICAL_TAB;
    }
    DS.UI.Tab tab;
    private void Start() {
        tab = GetComponent<DS.UI.Tab>();
    }
    private void OnDisable() {
        ChangeControllToPrevious();
    }
    override protected IEnumerator Controll() {
        while (true) {
            var axis = Input.GetAxisRaw("VerticalDigital");
            if (axis > 0.2f) {
                tab.PreviousTab();
                yield return new WaitForSecondsRealtime(0.2f);
            }
            if (axis < -0.2f) {
                tab.NextTab();
                yield return new WaitForSecondsRealtime(0.2f);
            }
            yield return null;
        }
    }
}
