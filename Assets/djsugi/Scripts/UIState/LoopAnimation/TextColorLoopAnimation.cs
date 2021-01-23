using NaughtyAttributes;
using System;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;
using UniRx;
using TMPro;

namespace DS.UI.LoopAnimation
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    [DisallowMultipleComponent]
    public class TextColorLoopAnimation : SimpleLoopAnimation
    {
        [SerializeField]
        [Tooltip("相対的な変化量")]
        private Color target = Color.white;
        //private Gradient target;


        protected override Tween Tween()
        {
            var obj = GetComponent<TextMeshProUGUI>();
            //return obj.DOGradientColor(target, duration);
            return obj.DOBlendableColor(target, duration);
        }
    }
}
