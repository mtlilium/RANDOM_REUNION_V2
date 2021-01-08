using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

namespace DS.UI.ContentUpdate
{
    public class ContentImageColorUpdater : ContentUpdater
    {
        public Color normalColor = Color.white;
        public Color selectedColor = Color.gray;

        public override void UpdateContents(IEnumerable<ContentInfo> contents)
        {
            foreach (var item in contents)
            {
                var image = item.gameObject.GetComponent<Image>();

                if (image == null) continue;

                var nextColor = normalColor;
                if (item.index.IsSelected()) nextColor = selectedColor;
                image.color = nextColor;
            }
        }
    }
}
