using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class QuestManager{
    
    public static Dictionary<string, Quest> OrderedQuest { get; private set; }    //クエスト名をキーとし,受注済みクエストを値とする辞書
    public static Dictionary<string, Quest> AcceptableQuest { get; private set; } //クエスト名をキーとし,受注可能クエストを値とする辞書
    public static Dictionary<string, bool> Flags { get; private set; }     //フラグを管理するクラス

    static public GameObject player;
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
}
