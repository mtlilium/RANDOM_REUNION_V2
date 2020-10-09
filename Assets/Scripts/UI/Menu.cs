using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class Menu{
    static DS.UI.Window menuWindow;
    public static void Init(DS.UI.Window _menuWindow) {
        menuWindow = _menuWindow;
    }

    public static void Toggle() {
        menuWindow.Toggle();
    }
    public static IEnumerator OpenAsItemSelect(Item_Behaviour item, string itemName) {
        menuWindow.Open();
        while (true) {

            yield return new WaitForSecondsRealtime(0.2f);
        }
    }
}

public static class ItemMenu { 
    public static Dictionary<Item_Behaviour, (GameObject header, GameObject detail)> ItemToUI { get; private set; }

    [System.Serializable]
    public struct UIs {
        public DS.UI.Tab tab;
        public Transform tabHeaders;
        public Transform contentContainer;
    }
    static UIs usable;
    static UIs unUsable;
    static UIs forStory;
    public static void Init(UIs _usable, UIs _unUsable, UIs _forStory) {
        ItemToUI = new Dictionary<Item_Behaviour, (GameObject header, GameObject detail)>();
        usable = _usable;
        unUsable = _unUsable;
        forStory = _forStory;
    }
    public static void AddNewItemUI(Item_Behaviour newItem) {
        var dict = new Dictionary<KindOfItem, UIs> {
            {KindOfItem.USABLE   , usable   },
            {KindOfItem.UN_USABLE, unUsable },
            {KindOfItem.FOR_STORY, forStory }
        };
        var uis = dict[newItem.KindProperty];
        ItemToUI.Add(newItem, (GenerateHeader(newItem,uis.tabHeaders), GenerateDetail(newItem,uis.tabHeaders)) );

    }
    static GameObject GenerateHeader(Item_Behaviour newItem, Transform tabHeaders) {
        var header = GameObject.Instantiate(ItemDatabase.itemHeaderPrefab, tabHeaders);

        var image = header.transform.Find("Image").GetComponent<Image>();
        image.sprite = newItem.GetComponent<SpriteRenderer>().sprite;

        var nameText = header.transform.Find("Text").GetComponent<Text>();
        nameText.text = newItem.name;
        return header;
    }
    static GameObject GenerateDetail(Item_Behaviour newItem, Transform contentContainer) {
        var detail = GameObject.Instantiate(ItemDatabase.itemDetailPrefab, contentContainer);
        var nameText = detail.transform.Find("name").GetComponent<Text>();
        nameText.text = newItem.name;
        return detail;
    }
}