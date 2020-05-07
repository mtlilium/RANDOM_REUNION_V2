using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSpot : MonoBehaviour {
    private void OnDrawGizmos() {
        Gizmos.color = new Color(1.0f,1.0f,1.0f);
        Gizmos.DrawWireSphere(transform.position, transform.lossyScale.x);
    }
}
