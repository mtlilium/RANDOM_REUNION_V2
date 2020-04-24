using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest_Subjugation_Behavior : Quest_Behaviour
{
    [System.Serializable]
    class StringInt:Serialize.KeyAndValue<string,int> {
        StringInt(string s, int x) : base(s, x) { }
    };
    [System.Serializable]
    class StringIntDict : Serialize.TableBase<string, int, StringInt> { };

    [SerializeField]
    StringIntDict serializedNorma;

    EnemyManager_Behaviour enemyManager=null;
    Dictionary<string, int> norma;
    Dictionary<string, int> defeatedEnemyCount;
    void Start(){
        enemyManager = SystemClass.enemyManager;
        defeatedEnemyCount = new Dictionary<string, int>();
        norma = serializedNorma.GetTable();
        foreach (string enemyName in norma.Keys) {
            defeatedEnemyCount.Add(enemyName, 0);
            enemyManager.WhenEnemyDefeated.Add(enemyName, ()=> { defeatedEnemyCount[enemyName]++; });
        }
    }

    void Update(){
        if (AllNormaCleared()) {
            QuestManager.QuestClear(base.questName);
            Destroy(this.gameObject);
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
