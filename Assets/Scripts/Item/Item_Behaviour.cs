using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Behaviour : ObjectOnMapScript{
    public string name { get; private set; }
    public void EnInactive() {
        gameObject.SetActive(false);
    }

    private void Start() {
        name = gameObject.name;
        pickupButtonSprite=transform.Find("pickupButtonSprite").GetComponent<SpriteRenderer>();
        thisSpriteRenderer = GetComponent<SpriteRenderer>();
        if (pickupButtonSprite == null) {
            Debug.LogError(gameObject.name + "の子要素にpickupButtonSpriteがありません");
        }
    }
    private SpriteRenderer pickupButtonSprite;
    private void OnTriggerEnter2D(Collider2D collision) {
        pickupButtonSprite.enabled = true;
    }
    private void OnTriggerExit2D(Collider2D collision) {
        pickupButtonSprite.enabled = false;
    }
    private SpriteRenderer thisSpriteRenderer;
    private void OnTriggerStay2D(Collider2D collision) {
        if (collision.tag=="Player" && Input.GetButtonDown("Submit")) {
            ItemManager.itemBag.AddNewItem(this);
            thisSpriteRenderer.enabled = false;
        }
    }
    
}
