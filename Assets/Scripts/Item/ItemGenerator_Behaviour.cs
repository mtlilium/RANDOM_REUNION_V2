using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ItemGenerator_Behaviour : MonoBehaviour{
    public string itemName;
    public Transform parent;
    public Vector2 pos;
    [SerializeField]
    private void Start() {
        ItemDatabase.Load();
    }
}

[CustomEditor(typeof(ItemGenerator_Behaviour))]
public class ItemManager_CustomEditor : Editor {
    public override void OnInspectorGUI() {
        base.OnInspectorGUI();
        var itemManager = target as ItemGenerator_Behaviour;
        if (GUILayout.Button("generate item")) {
            if (!ItemDatabase.loaded) ItemDatabase.Load();
            ItemManager.Generate(itemManager.itemName, itemManager.parent, itemManager.pos);
        }
    }
}

