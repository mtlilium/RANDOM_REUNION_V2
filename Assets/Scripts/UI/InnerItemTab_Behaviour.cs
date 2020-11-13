using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InnerItemTab_Behaviour : MonoBehaviour{
    ConditionalWindowActivator selectCancelWindowActivator;
    private void Start() {
        selectCancelWindowActivator = GetComponent<ConditionalWindowActivator>();

        var itemTabController = GetComponent<AxisController>();
        itemTabController.upAxisEvent.AddListener(SetSelectCancelWindowCanOpen);
        itemTabController.downAxisEvent.AddListener(SetSelectCancelWindowCanOpen);

        var tabController = GetComponent<AxisTabController>();
        tabController.upAxisEvent.AddListener(SetSelectCancelWindowCanOpen);
        tabController.downAxisEvent.AddListener(SetSelectCancelWindowCanOpen);
    }

    void SetSelectCancelWindowCanOpen() {
        selectCancelWindowActivator.canOpen = ItemMenu.SelectingItemIsSelectable();
    }
}
