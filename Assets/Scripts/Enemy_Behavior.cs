using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Behavior : MonoBehaviour {
    [SerializeField]
    EnemyManager enemyManager;

    new string name;
    StatusScript status;
    PalameterScript palameter;
    void Start(){
        name = gameObject.name;//GameObjectと別にする場合は要変更
        status = GetComponent<StatusScript>();
    }
    void Update(){
        if (palameter.IsDefeat()) {
            enemyManager.WhenEnemyDefeated[name]();
        }
    }
}
