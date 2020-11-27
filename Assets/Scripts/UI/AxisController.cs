using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AxisController : Controller{
    [SerializeField]
    protected string axisName=null;//GetAxisRawに渡す引数

    private void OnDisable() {
    //    if(handler!=null) handler.ChangeControllToPrevious();
    }

    [SerializeField]
    public UnityEngine.Events.UnityEvent upAxisEvent=null, downAxisEvent=null;
        
    override public IEnumerator Controll() {
        yield return new WaitForSecondsRealtime(0.2f);//別のAxisControllerからEnableされた時に一度のGetButtonDownで連続して反応するのを防ぐ
        while (true) {
            var axis = Input.GetAxisRaw(axisName);
            if (axis > 0.2f) {
                upAxisEvent?.Invoke();
                yield return new WaitForSecondsRealtime(0.2f);
            }
            if (axis < -0.2f) {
                downAxisEvent?.Invoke();
                yield return new WaitForSecondsRealtime(0.2f);
            }
            yield return null;
        }
    }
}
