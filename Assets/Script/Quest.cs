using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

enum QuestCategory
{
    Main,sub
}
public class Quest
{
    QuestCategory questCategory;
    string questName;
    string questTarget;
    int? deadline;
    string queriedBy;

    public Action<GameObject> WhenQuestAccepted { get; private set; }
    public Action<GameObject> WhenQuestCleared { get; private set; }
    public Action<GameObject> WhenQuestFailed { get; private set;}
}
