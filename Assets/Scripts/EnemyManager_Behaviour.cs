using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyManager_Behaviour : MonoBehaviour
{
    public Dictionary<string, Action> WhenEnemyDefeated { get; private set; }//Enemy側から呼び出す
    public Dictionary<string, GameObject> enemyNameToPrefab { get; private set; }//
    // Start is called before the first frame update
    private void Awake() {//Startより前にやっておきたい
        SystemClass.enemyManager = this;
    }
    void Start(){
        WhenEnemyDefeated = new Dictionary<string, Action>();
        enemyNameToPrefab = new Dictionary<string, GameObject>();
        var prefabs = Resources.LoadAll<GameObject>("EnemyPrefab/");
        foreach (GameObject prefab in prefabs) {
            enemyNameToPrefab[prefab.name] = prefab;
        }
    }

    public void EnemyGenerateRandomAtInnerMap(string enemyName,int amount,InnerMap_Behaviour innerMap) {
        if (!enemyNameToPrefab.ContainsKey(enemyName)) {
            Debug.LogError(enemyName+"という名前のEnemyが見つかりません");
            return;
        }
        var prefab = enemyNameToPrefab[enemyName];
        for (int i = 0; i < amount; i++) {
            ObjectGenerator.GenerateRandomAtInnerMap(prefab, innerMap);
        }
    }

    void Update(){
    }
}
