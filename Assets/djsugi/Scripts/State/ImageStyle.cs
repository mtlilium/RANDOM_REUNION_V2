using UnityEngine;
using UnityEngine.UI;
using System;

namespace DS.UI
{
    public abstract class ColorStyle<T> : Style
    {
        public Color normal = Color.white,
            active = Color.white,
            hover = Color.white,
            inactive = Color.white;

        private T target;

#if UNITY_EDITOR
        private UIState previous;
#endif

        public Color StyleColor(UIState style)
        {
            switch (style)
            {
                case UIState.NORMAL:
                    return normal;
                case UIState.ACTIVE:
                    return active;
                case UIState.HOVER:
                    return hover;
                case UIState.INACTIVE:
                    return inactive;
            }

            throw new NotImplementedException();
        }

        public abstract void SetColor(T target, Color color);

        public override void Apply(UIState style)
        {
            target = (target != null) ? target : GetComponent<T>();

#if UNITY_EDITOR
            previous = style;
#endif

            SetColor(target, StyleColor(style));
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            Apply(previous);
        }
#endif
    }

    public class ImageStyle : ColorStyle<Image>
    {
        public override void SetColor(Image target, Color color)
        {
            target.color = color;
        }
    }
}
