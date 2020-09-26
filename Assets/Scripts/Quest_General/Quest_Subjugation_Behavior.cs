using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Quest_Subjugation_Behavior : Quest_Behaviour{
    [System.Serializable]
    class StringInt:Serialize.KeyAndValue<string,int> {
        StringInt(string s, int x) : base(s, x) { }
    };
    [System.Serializable]
    class StringIntDict : Serialize.TableBase<string, int, StringInt> { };

    [SerializeField]
    StringIntDict serializedNorma=null;

    [SerializeField]
    string outerMapName = null;
    [SerializeField]
    string innerMapName = null;
    //List<string> innerMapNames = null    

    EnemyManager_Behaviour enemyManagerBehaviour=null;
    Dictionary<string, int> defeatedEnemyCount;
    override protected void Start(){
        base.Start();
        enemyManagerBehaviour = SystemClass.enemyManager;
        InitDefeatedAction();
        WhenQuestCleared = () => { Debug.Log("subujugation completed"); };
        if (!SystemClass.OuterMapDict.ContainsKey(outerMapName)) {
            Debug.Log(outerMapName+"という名前のOuterMapがSystemClassのOuterMapDictに見つかりません");
            return;
        }
        var innerMapDict = SystemClass.OuterMapDict[outerMapName].innerMapDict;
        if (!innerMapDict.ContainsKey(innerMapName)) {
            Debug.Log(innerMapName + "という名前のinnerMapが" + outerMapName + "のinnerMapDictに見つかりません");
        }
        var innerMap = innerMapDict[innerMapName];
        var spotManager = innerMap.GetComponent<SpawnSpotManager>();
        if (spotManager.SpotList == null) {
            Debug.Log(innerMap.name+"'s spotlist is null");
            return;
        }
        var norma = serializedNorma.GetTable();
        foreach (string name in norma.Keys) {
            enemyManagerBehaviour.EnemyGenerateRandomAtInnerMap(name, norma[name], innerMap);
        }
    }

    void InitDefeatedAction() {
        defeatedEnemyCount = new Dictionary<string, int>();
        var norma = serializedNorma.GetTable();
        foreach (string enemyName in norma.Keys) {
            defeatedEnemyCount.Add(enemyName, 0);
            Action func = () => { defeatedEnemyCount[enemyName]++; };
            if (enemyManagerBehaviour.WhenEnemyDefeated.ContainsKey(enemyName)) {
                enemyManagerBehaviour.WhenEnemyDefeated[enemyName] += func;
            }
            else {
                enemyManagerBehaviour.WhenEnemyDefeated.Add(enemyName, func);
            }
        }
    }

    override public bool AllNormaCleared() {
        bool allCleared = true;
        var norma = serializedNorma.GetTable();
        foreach(string name in norma.Keys) {
            if(defeatedEnemyCount[name] < norma[name]) {
                allCleared = false;
            }
        }
        return allCleared;
    }
}
