using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ItemManager{
    static public ItemBag_Behaviour itemBag { get; private set; }
    public static void Init(ItemBag_Behaviour _itemBag) {
        itemBag = _itemBag;
        ItemDatabase.Load();
    }
    public static void Generate(string itemName) {
        GameObject itemPrafab = ItemDatabase.itemPrefabs[itemName];
        GameObject.Instantiate(itemPrafab);
    }
}

public static class ItemDatabase {
    public static GameObject itemHeaderPrafab { get; private set; }
    public static GameObject itemDetailPrefab { get; private set; }
    static public Dictionary<string, GameObject> itemPrefabs { get; private set; }

    static bool loaded = false;
    public static void Load() {
        if (loaded) {
            Debug.LogAssertion("ItemDatabase内のLoadが二回以上呼ばれました");
            return;
        }
        loaded = true;
        itemPrefabs = new Dictionary<string, GameObject>();
        string path = @"Item/IndividualPrefabs/";
        var prefabs = Resources.LoadAll<GameObject>(path);
        foreach(var p in prefabs) {
            itemPrefabs.Add(p.name, p);
        }
        LoadUIPrefab();
    }
    static void LoadUIPrefab() {
        string path = @"Item/UI/Prefabs/";
        itemHeaderPrafab = Resources.Load<GameObject>(path + "itemHeader");
        itemDetailPrefab = Resources.Load<GameObject>(path + "itemDetail");
    }
}
