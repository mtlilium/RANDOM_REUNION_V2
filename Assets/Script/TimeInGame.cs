using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TimeInGame
{
    public static int Current { get; private set; }
    static SortedDictionary<int, Action<GameObject>> ActionTriggeredByTime = null; //時刻に応じて実行するActionリスト
    static SortedSet<int> RegistedExcuteTime=null;   //actionTriggeredByTimeに登録された時刻のセット

    static public GameObject player;
    public static void TimeUpdate()
    {
        Current++;
        ActionTriggeredByTime[TimeInGame.Current]?.Invoke(player);        
    }
    public static void InsertAction(int time,Action<GameObject> action)
    {
        if (ActionTriggeredByTime.ContainsKey(time))
        {
            ActionTriggeredByTime[time] += action;
        }
        else
        {
            ActionTriggeredByTime[time] = action;
        }
    }
}
