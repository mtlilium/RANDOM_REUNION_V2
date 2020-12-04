using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioTest : MonoBehaviour
{


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            AudioManager.SingletonInstance.RequestPlaySE(new Vector3(2, 2, 0), "かっ勘違いしないでよねっ!", GetInstanceID(this.gameObject) ,0.0f);
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            //amt.RequestPlayBGM("hamu_chiptune", 0.0f);
            AudioManager.SingletonInstance.RequestPlayBGM("ランダ村", GetInstanceID(this.gameObject) + 100, 0.0f);
        }
    }

    public int GetInstanceID(GameObject obj)
    {
        return obj.gameObject.GetInstanceID();
    }
}
