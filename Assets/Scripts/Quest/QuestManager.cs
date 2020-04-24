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
            Debug.Log(p.name + " add");
        }
    }

    static public GameObject player;
    static public Quest_Behaviour QuestAccept(string questName)
    {
        if (!AcceptableQuest.ContainsKey(questName)) {
            Debug.LogError(questName + "が受注可能クエスト内に見つかりません");
            return null;
        }
        Quest_Behaviour quest = AcceptableQuest[questName];
        //quest.WhenQuestAccepted(player);
        AcceptableQuest.Remove(questName);
        OrderedQuest.Add(questName, quest);
        Debug.Log(questName + "が受注されました！やったね！！");
        return quest;
    }
    static public void QuestClear(string questName)
    {
        OrderedQuest[questName].WhenQuestCleared(player);
        OrderedQuest.Remove(questName);
    }
    static public void QuestFail(string questName)
    {
        OrderedQuest[questName].WhenQuestFailed(player);
        OrderedQuest.Remove(questName);
    }
}
