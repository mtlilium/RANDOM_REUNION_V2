using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* GameScene側のFangus等から、UISceneのMenuを操作するためのクラス
 * Check : Fungus側のイベントを正しく作れば多分消せるので消したい
 */

public class Menu_Behaviour : MonoBehaviour {
    public void Open() {
        Menu.Open();
    }
    public void Close() {
        Menu.Close();
    }
    public void OpenItemMenuToSelect(string itemName) {
        ItemMenu.OpenToSelect(itemName);
    }
    
    public Item_Behaviour SelectedItem() {
        return ItemMenu.SelectedItem;
    }
    public string selectedItemName() {
        return SelectedItem().name;
    } 
}
