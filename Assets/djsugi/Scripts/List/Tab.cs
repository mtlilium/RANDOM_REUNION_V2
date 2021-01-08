using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using NaughtyAttributes;

namespace DS.UI
{
    public class Tab : MonoBehaviour
    {
        public ContentContainer headerContainer, contentContainer;
        public int selected = 0;

        public void SelectTab(UIContent content)
        {
            //headerのcontentに対応するindex
            UpdateContents(zip => zip.FirstOrDefault(z => z.Item2 == content).Item1);
        }

        [Button]
        public void NextTab()
        {
            UpdateContents(zip => Mathf.Clamp(selected + 1, 0, zip.Count() - 1));
        }

        [Button]
        public void PreviousTab()
        {
            UpdateContents(zip => Mathf.Clamp(selected - 1, 0, zip.Count() - 1));
        }

#if UNITY_EDITOR
        [Button]
        public void LinkTabHeader()
        {
            var headers = headerContainer.Contents<TabHeader>();
            var contents = contentContainer.Contents<UIContent>();
            var zip = Enumerable.Zip(headers, contents, (h, c) => (h, c));

            foreach (var (header, content) in zip)
            {
                header.Parent = this;
                header.Content = content;
            }
        }
#endif


        private void UpdateContents(System.Func<IEnumerable<(int, UIContent)>, int> selector)
        {
            var headers = headerContainer.Contents<TabHeader>();
            var tabs = contentContainer.Contents<UIContent>();
            var previous = selected;
            var start = 0;
            var end = tabs.Count() - 1;
            var zip = Enumerable.Range(0, tabs.Count())
                .Select(i => (index: i, tab: tabs.ElementAt(i)));

            selected = selector.Invoke(zip);

            var contents = zip.Select(item =>
            {
                return new ContentInfo()
                {
                    index = new ContentInfo.Index()
                    { start = start, end = end, self = item.index, selected = selected, previous = previous },
                    gameObject = item.tab.gameObject
                };
            });

            var headerContents = contents.Where(c => c.gameObject = headers.ElementAt(c.index.self).gameObject);

            headerContainer.UpdateContents(headerContents);
            contentContainer.UpdateContents(contents);
        }
    }
}
