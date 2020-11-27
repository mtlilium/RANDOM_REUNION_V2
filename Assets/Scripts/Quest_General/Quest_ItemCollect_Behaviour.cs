using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest_ItemCollect_Behaviour : Quest_Behaviour{
    [System.Serializable]
    class StringInt : Serialize.KeyAndValue<string, int> {
        StringInt(string s, int x) : base(s, x) { }
    };
    [System.Serializable]
    class StringIntDict : Serialize.TableBase<string, int, StringInt> { };

    [SerializeField]
    StringIntDict serializedNorma = null;

    ItemBag_Behaviour itemBag;

    protected override void Start() {
        base.Start();
        itemBag = ItemManager.itemBag;
        
        WhenQuestCleared += () => {
            var norma = serializedNorma.GetTable();
            foreach (string name in norma.Keys) {
                itemBag.DeleteItem(name,norma[name]);
            }
        };
        
    }
    public override bool AllNormaCleared() {
        var nameToHavingItems=itemBag.NameToHavingItems;
        var norma = serializedNorma.GetTable();
        foreach(string name in norma.Keys) {
            if (!nameToHavingItems.ContainsKey(name)) return false;
             if(nameToHavingItems[name].Count < norma[name]) {
                return false;
             }
        }
        return true;
    }
}