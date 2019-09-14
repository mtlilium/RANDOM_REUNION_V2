using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using UnityEngine;

public class ConversationControll : MonoBehaviour
{
    public List<PersonScript> PersonRegist;//ここにPersonScriptを追加すればAwake時登録される
    public Dictionary<string, ConversationClass> conversationIndex = new Dictionary<string, ConversationClass>();

    Dictionary<string, PersonScript> PersionIndex = new Dictionary<string, PersonScript>();

    void Awake(){        
        StaticConversationControll.controller = this;
        foreach (var x in PersonRegist)
        {
            PersionIndex.Add(x.PersonalName,x);
        }
        foreach (var x in PersonRegist)
        {
            var conv = x.GetComponents<ConversationClass>();
            foreach (var c in conv)
            {
                conversationIndex.Add(c.ConversationName, c);
            }
        }
    }
    public void Action(string conversationName)
    {
        try
        {
            if (!conversationIndex.ContainsKey(conversationName))
                throw new ArgumentException("その名前の会話は登録されていません");
            else
                conversationIndex[conversationName].Action();
        }
        catch(Exception e)
        {
            Debug.Log(e.Message);
            throw;
        }
    }
}

public static class StaticConversationControll
{
    public static ConversationControll controller;
    public static void Action(string conversationName)
    {
        controller.Action(conversationName);
    }
}