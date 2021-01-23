using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using NaughtyAttributes;

namespace DS.UI
{
    public class ScrollList : MonoBehaviour
    {
        public Scrollbar bar;
        public ContentContainer container;

        [Slider(0f, 1f)]
        public float value = 0f;

        private int selected = 0;

        private void Start()
        {
            bar.onValueChanged.AddListener(UpdateContent);
            UpdateContent();
        }

        [Button]
        public void UpdateContent()
        {
            bar.value = value;
            UpdateContent(value);
        }

        [Button]
        public void NextContent()
        {
            UpdateContent(_ => selected + 1);
        }

        [Button]
        public void PreviousContent()
        {
            UpdateContent(_ => selected - 1);
        }

        private void UpdateContent(float value)
        {
            UpdateContent(length => (int)(value * length + 0.5f));
        }

        private void UpdateContent(System.Func<int, int> selector)
        {
            var list = container.Contents<UIContent>();
            var previous = selected;
            var start = 0;
            var end = list.Count() - 1;
            var zip = Enumerable.Range(0, list.Count())
                .Select(i => (index: i, tab: list.ElementAt(i)));

            selected = Mathf.Clamp(selector(end - start), start, end);

            var contents = zip.Select(item =>
            {
                return new ContentInfo()
                {
                    index = new ContentInfo.Index()
                    { start = start, end = end, self = item.index, selected = selected, previous = previous },
                    gameObject = item.tab.gameObject
                };
            });

            container.UpdateContents(contents);
        }
    }

}
