using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Quest_Subjugation_Behavior : Quest_Behaviour
{
    [System.Serializable]
    class StringInt:Serialize.KeyAndValue<string,int> {
        StringInt(string s, int x) : base(s, x) { }
    };
    [System.Serializable]
    class StringIntDict : Serialize.TableBase<string, int, StringInt> { };

    [SerializeField]
    StringIntDict serializedNorma=null;

    EnemyManager_Behaviour enemyManager=null;
    Dictionary<string, int> norma;
    Dictionary<string, int> defeatedEnemyCount;
    override protected void Start(){
        base.Start();
        enemyManager = SystemClass.enemyManager;
        defeatedEnemyCount = new Dictionary<string, int>();
        norma = serializedNorma.GetTable();
        foreach (string enemyName in norma.Keys) {
            defeatedEnemyCount.Add(enemyName, 0);
            Action func = (() => { defeatedEnemyCount[enemyName]++; });
            if (enemyManager.WhenEnemyDefeated.ContainsKey(enemyName)) {
                enemyManager.WhenEnemyDefeated[enemyName] += func;
            }
            else {
                enemyManager.WhenEnemyDefeated.Add(enemyName, func);
            }
        }
        WhenQuestCleared = () => { Debug.Log("subujugation completed"); };
    }

    void Update(){
        if (AllNormaCleared()) {
            QuestManager.QuestClear(base.questName);
        }
    }
    bool AllNormaCleared() {
        bool allCleared = true;
        foreach(string name in norma.Keys) {
            if(defeatedEnemyCount[name] < norma[name]) {
                allCleared = false;
            }
        }
        return allCleared;
    }
}
