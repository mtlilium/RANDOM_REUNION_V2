using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DS.UI;

/* staticなクラスたちのinitを呼び出して参照を渡す
 * staticなクラス、MenuからSceneに存在するMenuWindowが開けるようにする
 */
public class MenuInUIScene_Behaviour : MonoBehaviour{
    [SerializeField]
    Window menuWindow=null;

    [SerializeField]
    Window itemWindow=null; //itemMenuの初期化用 itemWindowを直接開くために使う
    [SerializeField]
    TabHeader itemTabHeader = null; //itemMenuの初期化用　itemTabHeaderを選択状態にしてから開かないとバグるので必要
    [SerializeField]
    Tab_SelectingContent itemTab = null; //itemMenuの初期化用

    [SerializeField]
    Tab questTab = null; //QuestMenuの初期化用 HeaderとDetailのparent取得と、生成後にLinkTabHeaderを呼ぶのに使う　
    void Start() {
        ItemMenu.Init(itemWindow, itemTabHeader, itemTab);
        QuestMenu.Init(questTab);
    }
    public void OpenMenuWindow() {
        menuWindow.Open();
    }
    public void CloseMenuWindow() {
        menuWindow.Close();
    }
    public void SetSelectedItem() {//Select/Cancelボタンから呼ぶ用
        ItemMenu.SetSelectedItem();
    }

    [SerializeField]
    Button button;
    private void Update() {
        if (Input.GetButtonDown("Menu")) {
            if (menuWindow == null)  DebugLogWrapper.Log("menuWindowがnull");
            menuWindow.Toggle();
            UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(button.gameObject);
        }
    }
}
