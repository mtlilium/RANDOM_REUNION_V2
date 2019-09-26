using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class QuestManager {
    public static Dictionary<string, Quest> OrderedQuest { get; private set; }    //クエスト名をキーとし,受注済みクエストを値とする辞書
    public static Dictionary<string, Quest> AcceptableQuest { get; private set; } //クエスト名をキーとし,受注可能クエストを値とする辞書
    public static Dictionary<string, bool> Flags { get; private set; }     //フラグを管理するクラス

    static SortedDictionary<int, Action<GameObject> > ActionTriggeredByTime; //時刻に応じて実行するActionリスト

    static SortedSet<int> RegistedExcuteTime;   //actionTriggeredByTimeに登録された時刻のセット

    static GameObject player;
    static void QuestAccept(string questName)
    {
        AcceptableQuest[questName].WhenQuestAccepted(player);
    }
    static void QuestClear(string questName)
    {
        OrderedQuest[questName].WhenQuestCleared(player);
    }
    static void QuestFail(string questName)
    {
        OrderedQuest[questName].WhenQuestFailed(player);
    }

    public static void InsertAction(int time,Action<GameObject> action)
    {
        ActionTriggeredByTime[time]+=action;
    }
    public static void TimeUpdate()
    {
        ActionTriggeredByTime[TimeInGame.Current]?.Invoke(player);
    }
}
