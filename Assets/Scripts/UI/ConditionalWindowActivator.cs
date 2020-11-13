using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConditionalWindowActivator : WindowActivator{

    [HideInInspector]
    public bool canOpen,canClose;

    public override void Open() {
        if (canOpen) {
            base.Open();
        }
    }
    public override void Close() {
        if (canClose) {
            base.Close();
        }
    }
}
