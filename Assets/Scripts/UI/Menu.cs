using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

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
    public static Dictionary<Item_Behaviour, (ItemHeader_Behaviour header, GameObject detail)> ItemToUI { get; private set; } //Itemが削除された時に一緒にUIを削除するのに使う
    public static Dictionary<DS.UI.UIContent,Item_Behaviour> detailContentToItem { get; private set; } //現在選択中のアイテムを取得するのに使う
    static Item_Behaviour selectedItem;
    public static Item_Behaviour SelectedItem {
        get {
            if (selectedItem == null) {
                DebugLogWrapper.LogError("SelectedItemが参照されましたが、selectedItemがnullです");
            }
            var ret = selectedItem;
            selectedItem = null;
            return ret;
        }
    }

    static Tab_SelectingContent tab;

    static DS.UI.Window window;
    static DS.UI.TabHeader tabHeader; 
    /*
    [System.Serializable]
    public struct UIs {
        public DS.UI.Tab tab;
    }
    static UIs usable;
    static UIs unUsable;
    static UIs forStory;
    */
    static Dictionary<KindOfItem, DS.UI.Tab> kindToTabDict;

    public static void Init(DS.UI.Window _window, DS.UI.TabHeader _tabHeader, Tab_SelectingContent _tab /*, UIs _usable, UIs _unUsable, UIs _forStory*/) {
        ItemToUI = new Dictionary<Item_Behaviour, (ItemHeader_Behaviour header, GameObject detail)>();
        detailContentToItem = new Dictionary<DS.UI.UIContent, Item_Behaviour>();
        window = _window;
        tabHeader = _tabHeader;
        tab = _tab;
        /*
        usable = _usable;
        unUsable = _unUsable;
        forStory = _forStory;
        */

        kindToTabDict = new Dictionary<KindOfItem, DS.UI.Tab>();
        var contents = tab.GetComponent<DS.UI.Tab>().contentContainer.Contents<Transform>();
        int index = 1;
        foreach(var tr in contents) {
            kindToTabDict[(KindOfItem)index] = tr.GetComponentInChildren<DS.UI.Tab>();
            index++;
        }
    }

    public static void OpenToSelect(string itemName) {
        selectedItem = null;
        Menu.Open();
        tabHeader.SelectTab();
        window.Open();
        foreach(var item in ItemToUI.Keys) {
            if (item.name != itemName) {
                ItemToUI[item].header.Selectable = false;
            }
        }
    }
    public static void SetSelectedItem() {
        var selectingTab = SelectingTab();
        selectedItem = detailContentToItem[selectingTab.SelectingContent()];
    }
    public static bool SelectingItemIsSelectable() {
        var selectingTab = SelectingTab();
        var selectingItemHeader = selectingTab.SelectingTabHeader().GetComponent<ItemHeader_Behaviour>();
        return selectingItemHeader.Selectable;
    }
        static Tab_SelectingContent SelectingTab() {
            var selectingContent = tab.SelectingContent();
            return selectingContent.transform.Find(selectingContent.gameObject.name + "Tab").GetComponent<Tab_SelectingContent>();
        }

    public static void AddNewItemUI(Item_Behaviour newItem) {
        /*
        var dict = new Dictionary<KindOfItem, UIs> {
            {KindOfItem.USABLE   , usable   },
            {KindOfItem.UN_USABLE, unUsable },
            {KindOfItem.FOR_STORY, forStory }
        };
        var uis = dict[newItem.KindProperty];        
        var header = GenerateHeader(newItem, uis.tab.headerContainer.transform);
        var detail = GenerateDetail(newItem, uis.tab.contentContainer.transform);
        */
        var tab = kindToTabDict[newItem.KindProperty];
        var header = GenerateHeader(newItem, tab.headerContainer.transform);
        var detail = GenerateDetail(newItem, tab.contentContainer.transform);
        ItemToUI.Add(newItem, (header, detail) );
        detailContentToItem.Add(detail.GetComponent<DS.UI.UIContent>(), newItem);
        //uis.tab.LinkTabHeader();
        tab.LinkTabHeader();
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
        var detailObject = ItemToUI[deleteItem].detail;
        detailContentToItem.Remove(detailObject.GetComponent<DS.UI.UIContent>());
        GameObject.Destroy(ItemToUI[deleteItem].header.gameObject);
        GameObject.Destroy(detailObject);
        DebugLogWrapper.Log(deleteItem.name + " deleted");
        ItemToUI.Remove(deleteItem);
    }
}