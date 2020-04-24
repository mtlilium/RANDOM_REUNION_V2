using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* QuestManagerの初期化と、Fungusからクエスト受注を呼び出すためのクラス
 */
public class QuestManager_Behavior : MonoBehaviour { 
    private void Start() {
        QuestManager.StringToPrefabInit();
    }

    public void QuestAccept(string questName) {
        QuestManager.QuestAccept(questName);
    }
}
