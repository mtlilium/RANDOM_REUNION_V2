using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public static class ItemManager{
    static public ItemBag_Behaviour itemBag { get; private set; }
    public static void Init(ItemBag_Behaviour _itemBag) {
        itemBag = _itemBag;
        ItemDatabase.Load();
    }
    public static void Generate(string itemName) {
        //GameObject itemPrafab = ItemDatabase.itemPrefabs[itemName];
        var itemPrefab = ItemDatabase.itemPrefab;
        SpriteRenderer renderer = itemPrefab.GetComponent<SpriteRenderer>();
        GameObject.Instantiate(itemPrefab);
    }
}

public static class ItemDatabase {
    public static GameObject itemHeaderPrefab { get; private set; }
    public static GameObject itemDetailPrefab { get; private set; } 
    public static GameObject itemPrefab { get; private set; }
    //static public Dictionary<string, GameObject> itemPrefabs { get; private set; }

    static public Dictionary<string, Sprite> itemNameToSprites;
    static public Dictionary<string, KindOfItem> itemNameToKinds;

    static bool loaded = false;
    public static void Load() {
        if (loaded) {
            Debug.LogAssertion("ItemDatabase内のLoadが二回以上呼ばれました");
            return;
        }
        loaded = true;
        LoadPrefabs();
        LoadSprites();
        LoadKinds();
    }
    static void LoadPrefabs() {
        string path = @"Item/Prefabs/";
        itemHeaderPrefab = Resources.Load<GameObject>(path + "UI/itemHeader");
        itemDetailPrefab = Resources.Load<GameObject>(path + "UI/itemDetail");
        itemPrefab = Resources.Load<GameObject>(path + "item");
    }
    static void LoadSprites() {
        string path = @"Item/Sprites/";
        var sprites = Resources.LoadAll<Sprite>(path);
        itemNameToSprites = new Dictionary<string, Sprite>();
        foreach(var s in sprites) {
            itemNameToSprites[s.name] = s;
        }
    }
    static void LoadKinds() {
        string path = @"Item/TextDatas/";
        var textAsset=Resources.Load<TextAsset>(path+"kinds");
        Dictionary<string, KindOfItem> dict = 
        new Dictionary<string, KindOfItem> {
            { "usable", KindOfItem.USABLE },
            { "unUsable",KindOfItem.UN_USABLE},
            { "forStory",KindOfItem.FOR_STORY}
        };
        itemNameToKinds = new Dictionary<string, KindOfItem>();
        using (StringReader reader = new StringReader(textAsset.text)) {
            string[] texts = reader.ReadLine().Split(' ');
            Debug.Log(texts[0]+","+texts[1]+",");
            itemNameToKinds.Add(texts[0],dict[texts[1]]);
        }
    }
}
