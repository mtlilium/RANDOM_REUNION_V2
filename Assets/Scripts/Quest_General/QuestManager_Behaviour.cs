using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* QuestManagerの初期化と、FungusからQuestManagerを参照する用
 */
public class QuestManager_Behaviour : MonoBehaviour {
    [SerializeField]
    Transform questHeaderParent = null; //QuestHeader(UI)をこのTransformの子として生成する　QuestManagerの初期化用
    [SerializeField]
    Transform questDetailParent = null; //QuestのDetail(UI)をこのTransformの子として生成する QuestDetailGeneratorの初期化用
    [SerializeField]
    DS.UI.Tab questTab = null;          //HeaderとDetailを動的生成した後にLinkTabHeaderを呼ぶため　QuestManagerの初期化用
    private void Start() {
        QuestManager.Init(questHeaderParent,questTab);
        QuestHeaderGenerator.Init(questHeaderParent);
        QuestDetailGenerator.Init(questDetailParent);
    }
    public void QuestAccept(string questName) {
        QuestManager.QuestAccept(questName);
    }
    public bool QuestNormaCleared(string questName) {
        return QuestManager.QuestNormaCleared(questName);
    }
    public void QuestClear(string questName) {
        QuestManager.QuestClear(questName);
    }
}
