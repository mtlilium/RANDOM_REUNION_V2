using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DS.UI.ContentUpdate
{
    public class ContentUIStateUpdater : ContentUpdater
    {
        public override void UpdateContents(IEnumerable<ContentInfo> contents)
        {
            foreach (var item in contents)
            {
                var selector = item.gameObject.GetComponent<UIStateManager>();
                var next = (item.index.IsSelected()) ? UIState.HOVER : UIState.NORMAL;

                selector.SetState(next);
            }
        }
    }
}
