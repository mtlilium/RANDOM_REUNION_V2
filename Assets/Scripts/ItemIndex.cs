using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum KindOfItem
{
    important,
    food
}

public static class ItemIndex
{
    public static Sprite GetSprite(string itemname)
    {
        return Resources.Load<Sprite>("Data/Sprite/" + itemname);
    }

    static Dictionary<string, Item> itemTemplate = new Dictionary<string, Item>();

    public static KindOfItem GetKind(string itemname)
    {
        return itemTemplate[itemname].kind;
    }
}
