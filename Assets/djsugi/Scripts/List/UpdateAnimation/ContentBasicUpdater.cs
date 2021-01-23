    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DS.UI.ContentUpdate
{
    public abstract class ContentUpdater : MonoBehaviour
    {
        public abstract void UpdateContents(IEnumerable<ContentInfo> contents);
    }

    public class ContentBasicUpdater : ContentUpdater
    {
        private void UpdateContent(ContentInfo content){
            content.gameObject.SetActive(content.index.IsSelected());
        }

        public override void UpdateContents(IEnumerable<ContentInfo> contents){
            foreach (var item in contents){
                //UpdateContent(item);
                if (!item.index.IsSelected()) {
                    item.gameObject.SetActive(false);
                }
            }
            foreach (var item in contents) {
                //UpdateContent(item);
                if (item.index.IsSelected()) {
                    item.gameObject.SetActive(true);
                }
            }
        }
    }
}
