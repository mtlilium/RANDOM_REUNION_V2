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
    [DisallowMultipleComponent]
    public class ScaleLoopAnimation : SimpleLoopAnimation
    {
        [SerializeField]
        [Tooltip("相対的な変化量")]
        private Vector2 target = Vector2.zero;


        protected override Tween Tween()
        {
            var obj = GetComponent<RectTransform>();
            return obj.DOScale(target, duration);
        }
    }
}
