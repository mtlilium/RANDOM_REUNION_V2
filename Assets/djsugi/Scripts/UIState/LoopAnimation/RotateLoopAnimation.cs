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
    public class RotateLoopAnimation : SimpleLoopAnimation
    {
        [SerializeField]
        [Tooltip("相対的な変化量")]
        private Vector3 target = Vector3.zero;


        protected override Tween Tween()
        {
            var obj = GetComponent<RectTransform>();
            return obj.DORotate(target, duration);
        }
    }
}
