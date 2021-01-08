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
        protected float duration = 1.0f;
        [BoxGroup("Animation"), SerializeField]
        protected float interval = .0f;
        [BoxGroup("Animation"), SerializeField]
        protected Ease ease = Ease.InOutSine;
        [BoxGroup("Animation"), SerializeField]
        protected LoopType loop = LoopType.Yoyo;


        [Button]
        public abstract void PlayAnimation();

        [Button]
        public abstract void StopAnimation();

        protected Sequence LoopSequence(Tween tween)
        {
            return DOTween.Sequence()
                .AppendInterval(interval)
                .Append(tween.SetRelative().SetEase(ease))
                .SetLoops(-1, loop)
                .SetLink(gameObject)
                .SetUpdate(true)
                ;
        }
    }
    #endregion


    [RequireComponent(typeof(RectTransform))]
    public class MoveLoopAnimation : LoopAnimation
    {
        [SerializeField]
        private Vector2 target = Vector2.zero;

        private Sequence sequence;


        public override void PlayAnimation() => sequence.Play();

        public override void StopAnimation() => sequence.Pause();


        private void Start()
        {
            var obj = GetComponent<RectTransform>();
            var tween = obj.DOLocalMove(target, duration);

            sequence = LoopSequence(tween);
        }
    }
}
