using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* QuestManagerの初期化と、Fungusからクエスト受注を呼び出す,QuestオブジェクトInstantiate用
 */
public class QuestManager_Behaviour : MonoBehaviour { 
    private void Start() {
        QuestManager.Init();
    }

    public void QuestAccept(string questName) {
        QuestManager.QuestAccept(questName);        
    }
}
