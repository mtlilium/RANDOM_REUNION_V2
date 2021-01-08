using NaughtyAttributes;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace DS.UI
{
    [System.Serializable]
    public enum UIState
    {
        NORMAL,
        ACTIVE,
        HOVER,
        INACTIVE,
    }

    [System.Serializable]
    public abstract class UIStateStyle : MonoBehaviour
    {

#if UNITY_EDITOR
        [Button]
#endif
        public void Resist()
        {
            var selector = GetComponentInParent<UIStateManager>();
            //Debug.Log("resist");
            selector?.SetStyle(this);
        }

        public abstract void Apply(UIState state);
    }

    [DisallowMultipleComponent]
    public class UIStateManager : MonoBehaviour
    {
        [SerializeField]
        private UIState state = default;
        [SerializeField]
        private List<UIStateStyle> styles = new List<UIStateStyle>();


        public void SetState(UIState state)
        {
            this.state = state;
            Apply();
        }
        public void SetState(int state)
        {
            SetState((UIState)state);
        }

        public void SetState()
        {
            SetState((UIState)((int)(state + 1) % Enum.GetNames(typeof(UIState)).Length));
        }

        public void SetStyle(UIStateStyle style)
        {
            if (styles.Contains(style)) return;

            styles.Add(style);
            Apply();
        }

        private void Apply()
        {
            if (styles == null) return;

            styles = styles.Where(s => s != null).ToList();

            foreach (var style in styles)
            {
                style.Apply(state);
            }
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            Apply();
        }
#endif

    }
}
