using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DS.UI;
using NaughtyAttributes;
using System.Linq;

namespace ADD_DS.UI {

    public class SelectableContentList : MonoBehaviour {
        public ContentContainer contentContainer;
        public int selected = 0;
        public void SelectTab(UIContent content) {
            //headerのcontentに対応するindex
            UpdateContents(zip => zip.FirstOrDefault(z => z.Item2 == content).Item1);
        }

        [Button]
        public void NextTab() {
            UpdateContents(zip => Mathf.Clamp(selected + 1, 0, zip.Count() - 1));
        }

        [Button]
        public void PreviousTab() {
            UpdateContents(zip => Mathf.Clamp(selected - 1, 0, zip.Count() - 1));
        }

        [Button]
        public void AddAxisController_Horizontal() {
            var axisController = AddAxisController();
            axisController.axisName = InputManagerInfomation.HorizontalDigitalAxesName;
        }
        [Button]
        public void AddAxisController_Vertical() {
            var axisController = AddAxisController();
            axisController.axisName = InputManagerInfomation.VerticalDigitalControllerName;
        }
            AxisController AddAxisController() {
                var axisController = gameObject.AddComponent<AxisController>();
                axisController.upAxisEvent.AddListener(PreviousTab);
                axisController.downAxisEvent.AddListener(NextTab);
                return axisController;
            }


        private void UpdateContents(System.Func<IEnumerable<(int, UIContent)>, int> selector) {
            var contents = contentContainer.Contents<UIContent>();
            var previous = selected;
            int start = 0, end = contents.Count() - 1;
            var zip = Enumerable.Range(0, contents.Count())
                .Select(i => (index: i, tab: contents.ElementAt(i)));

            selected = selector.Invoke(zip);
            var contentsToUpdate = zip.Select(item => {
                return new ContentInfo() {
                    index = new ContentInfo.Index() {
                        start = start,
                        end = end,
                        self = item.index,
                        selected = selected,
                        previous = previous
                    },
                    gameObject = item.tab.gameObject
                };
            });

            contentContainer.UpdateContents(contentsToUpdate);
        }

    }

}
