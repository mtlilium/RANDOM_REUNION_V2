using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class Menu{
    static Menu_Behaviour menuBehaviour;
    public static void Init(Menu_Behaviour _menuBehaviour) {
        menuBehaviour = _menuBehaviour;
    }
    static public void Open() {
        menuBehaviour.Open();
    }
}

public static class ItemMenu { 
    public static Dictionary<Item_Behaviour, (ItemHeader_Behaviour header, GameObject detail)> ItemToUI { get; private set; }
    public static Dictionary<ItemHeader_Behaviour,Item_Behaviour> headerToItem { get; private set; }

    static DS.UI.Window window;

    static DS.UI.TabHeader tabHeader;

    [System.Serializable]
    public struct UIs {
        public DS.UI.Tab tab;
        public Transform tabHeaders;
        public Transform contentContainer;
    }
    static UIs usable;
    static UIs unUsable;
    static UIs forStory;

    public static void Init(DS.UI.Window _window, DS.UI.TabHeader _tabHeader, UIs _usable, UIs _unUsable, UIs _forStory) {
        ItemToUI = new Dictionary<Item_Behaviour, (ItemHeader_Behaviour header, GameObject detail)>();
        headerToItem = new Dictionary<ItemHeader_Behaviour, Item_Behaviour>();
        window = _window;
        tabHeader = _tabHeader;
        usable = _usable;
        unUsable = _unUsable;
        forStory = _forStory;
    }

    static public void OpenToSelect(string itemName) {
        Menu.Open();
        tabHeader.SelectTab();
        window.Open();
        foreach(var item in ItemToUI.Keys) {
            if (item.name != itemName) {
                Debug.Log(item.name + " is unselectable");
                ItemToUI[item].header.Selectable = false;
            }
        }
    }
    
    public static void AddNewItemUI(Item_Behaviour newItem) {
        var dict = new Dictionary<KindOfItem, UIs> {
            {KindOfItem.USABLE   , usable   },
            {KindOfItem.UN_USABLE, unUsable },
            {KindOfItem.FOR_STORY, forStory }
        };
        var uis = dict[newItem.KindProperty];
        var header = GenerateHeader(newItem, uis.tabHeaders);
        ItemToUI.Add(newItem, (header, GenerateDetail(newItem,uis.contentContainer)) );
        headerToItem.Add(header, newItem);
        uis.tab.LinkTabHeader();
    }
        static ItemHeader_Behaviour GenerateHeader(Item_Behaviour newItem, Transform tabHeaders) {
            var header = GameObject.Instantiate(ItemDatabase.itemHeaderPrefab, tabHeaders);
            
            var image = header.transform.Find("Image").GetComponent<Image>();
            image.sprite = newItem.GetComponent<SpriteRenderer>().sprite;

            var nameText = header.transform.Find("Text").GetComponent<Text>();
            nameText.text = newItem.name;
            return header.GetComponent<ItemHeader_Behaviour>();
        }
        static GameObject GenerateDetail(Item_Behaviour newItem, Transform contentContainer) {
            var detail = GameObject.Instantiate(ItemDatabase.itemDetailPrefab, contentContainer);
            var nameText = detail.transform.Find("name").GetComponent<Text>();
            nameText.text = newItem.name;
            return detail;
        }
    public static void DeleteItemUI(Item_Behaviour deleteItem) {
        GameObject.Destroy(ItemToUI[deleteItem].header.gameObject);
        GameObject.Destroy(ItemToUI[deleteItem].detail);
        ItemToUI.Remove(deleteItem);
    }
}