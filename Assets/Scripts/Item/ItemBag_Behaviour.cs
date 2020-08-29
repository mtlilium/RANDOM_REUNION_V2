using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemBag_Behaviour : MonoBehaviour{
    [SerializeField]
    DS.UI.Tab itemTab = null;
    [SerializeField]
    Transform itemTabHeaders = null;
    [SerializeField]
    Transform itemContentContainer =null;

    void Start(){
        
    }
    public void AddNewItem(Item_Behaviour newItem){
        newItem.transform.parent = transform;
        newItem.EnInactive();
        var header = Instantiate(ItemDatabase.itemHeaderPrafab, itemTabHeaders);
        var detail = Instantiate(ItemDatabase.itemDetailPrefab, itemContentContainer);
        itemTab.LinkTabHeader();

        var image = header.transform.Find("Image").GetComponent<Image>();
        image.sprite = newItem.GetComponent<SpriteRenderer>().sprite;
        var nameText = header.transform.Find("Text").GetComponent<Text>();
        nameText.text = newItem.name;

        nameText = detail.transform.Find("name").GetComponent<Text>();
        nameText.text = newItem.name;
    }
}
