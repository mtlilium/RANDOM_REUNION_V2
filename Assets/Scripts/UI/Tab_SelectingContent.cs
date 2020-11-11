using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using DS.UI;

//tabと同じobjectで使う
public class Tab_SelectingContent : MonoBehaviour {
    Tab tab;
    private void Awake() {
        tab = GetComponent<Tab>();
    }
    public UIContent SelectingContent() {
        var contents = tab.contentContainer.Contents<UIContent>();
        return contents.ElementAt(tab.selected);
    }
}
