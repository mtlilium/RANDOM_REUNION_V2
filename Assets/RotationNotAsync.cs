using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationNotAsync : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		gameObject.transform.rotation = Quaternion.Euler(90,0,0);
		Debug.Log(gameObject.transform.rotation);
    }
}
