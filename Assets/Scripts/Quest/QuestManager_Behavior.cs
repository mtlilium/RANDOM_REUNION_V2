using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* QuestManagerの初期化と、Fungusからクエスト受注を呼び出す,QuestオブジェクトInstantiate用
 */
public class QuestManager_Behavior : MonoBehaviour { 
    private void Start() {
        QuestManager.Init();
    }

    public void QuestAccept(string questName) {
        Quest_Behaviour toInstantiateQuest = QuestManager.QuestAccept(questName);
        if (toInstantiateQuest == null) return;
        Instantiate(toInstantiateQuest);
    }
}
