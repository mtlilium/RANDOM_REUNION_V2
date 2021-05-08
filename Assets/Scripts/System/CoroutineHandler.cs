using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoroutineHandler : MonoBehaviour{
    static protected CoroutineHandler m_Instance;
    /*　初回参照があった時に「CoroutineHandler」という名称でgameObjectを生成
     * 
     */
    static public CoroutineHandler instance {
        get {
            if (m_Instance == null) {
                GameObject o = new GameObject("CoroutineHandler");
                DontDestroyOnLoad(o);
                m_Instance = o.AddComponent<CoroutineHandler>();
            }
            return m_Instance;
        }
    }
    static public Coroutine StaticStartCoroutine(IEnumerator coroutine) {
        return instance.StartCoroutine(coroutine);
    }
    static public void StaticStopCoroutine(Coroutine coroutine) {
        instance.StopCoroutine(coroutine);
    }
    public void OnDisable() {
        if (m_Instance)
            Destroy(m_Instance.gameObject);
    }
}
