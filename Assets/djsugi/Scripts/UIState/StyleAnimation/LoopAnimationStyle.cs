using UnityEngine;
using TMPro;
using DG.Tweening;
using System;

namespace DS.UI.StyleAnimation
{
    public abstract class UIStateStyleSimple<T> : UIStateStyle
    {
        [SerializeField]
        protected T normal = default,
            active = default,
            hover = default,
            inactive = default;


        public T StyleData(UIState state)
        {
            switch (state)
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
    }


    public class LoopAnimationStyle : UIStateStyleSimple<bool>
    {
        public override void Apply(UIState state)
        {
            var objs = GetComponents<LoopAnimation.LoopAnimation>();
            var data = StyleData(state);

            foreach (var obj in objs)
            {
                if (data)
                {
                    obj.PlayAnimation();
                }
                else
                {
                    obj.StopAnimation();
                }
            }
        }
    }
}
