using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using UnityEngine;


public class PlayerOperateScript : PersonScript{
    public static GameObject Player { get; private set; }

    void Update ()
    {
        Player = gameObject;
        MapCoordinate c;
		if (IsArrowInput(out c)) {
			Move(c);
        }
        Action();
    }

    void Action()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            InteractionTrigger.enabled = true;
        else
            InteractionTrigger.enabled = false;
    }

    private bool IsArrowInput(out MapCoordinate c){
        c = new MapCoordinate(0, 0);
        var verticalAxis = Input.GetAxis("VerticalDigital");
        var horizontalAxis = Input.GetAxis("HorizontalDigital");
        //if (Input.GetKey(KeyCode.UpArrow))
        if(verticalAxis > 0.2f)
        {
            c += new MapCoordinate(1, 1);
        }
        //if (Input.GetKey(KeyCode.DownArrow))
        if(verticalAxis < -0.2f)
        {
            c -= new MapCoordinate(1, 1);
        }
        //if (Input.GetKey(KeyCode.LeftArrow))
        if(horizontalAxis < -0.2f)
        {
            c += new MapCoordinate(1, -1);
        }
        //if (Input.GetKey(KeyCode.RightArrow))
        if(horizontalAxis > 0.2f)
        {
            c -= new MapCoordinate(1, -1);
        }
        if (c != new MapCoordinate(0, 0))
            return true;
        else
            return false;
	}
}
