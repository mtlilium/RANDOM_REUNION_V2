using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioTest : MonoBehaviour
{


    // Update is called once per frame
    void Update()
    {

    }

    public int GetInstanceID(GameObject obj)
    {
        return obj.gameObject.GetInstanceID();
    }
}
