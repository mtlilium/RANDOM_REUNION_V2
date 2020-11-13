using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowActivator : Controller{
    [SerializeField]
    DS.UI.Window activateWindow=null;
    [SerializeField]
    DS.UI.Window inactivateWindow = null;

    private void OnEnable () {
        //base.OnEnable();
        activateWindow?.gameObject.SetActive(false);
    }

    override public IEnumerator Controll() {
        yield return new WaitForSecondsRealtime(0.2f);//別のActivatorでこのActivatorがEnableされた時に一度のGetButtonDownで連続して反応するのを防ぐ
        while (true) {
            if (Input.GetButtonDown("Submit")) {
                Open();
            }
            if (Input.GetButtonDown("Cancel")) {
                Close();
            }
            yield return null;
        }
    }
    public virtual void Open() {
        activateWindow?.Open();
    }
    public virtual void Close() {
        inactivateWindow?.Close();
        handler.ChangeControllToPrevious();
    }
}
