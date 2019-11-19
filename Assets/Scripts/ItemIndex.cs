using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

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

    public static TileBase GetChip(string itemname)
    {
        return Resources.Load<TileBase>("Data/MapChip/" + itemname);
    }

    static Dictionary<string, Item> itemTemplate = new Dictionary<string, Item>();

    public static KindOfItem GetKind(string itemname)
    {
        return itemTemplate[itemname].kind;
    }
}
