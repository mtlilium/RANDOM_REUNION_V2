using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemBag_Behaviour : MonoBehaviour {

    public Dictionary<string, List<Item_Behaviour>> NameToHavingItems { get; private set; }
    public void Awake() {
        NameToHavingItems = new Dictionary<string, List<Item_Behaviour>>();
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
        ItemMenu.AddNewItemUI(newItem);
    }
    /*
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
    */
    public void DeleteItem(string itemName, int amount) {
        //とりあえずの実装として先頭の一個を削除
        Item_Behaviour deleteItem = NameToHavingItems[itemName][0];
        NameToHavingItems[itemName].Remove(deleteItem);
        ItemMenu.DeleteItemUI(deleteItem);
    }
}
