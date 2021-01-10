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
    [DisallowMultipleComponent]
    public class FadeLoopAnimation : SimpleLoopAnimation
    {
        [SerializeField]
        [Tooltip("相対的な変化量")]
        private float target = -1f;


        protected override Tween Tween()
        {
            var obj = GetComponent<CanvasGroup>();
            return obj.DOFade(target, duration);
        }
    }
}
