using NaughtyAttributes;
using System;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;
using UniRx;

namespace DS.UI.LoopAnimation
{
    [RequireComponent(typeof(CanvasGroup))]
    public class FadeLoopAnimation : LoopAnimation
    {
        [SerializeField]
        private float target = -1f;

        private Sequence sequence;


        public override void PlayAnimation() => sequence.Play();

        public override void StopAnimation() => sequence.Pause();


        private void Start()
        {
            var obj = GetComponent<CanvasGroup>();
            var tween = obj.DOFade(target, duration);

            sequence = LoopSequence(tween);
        }
    }
}
