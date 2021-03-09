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
        private bool showMore = false;
        [BoxGroup("Animation"), SerializeField]
        [ShowIf("showMore")]
        [Tooltip("ループ前に1度待機する時間")]
        protected float delay = .0f;
        [BoxGroup("Animation"), SerializeField]
        [ShowIf("showMore")]
        [Tooltip("ループ中毎回待機する時間")]
        protected float interval = .0f;
        [BoxGroup("Animation"), SerializeField]
        [ShowIf("showMore")]
        [Tooltip("停止中再生するか")]
        protected bool ignoreTimescale = false;
        [BoxGroup("Animation"), SerializeField]
        [ShowIf("showMore")]
        [Tooltip("ループ再生(-1=infinite)")]
        protected int loopTimes = -1;


        private Sequence sequence;


        public override void PlayAnimation()
        {
            if (LoopTimes() > 0)
            {
                Delay(() => sequence.Restart());
            }
            else
            {
                sequence.Play();
            }
        }

        public override void StopAnimation()
        {
            if (LoopTimes() > 0)
            {
                sequence.Rewind(true);
            }
            else
            {
                sequence.Pause();
            }
        }


        private void Start()
        {
            Delay(() => sequence = LoopSequence(Tween()));
        }


        private void Delay(Action callback)
        {
            if (delay == .0f)
            {
                callback();
            }
            else
            {
                DOVirtual.DelayedCall(delay, () =>
                {
                    callback();
                });
            }
        }

        protected abstract Tween Tween();

        private int LoopTimes() => loopTimes;

        protected Sequence LoopSequence(Tween tween)
        {
            return DOTween.Sequence()
                .Append(tween.SetRelative().SetEase(ease))
                .SetLoops(LoopTimes(), loop)
                .PrependInterval(interval)
                .SetLink(gameObject)
                .SetUpdate(UpdateType.Normal, isIndependentUpdate: ignoreTimescale)
                .SetAutoKill(false)
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
