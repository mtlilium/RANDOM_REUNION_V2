using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using NaughtyAttributes;
using DS.UI.ContentUpdate;

namespace DS.UI
{
    public class ContentContainer : MonoBehaviour
    {
        [SerializeField]
        private List<ContentUpdater> updaters;

        public void UpdateContents(IEnumerable<ContentInfo> contents)
        {
            foreach (var updater in updaters)
            {
                updater.UpdateContents(contents);
            }
        }

#if UNITY_EDITOR
        [Button]
        public void SetUpdater()
        {
            updaters = GetComponents<ContentUpdater>().ToList();
        }
#endif

        public IEnumerable<T> Contents<T>()
        {
            return transform.Cast<Transform>()
                .Select(t => t.GetComponent<T>())
                .Where(h => h != null);
        }
    }
}
