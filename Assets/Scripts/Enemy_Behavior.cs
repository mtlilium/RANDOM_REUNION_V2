using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Behavior : MonoBehaviour {

    EnemyManager_Behaviour enemyManager=null;
    new string name;
    StatusScript status;
    PalameterScript palameter;
    void Start(){
        RaycastTrackerScript tracker = GetComponent<RaycastTrackerScript>();
        GameObject player = GameObject.FindGameObjectWithTag("Player");//SystemClassにPlayerの参照を持たせておいてそこから持ってくるようにするかも check
        tracker.InitDestination(player.transform);
        enemyManager = SystemClass.enemyManager;
        name = gameObject.name;//GameObjectと別にする場合は要変更
        status = GetComponent<StatusScript>();
        palameter = GetComponent<PalameterScript>();
    }
    void Update(){
        if (palameter.IsDefeat()) {
            if (enemyManager.WhenEnemyDefeated.ContainsKey(name)) {
                enemyManager.WhenEnemyDefeated[name]?.Invoke();
            }
            Destroy(this.gameObject);
        }
    }
}
