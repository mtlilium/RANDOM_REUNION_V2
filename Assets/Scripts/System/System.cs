using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SystemClass{
    //EnemyManagerのAwakeで初期化
    public static EnemyManager_Behaviour enemyManager;
    //System_BehaviorのAwakeで初期化
    public static System.Random randGen;
    public static Dictionary<string, OuterMap_Behaviour> OuterMapDict { get; private set; }
    public static void InitOuterMapDict() {
        OuterMapDict = new Dictionary<string, OuterMap_Behaviour>();
    }
}
