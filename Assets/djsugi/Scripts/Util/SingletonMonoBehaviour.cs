﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DS.Util
{
    /// <summary>
    /// シーンに１つだけのコンポーネント
    /// </summary>
    /// <typeparam name="T">継承先</typeparam>
    public abstract class SingletonMonoBehaviour<T> : MonoBehaviour where T : MonoBehaviour
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
                        Debug.LogError(t + " をアタッチしているGameObjectはありません");
                    }
                }

                return instance;
            }
        }

        virtual protected void Awake()
        {
            if (this != Instance)
            {
                Destroy(this);
                Debug.LogError(
                    typeof(T) +
                    " は既に他のGameObjectにアタッチされているため、コンポーネントを破棄しました." +
                    " アタッチされているGameObjectは " + Instance.gameObject.name + " です.");
                return;
            }
        }

    }
}
