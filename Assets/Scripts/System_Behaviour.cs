using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class System_Behaviour : MonoBehaviour
{
    EnemyManager_Behaviour enemyManager;
    private void Awake() {
        SystemClass.randGen = new System.Random((int)System.DateTime.Now.Ticks);
    }
    void Start(){
        enemyManager = transform.Find("EnemyManager").GetComponent<EnemyManager_Behaviour>();
    }
    void Update(){
        
    }
}
