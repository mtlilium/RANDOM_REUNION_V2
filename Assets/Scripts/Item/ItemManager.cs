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
    public static void Generate(string itemName, Transform parent, Vector2 position) {
        var itemObj = GameObject.Instantiate(ItemDatabase.itemPrefab, position, Quaternion.identity, parent);

        itemObj.name = itemName;

        SpriteRenderer renderer = itemObj.GetComponent<SpriteRenderer>();
        renderer.sprite = ItemDatabase.itemNameToSprites[itemName];

        Item_Behaviour itemBehaviour = itemObj.GetComponent<Item_Behaviour>();
        itemBehaviour.Init(itemName, ItemDatabase.itemNameToKinds[itemName]);
    }
}

public static class ItemDatabase {
    public static GameObject itemHeaderPrefab { get; private set; }
    public static GameObject itemDetailPrefab { get; private set; } 
    public static GameObject itemPrefab { get; private set; }

    static public Dictionary<string, Sprite> itemNameToSprites;
    static public Dictionary<string, KindOfItem> itemNameToKinds;

    static public bool loaded { get; private set; }
    public static void Load() {
        if (loaded) {
            //Debug.LogAssertion("ItemDatabase内のLoadが二回以上呼ばれました");
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
        itemPrefab = Resources.Load<GameObject>(path + "itemPrefab");
    }
    static void LoadSprites() {
        string path = @"Item/Sprites/";
        var sprites = Resources.LoadAll<Sprite>(path);
        itemNameToSprites = new Dictionary<string, Sprite>();
        foreach(var sp in sprites) {
            itemNameToSprites[sp.name] = sp;
        }
    }
    static void LoadKinds() {
        Dictionary<string, KindOfItem> dict = 
        new Dictionary<string, KindOfItem> {
            { "usable", KindOfItem.USABLE },
            { "unUsable",KindOfItem.UN_USABLE},
            { "forStory",KindOfItem.FOR_STORY}
        };

        itemNameToKinds = new Dictionary<string, KindOfItem>();

        string path = @"Item/TextDatas/";
        var textAsset = Resources.Load<TextAsset>(path + "kinds");
        char[] separator = { '\r', '\n' };
        string[] texts = textAsset.text.Split(separator, System.StringSplitOptions.RemoveEmptyEntries);

        foreach(var s in texts) {
            string[] nameAndKind = s.Split(' ');
            itemNameToKinds.Add(nameAndKind[0], dict[nameAndKind[1]]);
        }
    }
}
