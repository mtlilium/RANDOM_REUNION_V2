using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest_Subjugation_Behavior : MonoBehaviour
{
    Quest_Subjugation quest;
    EnemyManager enemyManager;
    Dictionary<string, int> defeatedEnemyCount;
    void Start(){
        defeatedEnemyCount = new Dictionary<string, int>();
        foreach (string name in quest.enemyNames) {
            defeatedEnemyCount.Add(name, 0);
            enemyManager.WhenEnemyDefeated.Add(name,()=> { defeatedEnemyCount[name]++; });
        }
    }

    void Update(){
        if (AllNormaCleared()) {
            QuestManager.QuestClear(quest.questName);
            Destroy(this.gameObject);
        }
    }
    bool AllNormaCleared() {
        bool allCleared = true;
        foreach(string name in quest.enemyNames) {
            if(defeatedEnemyCount[name] < quest.norma[name]) {
                allCleared = false;
            }
        }
        return allCleared;
    }
}
