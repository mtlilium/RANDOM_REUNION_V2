using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using UnityEngine;

public class PersonScript : MovableObjectScript
{
    public string Name;
    public int PersonnelD;
}

public class ConversationClass : PersonScript
{
    [SerializeField]
    List<Saying> Contents = new List<Saying>();

    public void Talk(){
    Debug.Log(Contents[0].Who + " " + Contents[0].What);
    }
}

[System.SerializableAttribute]
public class Saying{//インスペクタから表示、編集する内容を示すクラス
	public int Who;
	public string What;
}
