using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using UnityEngine;

public class ConversationControll : MonoBehaviour
{
    public List<ConversationClass> Conversations;//会話の登録
    public List<PersonScript> PersonRegist;//ここにPersonScriptを追加すればAwake時登録される

    private Dictionary<int, string> IDToName = new Dictionary<int, string>();//IDと名前の対応

    void Awake(){
    for(int i = 0;i < PersonRegist.Count;i++){
	    IDToName.Add(PersonRegist[i].PersonnelD, PersonRegist[i].Name);
        }
    }
}
