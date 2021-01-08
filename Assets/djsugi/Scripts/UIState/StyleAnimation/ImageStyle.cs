using UnityEngine;
using UnityEngine.UI;
using System;
using DG.Tweening;
using NaughtyAttributes;

namespace DS.UI
{
    #region Abstract Class
    public abstract class ColorStyle<T> : UIStateStyle
    {
        public Color normal = Color.white,
            active = Color.white,
            hover = Color.white,
            inactive = Color.white;

        private T target;


        [BoxGroup("Animation"), SerializeField]
        protected float duration = .0f;
        [BoxGroup("Animation"), SerializeField]
        private bool customCurve = false;
        [BoxGroup("Animation"), SerializeField]
        [HideIf(ConditionOperator.Or, new string[] { "customCurve" })]
        protected Ease ease = Ease.Linear;
        [BoxGroup("Animation"), SerializeField]
        [ShowIf(ConditionOperator.Or, new string[] { "customCurve" })]
        protected AnimationCurve curve = AnimationCurve.Linear(0, 0, 1, 1);


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

        protected void SetTween(Tween tween)
        {
            tween.SetUpdate(true);
            if (customCurve) tween.SetEase(curve);
            else tween.SetEase(ease);
        }

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
            if (Application.isPlaying) return;

            Apply(previous);
        }
#endif
    }
    #endregion

    public class ImageStyle : ColorStyle<Image>
    {

        public override void SetColor(Image target, Color color)
        {
            if (duration == .0f || !Application.isPlaying)
            {
                target.color = color;
            }
            else
            {
                var tween = target.DOColor(color, duration);
                SetTween(tween);
            }
        }
    }
}
