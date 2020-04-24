using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

enum QuestCategory
{
    Main,sub
}

public class Quest_Behaviour:MonoBehaviour
{
    QuestCategory questCategory;
    public string questName { get; private set; }
    string questTarget;
    int? deadline;
    string queriedBy;
    public Action<GameObject> WhenQuestAccepted { get; private set; }
    public Action<GameObject> WhenQuestCleared { get; private set; }
    public Action<GameObject> WhenQuestFailed { get; private set;}
}
