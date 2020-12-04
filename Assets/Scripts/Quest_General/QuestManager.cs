using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using System.Linq;

public static class QuestManager{
    static DS.UI.Window questWindow;        //DetailのWindowAcrivatorのInactivateWindow(キャンセルしたときに消えるWindow)に設定する
    static DS.UI.Tab questTab;              //HeaderとDetailを動的生成した後にLinkTabHeaderを呼ぶため
   
    public static Dictionary<string, Quest_Behaviour> OrderedQuest {get; private set; }     //クエスト名をキーとし,受注済みクエストを値とする辞書
    public static Dictionary<string,Quest_Behaviour> ClearedQuest { get; private set; }     //クエスト名をキーとし,クリア済みクエストを値とする辞書
    public static void Init() {//QuestManagerBehaviorのStartで呼び出される
        OrderedQuest = new Dictionary<string, Quest_Behaviour>();
        ClearedQuest = new Dictionary<string, Quest_Behaviour>();
        QuestDatabase.Init();
    }
    static public void QuestAccept(string questName) {
        if (!QuestDatabase.AcceptableQuest.ContainsKey(questName)) {
            Debug.LogError(questName + "が受注可能クエスト内に見つかりません");
            return;
        }
        Quest_Behaviour quest = QuestDatabase.AcceptableQuest[questName];
        QuestDatabase.AcceptableQuest.Remove(questName);
        //quest.WhenQuestAccepted?.Invoke();
        var obj=GameObject.Instantiate(quest);
        obj.name = questName;//名前から(Clone)を除く
        OrderedQuest.Add(questName, obj);
        QuestMenu.AddNewQuestUI(quest.info);
    }    
    static public void QuestClear(string questName){
        if (OrderedQuest.ContainsKey(questName)) {
            OrderedQuest[questName].WhenQuestCleared?.Invoke();
            var quest = OrderedQuest[questName];
            
            OrderedQuest.Remove(questName);
            ClearedQuest.Add(questName, quest);

            GameObject.Destroy(quest);
            QuestMenu.EnCleared(quest.info.displayName);
        }
        else {
            Debug.LogError("受注済みクエストに" + questName + "がない状態でQuestClearが呼ばれました");
            return;
        }
    }
    static public void QuestFail(string questName){
        Debug.Log("QuestFailが未定義です");
        //OrderedQuest[questName].WhenQuestFailed();
        //OrderedQuest.Remove(questName);
    }
    static public bool QuestNormaCleared(string questName) {//指定したQuestのノルマが達成できてるかどうか
        if (!OrderedQuest.ContainsKey(questName)) {
            Debug.LogError(questName + "が受注済みクエスト内に見つかりません");
            return false;
        }
        return OrderedQuest[questName].AllNormaCleared();
    }
}

public static class QuestDatabase {
    public static GameObject questHeaderPrefab;
    public static GameObject questDetailPrefab;
    public static Dictionary<string, Quest_Behaviour> AcceptableQuest { get; private set; } //クエスト名をキーとし,受注可能クエストを値とする辞書
    public static Dictionary<string, Sprite> QuestHeaderSprite { get; private set; } //クエスト名をキーとし、メニューで表示されるヘッダーの画像を値とする辞書
    public static Dictionary<string, Sprite> QuestDetailSprite { get; private set; } //クエスト名をキーとし、メニューで表示される詳細の画像を値とする辞書
    
    public static void Init() {
        string path = @"Quest/UI/Prefabs/";
        questHeaderPrefab = Resources.Load<GameObject>(path + "questHeader");
        questDetailPrefab = Resources.Load<GameObject>(path + "questDetail");
        InitAcceptableQuest();
        InitQuestSprite();
    }

    static void InitAcceptableQuest() {
        AcceptableQuest = new Dictionary<string, Quest_Behaviour>();
        string path = @"Quest/IndividualPrefabs/";
        var prefabs = Resources.LoadAll<GameObject>(path);
        foreach (var p in prefabs) {
            AcceptableQuest[p.name] = p.GetComponent<Quest_Behaviour>();
        }
    }
    static void InitQuestSprite() {
        QuestHeaderSprite = new Dictionary<string, Sprite>();
        string path = @"Quest/UI/HeaderImages";
        var headerSprites = Resources.LoadAll<Sprite>(path);
        foreach (var sp in headerSprites) {
            QuestHeaderSprite.Add(sp.name, sp);
        }

        QuestDetailSprite = new Dictionary<string, Sprite>();
        path = @"Quest/UI/DetailImages";
        var detailSprites = Resources.LoadAll<Sprite>(path);
        foreach (var sp in detailSprites) {
            QuestDetailSprite.Add(sp.name, sp);
        }
    }
}