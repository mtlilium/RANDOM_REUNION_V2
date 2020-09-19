using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemBag_Behaviour : MonoBehaviour{
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

    public void AddNewItem(Item_Behaviour newItem){
        newItem.transform.parent = transform;
        newItem.EnInactive();
        var dict = new Dictionary<KindOfItem, UIs> {
            {KindOfItem.USABLE, usable },
            {KindOfItem.UN_USABLE, unUsable },
            {KindOfItem.FOR_STORY, forStory }
        };
        Debug.Log(newItem.name + ":" + newItem.KindProperty);
        var uis = dict[newItem.KindProperty];
        GenerateHeader(newItem,uis.tabHeaders);
        GenerateDetail(newItem,uis.contentContainer);
        uis.tab.LinkTabHeader();
    }
    void GenerateHeader(Item_Behaviour newItem,Transform tabHeaders) {
        var header = Instantiate(ItemDatabase.itemHeaderPrefab, tabHeaders);

        var image = header.transform.Find("Image").GetComponent<Image>();
        image.sprite = newItem.GetComponent<SpriteRenderer>().sprite;

        var nameText = header.transform.Find("Text").GetComponent<Text>();
        nameText.text = newItem.name;
    }
    void GenerateDetail(Item_Behaviour newItem, Transform contentContainer) {
        var detail = Instantiate(ItemDatabase.itemDetailPrefab, contentContainer);
        var nameText = detail.transform.Find("name").GetComponent<Text>();
        nameText.text = newItem.name;
    }
}
