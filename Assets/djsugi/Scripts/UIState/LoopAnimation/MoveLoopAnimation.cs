using NaughtyAttributes;
using System;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;
using UniRx;

namespace DS.UI.LoopAnimation
{
    #region Abstract Class
    public abstract class LoopAnimation : MonoBehaviour
    {

        [BoxGroup("Animation"), SerializeField]
        [Tooltip("アニメーションする時間")]
        protected float duration = 1.0f;
        [BoxGroup("Animation"), SerializeField]
        [Tooltip("イージングタイプ")]
        protected Ease ease = Ease.InOutSine;
        [BoxGroup("Animation"), SerializeField]
        [Tooltip("ループタイプ")]
        protected LoopType loop = LoopType.Yoyo;

        [Button]
        public abstract void PlayAnimation();

        [Button]
        public abstract void StopAnimation();

    }

    public abstract class SimpleLoopAnimation : LoopAnimation
    {
        [BoxGroup("Animation"), SerializeField]
        [Tooltip("ループ前に1度待機する時間")]
        protected float delay = .0f;
        [BoxGroup("Animation"), SerializeField]
        [Tooltip("ループ中毎回待機する時間")]
        protected float interval = .0f;
        [BoxGroup("Animation"), SerializeField]
        [Tooltip("停止中再生するか")]
        protected bool ignoreTimescale = false;


        private Sequence sequence;

        public override void PlayAnimation() => sequence.Play();

        public override void StopAnimation() => sequence.Pause();


        private void Start()
        {
            if(delay == .0f)
            {
                sequence = LoopSequence(Tween());
            }
            else
            {
                DOVirtual.DelayedCall(delay, () =>
                {
                    sequence = LoopSequence(Tween());
                });
            }
        }

        protected abstract Tween Tween();

        protected Sequence LoopSequence(Tween tween)
        {
            return DOTween.Sequence()
                .Append(tween.SetRelative().SetEase(ease))
                .SetLoops(-1, loop)
                .PrependInterval(interval)
                .SetLink(gameObject)
                .SetUpdate(UpdateType.Normal, isIndependentUpdate: ignoreTimescale)
                ;
        }
    }
    #endregion


    [RequireComponent(typeof(RectTransform))]
    [DisallowMultipleComponent]
    public class MoveLoopAnimation : SimpleLoopAnimation
    {
        [SerializeField]
        [Tooltip("相対的な変化量")]
        private Vector2 target = Vector2.zero;


        protected override Tween Tween()
        {
            var obj = GetComponent<RectTransform>();
            return obj.DOLocalMove(target, duration);
        }
    }
}
