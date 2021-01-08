using NaughtyAttributes;
using System;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;
using UniRx;

namespace DS.UI.LoopAnimation
{
    [RequireComponent(typeof(RectTransform))]
    public class RotateLoopAnimation : LoopAnimation
    {
        [SerializeField]
        private Vector3 target = Vector3.zero;

        private Sequence sequence;


        public override void PlayAnimation() => sequence.Play();

        public override void StopAnimation() => sequence.Pause();


        private void Start()
        {
            var obj = GetComponent<RectTransform>();
            var tween = obj.DORotate(target, duration);

            sequence = LoopSequence(tween);
        }
    }
}
