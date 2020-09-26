using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public enum KindOfItem {
    USABLE,
    UN_USABLE,
    FOR_STORY
}

public class Item_Behaviour : MonoBehaviour /*ObjectOnMapScript*/ {
    public new string name { get; private set; }
    public KindOfItem KindProperty {
        get {
            return kind;
        }
        private set {
            kind = value;
        }
    }
    //Serializeに指定しておくとInspectorでの変更が反映されるので、ItemGeneratorでうまくkindが指定できる (serializeじゃないと勝手にkindが0に初期化される)
    [SerializeField]
    KindOfItem kind;


    bool initialized = false;
    public void Init(string _name,KindOfItem _kind) {
        if (initialized) {
            Debug.LogAssertion(gameObject.name + "のItem_Behaviour.Init()が二回以上呼ばれました");
            return;
        }
        initialized = true;
        name = _name;
        KindProperty = _kind;
    }

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
