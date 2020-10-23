using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxisTabController : AxisController{
    DS.UI.Tab tab;
    private void Start() {
        tab = GetComponent<DS.UI.Tab>();
        if (tab == null) {
            Debug.LogError("AxisTabControllerがアタッチされた"+gameObject.name+"にTabが見つかりません");
        }
    }
    
    protected override IEnumerator Controll() {
        while (true) {
            var axis = Input.GetAxisRaw(base.axisName);
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
