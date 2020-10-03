using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ItemSelector_Behaviour : MonoBehaviour {
    DS.UI.Window itemSelectMenu;
    public IEnumerator SelectItem(Item_Behaviour result) {
        itemSelectMenu.Toggle(); // ここでtimescaleが0に
        while (Time.timeScale == 0) {

            yield return new WaitForSecondsRealtime(0.5f);
            
        }
    }
}
