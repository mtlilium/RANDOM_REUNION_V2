using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Player : MonoBehaviour
{
	Vector3 goal = new Vector3(30,201,-4);

    // Start is called before the first frame update
    void Start()
    {
		GetComponent<NavMeshAgent>().SetDestination(goal);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
