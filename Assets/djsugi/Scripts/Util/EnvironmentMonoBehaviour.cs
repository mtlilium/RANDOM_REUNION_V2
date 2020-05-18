using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DS.Util
{
    /// <summary>
    /// ヒエラルキーに１つだけのコンポーネント
    /// </summary>
    /// <typeparam name="T">継承先</typeparam>
    public abstract class EnvironmentMonoBehaviour<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T instance;

        public static T Instance
        {
            get
            {
                if (instance == null)
                {
                    Type t = typeof(T);

                    instance = (T)FindObjectOfType(t);
                    if (instance == null)
                    {
                        Debug.Log(t + " を生成しました");
                        var environment = new GameObject(typeof(T).FullName);
                        environment.AddComponent<T>();

                        instance = environment.GetComponent<T>();
                    }
                }

                return instance;
            }
        }

        virtual protected void Awake()
        {
            if (this != Instance)
            {
                Destroy(gameObject);
                Debug.LogWarning(
                    typeof(T) +
                    " は既に他のGameObjectにアタッチされているため、コンポーネントを破棄しました." +
                    " アタッチされているGameObjectは " + Instance.gameObject.name + " です.");
                return;
            }

            DontDestroyOnLoad(gameObject);
        }

    }
}
