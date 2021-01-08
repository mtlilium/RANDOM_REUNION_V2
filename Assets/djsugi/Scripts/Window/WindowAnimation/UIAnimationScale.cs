using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace DS.UI.WindowAnimation
{
    public class UIAnimationScale : UIAnimation
    {
        [Header("Volume")]
        public Vector3 startScale;

        private Vector3 defaultScale;

        protected override Tween ShowAnimation()
        {
            return transform.DOScale(defaultScale, ShowDuring());
        }

        protected override Tween HideAnimation()
        {
            return transform.DOScale(startScale, HideDuring());
        }

        protected override void Init()
        {
            defaultScale = transform.localScale;
            transform.localScale = startScale;
        }
    }
}
