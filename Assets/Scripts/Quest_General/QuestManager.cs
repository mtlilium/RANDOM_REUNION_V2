using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public static class QuestManager{
    public static Dictionary<string, Quest_Behaviour> OrderedQuest {get; private set; }    //クエスト名をキーとし,受注済みクエストを値とする辞書
    public static Dictionary<string, Quest_Behaviour> AcceptableQuest { get; private set; } //クエスト名をキーとし,受注可能クエストを値とする辞書
    public static Dictionary<string, bool> Flags { get; private set; }     //フラグを管理するクラス

    public static void Init() {
        OrderedQuest = new Dictionary<string, Quest_Behaviour>();
        InitAcceptableQuest();
    }
    static void InitAcceptableQuest() {//QuestManagerBehaviorのStartで呼び出される
        AcceptableQuest = new Dictionary<string, Quest_Behaviour>();
        string path = @"QuestPrefab/";
        var prefabs = Resources.LoadAll<GameObject>(path);
        foreach (var p in prefabs) {
            AcceptableQuest[p.name] = p.GetComponent<Quest_Behaviour>();
        }
    }

    static public void QuestAccept(string questName)
    {
        if (!AcceptableQuest.ContainsKey(questName)) {
            Debug.LogError(questName + "が受注可能クエスト内に見つかりません");
            return;
        }
        Quest_Behaviour quest = AcceptableQuest[questName];
        //quest.WhenQuestAccepted(player);
        AcceptableQuest.Remove(questName);
        var obj=GameObject.Instantiate(quest);
        obj.name = questName;
        OrderedQuest.Add(questName, obj);
    }
    static public void QuestClear(string questName)
    {
        if (OrderedQuest.ContainsKey(questName)) {
            OrderedQuest[questName].WhenQuestCleared?.Invoke();
            GameObject.Destroy(OrderedQuest[questName]);
            OrderedQuest.Remove(questName);
        }
    }
    static public void QuestFail(string questName)
    {
        OrderedQuest[questName].WhenQuestFailed();
        OrderedQuest.Remove(questName);
    }
    static public bool QuestNormaCleared(string questName) {//指定したQuestのノルマが達成できてるかどうか
        if (!OrderedQuest.ContainsKey(questName)) {
            Debug.LogError(questName + "が受注済みクエスト内に見つかりません");
            return false;
        }
        return OrderedQuest[questName].AllNormaCleared();
    }
}
