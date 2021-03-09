using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

namespace DS.UI.ContentUpdate
{
    [System.Serializable]
    public class Boxstyle
    {
        public Color text;
        public Sprite image;
    }

    public class ContentTextboxUpdater : ContentUpdater
    {
        public Boxstyle normal;
        public Boxstyle selected;

        public override void UpdateContents(IEnumerable<ContentInfo> contents)
        {
            foreach (var item in contents)
            {
                var image = item.gameObject.GetComponent<Image>();
                var text = item.gameObject.GetComponentInChildren<Text>();
                var next = (item.index.IsSelected()) ? selected : normal;

                if (image != null) image.sprite = next.image;
                if (text != null) text.color = next.text;

            }
        }
    }
}
