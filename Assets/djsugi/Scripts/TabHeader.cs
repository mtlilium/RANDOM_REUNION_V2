using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using NaughtyAttributes;
using UnityEngine.UI;

namespace DS.UI
{
    public class TabHeader : MonoBehaviour
    {
        //[SerializeField]
        private bool edit;
        [SerializeField, EnableIf("edit")]
        private Tab parent;
        [SerializeField, EnableIf("edit")]
        private UIContent content;

        public Tab Parent { get => parent; set => parent = value; }
        public UIContent Content { get => content; set => content = value; }

        [Button]
        public void SelectTab()
        {
            Parent.SelectTab(Content);
        }
    }
}

