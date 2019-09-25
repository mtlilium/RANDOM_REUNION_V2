using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class QuestManager {
    public static Dictionary<string, Quest> OrderedQuest { get; private set; }    //クエスト名をキーとし,受注済みクエストを値とする辞書
    public static Dictionary<string, Quest> AcceptableQuest { get; private set; } //クエスト名をキーとし,受注可能クエストを値とする辞書
    public static Dictionary<string, bool> Flags { get; set; }     //フラグを管理するクラス

    static SortedDictionary<int, List<Action<GameObject>> > ActionTriggeredByTime; //時刻に応じて実行するActionリスト

    static SortedSet<int> RegistedExcuteTime;   //actionTriggeredByTimeに登録された時刻のセット

    static void QuestAccept(string questName)
    {
        AcceptableQuest[questName].WhenQuestAccepted(/*ここにプレイヤーが入る*/);
    }
    static void QuestClear(string questName)
    {
        OrderedQuest[questName].WhenQuestCleared();
    }
    static void QuestFail(string questName)
    {
        OrderedQuest[questName].WhenQuestFailed();
    }

    public static void InsertAction(int time,Action<GameObject> action)
    {
        ActionTriggeredByTime[time].Add(action);
    }
    
}
