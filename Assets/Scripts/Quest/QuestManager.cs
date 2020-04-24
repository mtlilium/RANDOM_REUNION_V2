using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public static class QuestManager{
    public static Dictionary<string, Quest> OrderedQuest { get; private set; }    //クエスト名をキーとし,受注済みクエストを値とする辞書
    public static Dictionary<string, Quest> AcceptableQuest { get; private set; } //クエスト名をキーとし,受注可能クエストを値とする辞書
    public static Dictionary<string, bool> Flags { get; private set; }     //フラグを管理するクラス

    public static Dictionary<string, GameObject> stringToPrefab { get; private set; }   //クエスト名をキー都としQuestBehaviorをアタッチしたprefab」を値とする辞書
    public static void StringToPrefabInit() {
        string path = @"QuestPrefab/";
        var prefabs = Resources.LoadAll<GameObject>(path);
        foreach (var p in prefabs) {
            stringToPrefab[p.name] = p;
        }
    }//QuestManagerBehaviorのStartで呼び出される

    static public GameObject player;
    static public void QuestAccept(string questName)
    {
        if (!AcceptableQuest.ContainsKey(questName)) {
            Debug.LogError(questName + "が受注可能クエスト内に見つかりません");
            return;
        }
        Quest quest = AcceptableQuest[questName];
        quest.WhenQuestAccepted(player);
        AcceptableQuest.Remove(questName);

        OrderedQuest.Add(questName, quest);
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
