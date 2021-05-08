using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Controller :MonoBehaviour{
    protected ControllHandler handler;
    private void Awake() {
        handler = GetComponent<ControllHandler>();
        if (handler == null) {
            handler = gameObject.AddComponent<ControllHandler>();
        }
    }
    public abstract IEnumerator Controll();
}