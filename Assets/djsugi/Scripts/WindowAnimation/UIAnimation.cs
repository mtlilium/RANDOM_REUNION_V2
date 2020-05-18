using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using NaughtyAttributes;
#endif

namespace DS.UI.WindowAnimation
{
    public abstract class UIAnimation : MonoBehaviour
    {
        [BoxGroup("Animation"), ShowIf("development"),SerializeField]
        private bool separateDuring = false;
        [BoxGroup("Animation"), HideIf("separateDuring"), SerializeField]
        protected float during = 0.2f;
        [BoxGroup("Animation"), SerializeField]
        [ShowIf(ConditionOperator.And, new string[] { "separateDuring", "development" })]
        protected float showDuring = 0.2f, hideDuring = 0.2f;

        [BoxGroup("Animation"), ShowIf("development"), SerializeField, Space()]
        private bool customCurve = false;
        [BoxGroup("Animation"), SerializeField]
        [HideIf(ConditionOperator.Or, new string[] { "customCurve", "NotDevelopment" })]
        protected Ease inEase = Ease.Linear, outEase = Ease.Linear;
        [BoxGroup("Animation"), SerializeField]
        [ShowIf(ConditionOperator.And, new string[] { "customCurve", "development" })]
        protected AnimationCurve inCurve = AnimationCurve.Linear(0, 0, 1, 1),
                                outCurve = AnimationCurve.Linear(0, 0, 1, 1);

#if UNITY_EDITOR
        [InfoBox("This animation will not play", InfoBoxType.Warning, "IsShow")]
        [SerializeField]
        private bool development = false;
        public bool NotDevelopment() => !development;
#endif


        private bool isCashed = false;
        private Tween previous;


        public Tween Show()
        {
            TryInit();

            previous = ShowAnimation().SetUpdate(true);
            if (customCurve) previous.SetEase(inCurve);
            else previous.SetEase(inEase);

            return previous;
        }

        public Tween Hide()
        {
            TryInit();

            previous = HideAnimation().SetUpdate(true);
            if (customCurve) previous.SetEase(outCurve);
            else previous.SetEase(outEase);

            return previous;
        }

        protected float ShowDuring() => (separateDuring) ? showDuring : during;
        protected float HideDuring() => (separateDuring) ? hideDuring : during;
        protected abstract Tween ShowAnimation();
        protected abstract Tween HideAnimation();
        protected abstract void Init();

        private void TryInit()
        {
            if (previous != null && !previous.IsComplete()) previous.Complete();

            if (!isCashed) Init();
            isCashed = true;
        }


#if UNITY_EDITOR
        [Button]
        public void SetAnimation()
        {
            var animations = GetComponentInParent<Window>().animations;
            if (!animations.Contains(this))
            {
                animations.Add(this);
            }
        }

        public bool IsShow() {
            var window = GetComponentInParent<Window>();
            if (window == null) return false;
            return !window.animations.Contains(this);
        }
    }
#endif
}
