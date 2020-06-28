using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using System.Linq;

public static class QuestManager{
    static private QuestDatabase database = null;
    static Transform headerParentTransform; //QuestのHeader(UI)をこのTransformの子として生成する
    static Transform detailParentTransform; //QuestのDetail(UI)をこのTransformの子として生成する
    static DS.UI.Window questWindow;        //DetailのWindowAcrivatorのInactivateWindow(キャンセルしたときに消えるWindow)に設定する
    static DS.UI.Tab questTab;              //HeaderとDetailを動的生成した後にLinkTabHeaderを呼ぶため
    const int headerOffset = 20;
    const int headerPosX = 280;
    public static Dictionary<string, Quest_Behaviour> OrderedQuest {get; private set; }     //クエスト名をキーとし,受注済みクエストを値とする辞書
    static Dictionary<string, GameObject> nowValidHeader;
    static Dictionary<string, GameObject> nowValidDetail;
    public static Dictionary<string,Quest_Behaviour> ClearedQuest { get; private set; }     //クエスト名をキーとし,クリア済みクエストを値とする辞書
    public static void Init(Transform headerParent,Transform detailParent,DS.UI.Tab tab) {//QuestManagerBehaviorのStartで呼び出される
        headerParentTransform = headerParent;
        detailParentTransform = detailParent;
        questTab = tab;
        OrderedQuest = new Dictionary<string, Quest_Behaviour>();
        nowValidHeader = new Dictionary<string, GameObject>();
        nowValidDetail = new Dictionary<string, GameObject>();
        database = new QuestDatabase();
    }
    static public void QuestAccept(string questName){
        if (!database.AcceptableQuest.ContainsKey(questName)) {
            Debug.LogError(questName + "が受注可能クエスト内に見つかりません");
            return;
        }
        Quest_Behaviour quest = database.AcceptableQuest[questName];
        database.AcceptableQuest.Remove(questName);
        //quest.WhenQuestAccepted?.Invoke();
        var obj=GameObject.Instantiate(quest);
        obj.name = questName;//名前から(Clone)を除く

        OrderedQuest.Add(questName, obj);
        InstantiateHeader(questName);
        InstatntiateDetail(questName);
        questTab.LinkTabHeader();
    }
    static void InstantiateHeader(string questName) {
        var headerPrefab = GameObject.Instantiate(database.questHeaderPrefab, headerParentTransform);
        headerPrefab.GetComponent<Image>().sprite = database.QuestHeaderSprite[questName];
        var rect = headerPrefab.GetComponent<RectTransform>();
        var array = nowValidHeader.ToArray();
        float topPosY=-120;
        if (array.Length > 0) {
            topPosY = array[0].Value.GetComponent<RectTransform>().sizeDelta.y;
        }
        rect.anchoredPosition = new Vector2(headerPosX, topPosY + (rect.sizeDelta.y + headerOffset) );
        nowValidHeader.Add(questName, headerPrefab);
    } 
    static void InstatntiateDetail(string questName) {
        var detailObj = GameObject.Instantiate(database.questDetailPrefab, detailParentTransform);
        detailObj.GetComponent<Image>().sprite = database.QuestDetailSprite[questName];
        nowValidDetail.Add(questName, detailObj);
    }
    static public void QuestClear(string questName){
        if (OrderedQuest.ContainsKey(questName)) {
            OrderedQuest[questName].WhenQuestCleared?.Invoke();
            var quest = OrderedQuest[questName];
            GameObject.Destroy(quest);
            OrderedQuest.Remove(questName);

            ClearedQuest.Add(questName, quest);
            nowValidHeader[questName].GetComponent<Image>().color = Color.black;
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

public class QuestDatabase {
    public GameObject questHeaderPrefab;
    public GameObject questDetailPrefab;
    public Dictionary<string, Quest_Behaviour> AcceptableQuest { get; private set; } //クエスト名をキーとし,受注可能クエストを値とする辞書
    public Dictionary<string, Sprite> QuestHeaderSprite { get; private set; } //クエスト名をキーとし、メニューで表示されるヘッダーの画像を値とする辞書
    public Dictionary<string, Sprite> QuestDetailSprite { get; private set; } //クエスト名をキーとし、メニューで表示される詳細の画像を値とする辞書
    public Dictionary<string, Detail> QuestDetail { get; private set; }

    //////// staticquestdata.json読み込み用 //////////////////////////
    [Serializable]
    public struct Detail {
        public string name;
        public string target;
        public string place;
        public int timeLimit;
        public string detail;
    }
    [Serializable]
    public class StaticQuestData { public List<Detail> dataList; }
    /// /////////////////////////////////////////////////////////////
    
    public QuestDatabase() {
        string path = @"Quest/UI/Prefabs/";
        questHeaderPrefab = Resources.Load<GameObject>(path + "questHeader");
        questDetailPrefab = Resources.Load<GameObject>(path + "questDetail");
        InitAcceptableQuest();
        InitQuestSprite();
        InitQuestDetail();
    }

    void InitAcceptableQuest() {
        AcceptableQuest = new Dictionary<string, Quest_Behaviour>();
        string path = @"Quest/IndividualPrefabs/";
        var prefabs = Resources.LoadAll<GameObject>(path);
        foreach (var p in prefabs) {
            AcceptableQuest[p.name] = p.GetComponent<Quest_Behaviour>();
        }
    }
    void InitQuestSprite() {
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
    void InitQuestDetail() {
        var text = Resources.Load<TextAsset>(@"Quest/staticQuestData").text;
        var list = JsonUtility.FromJson<StaticQuestData>(text).dataList;
        QuestDetail = new Dictionary<string, Detail>();
        foreach (Detail x in list) {
            QuestDetail.Add(x.name, x);
        }
    }
}