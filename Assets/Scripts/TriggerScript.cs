using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum ActivationMode { PlayerInteraction, PlayerPassing }

public class TriggerScript : MonoBehaviour
{
    public Action ActionsByPlayerPassing;
    public Action ActionsByPlayerInteraction;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (Input.GetKeyDown(KeyCode.Space))
                ActionsByPlayerInteraction();
            ActionsByPlayerInteraction();
        }
    }
    void OnCollisionEnter(Collision other)
    {
        if (other.collider.tag == "Player")
        {
            if (Input.GetKeyDown(KeyCode.Space))
                ActionsByPlayerInteraction();
            ActionsByPlayerInteraction();
        }
    }
}