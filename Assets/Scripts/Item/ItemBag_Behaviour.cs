using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemBag_Behaviour : MonoBehaviour {
    [System.Serializable]
    struct UIs {
        public DS.UI.Tab tab;
        public Transform tabHeaders;
        public Transform contentContainer;
    }
    [SerializeField]
    UIs usable;
    [SerializeField]
    UIs unUsable;
    [SerializeField]
    UIs forStory;

    [SerializeField]
    ItemSelector_Behaviour selectorBehaviour;

    public Dictionary<string, List<Item_Behaviour>> NameToHavingItems { get; private set; }

    public Dictionary<Item_Behaviour, (GameObject header, GameObject detail)> ItemToUI { get; private set; }
    public void Awake() {
        NameToHavingItems = new Dictionary<string, List<Item_Behaviour>>();
        ItemToUI = new Dictionary<Item_Behaviour, (GameObject header, GameObject detail)>();
    }

    public void AddNewItem(Item_Behaviour newItem){
        newItem.transform.parent = transform;
        newItem.EnInactive();
        if (NameToHavingItems.ContainsKey(newItem.name)) {
            NameToHavingItems[newItem.name].Add(newItem);
        }
        else {
            NameToHavingItems.Add(newItem.name, new List<Item_Behaviour>());
            NameToHavingItems[newItem.name].Add(newItem);
        }
        var dict = new Dictionary<KindOfItem, UIs> {
            {KindOfItem.USABLE   , usable   },
            {KindOfItem.UN_USABLE, unUsable },
            {KindOfItem.FOR_STORY, forStory }
        };
        var uis = dict[newItem.KindProperty];
        var header=GenerateHeader(newItem,uis.tabHeaders);
        var detail=GenerateDetail(newItem,uis.contentContainer);
        uis.tab.LinkTabHeader();
        ItemToUI.Add(newItem, (header, detail));
    }
    GameObject GenerateHeader(Item_Behaviour newItem,Transform tabHeaders) {
        var header = Instantiate(ItemDatabase.itemHeaderPrefab, tabHeaders);

        var image = header.transform.Find("Image").GetComponent<Image>();
        image.sprite = newItem.GetComponent<SpriteRenderer>().sprite;

        var nameText = header.transform.Find("Text").GetComponent<Text>();
        nameText.text = newItem.name;
        return header;
    }
    GameObject GenerateDetail(Item_Behaviour newItem, Transform contentContainer) {
        var detail = Instantiate(ItemDatabase.itemDetailPrefab, contentContainer);
        var nameText = detail.transform.Find("name").GetComponent<Text>();
        nameText.text = newItem.name;
        return detail;
    }
    public void DeleteItem(string itemName, int amount) {
        //とりあえずの実装として先頭の一個を削除
        Item_Behaviour deleteItem = NameToHavingItems[itemName][0];
        Destroy(ItemToUI[deleteItem].header);
        Destroy(ItemToUI[deleteItem].detail);
        NameToHavingItems[itemName].Remove(deleteItem);
    }
    /*
    ItemSelector_Behaviour selector;
    public void SelectItem(out Item_Behaviour result) {
        StartCoroutine
    }

    public Item_Behaviour SelectItem(params Item_Behaviour[] items) {

    }
    */
}
