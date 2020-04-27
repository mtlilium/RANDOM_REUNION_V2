using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyManager_Behaviour : MonoBehaviour
{
    public Dictionary<string, Action> WhenEnemyDefeated { get; private set; }//Enemy側から呼び出す
    // Start is called before the first frame update
    private void Awake() {//Startより前にやっておきたい
        SystemClass.enemyManager = this;
    }
    void Start(){
        WhenEnemyDefeated = new Dictionary<string, Action>();
        WhenEnemyDefeated.Add("Mouse", () => { Debug.Log("mouse defeat"); });
    }

    public void EnemyGenerate() {

    }

    void Update(){
    }
}
