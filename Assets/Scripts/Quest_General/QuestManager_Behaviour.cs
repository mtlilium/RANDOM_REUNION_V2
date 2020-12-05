using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* QuestManagerの初期化と、FungusからQuestManagerを参照する用
 */
public class QuestManager_Behaviour : MonoBehaviour {
    
    public void QuestAccept(string questName) {
        QuestManager.QuestAccept(questName);
    }
    public bool QuestNormaCleared(string questName) {
        return QuestManager.QuestNormaCleared(questName);
    }
    public void QuestClear(string questName) {
        QuestManager.QuestClear(questName);
    }
    private void Start() {
        QuestManager.Init();
    }
}
