using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using UnityEngine;

public class ConversationClass : MonoBehaviour
{
    [SerializeField]
    public string ConversationName;
    [SerializeField]
    public bool OnlyOnce;
    public bool Running { get; private set; }
    public bool Finished { get; private set; }

    [SerializeField]
    List<Saying> Contents = new List<Saying>();//会話の内容

    public bool Talkable()
    {
        if (Running)
            return false;
        if (OnlyOnce && Finished)
            return false;
        return true;
    }

    public IEnumerator Talk() {//スペースキーが押されると会話が進む
        Running = true;
        Finished = false;
        foreach (var content in Contents) {
            var who = content.Who;
            var what = content.What;
            foreach (var statement in what)
            {
                Debug.Log($"{who} {statement}");
                yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
            }
        }
        Running = false;
        Finished = true;
        yield break;
    }

    public void Action()
    {
        if (!Talkable())
            return;
        else
            StartCoroutine(Talk());
    }
}

[System.SerializableAttribute]
class Saying{//インスペクタから表示、編集する内容を示すクラス
    public string Who = "";
    public List<string> What;

    public Saying(string who, params string[] statements)
    {
        Who = who;
        What = new List<string>() { Capacity = statements.Length };
        What.AddRange(statements);
    }    
}
