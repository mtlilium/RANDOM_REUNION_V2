using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace DS.UI.WindowAnimation
{
    [RequireComponent(typeof(CanvasGroup))]
    public class UIAnimationCanvasGroup : UIAnimation
    {
        [Header("Volume")]
        [SerializeField]
        private float hideAlpha = 0f;

        private CanvasGroup canvas;
        private float defaultAlpha;

        protected override Tween ShowAnimation()
        {
            return canvas.DOFade(defaultAlpha, ShowDuring());
        }

        protected override Tween HideAnimation()
        {
            return canvas.DOFade(hideAlpha, HideDuring());
        }

        protected override void Init()
        {
            canvas = GetComponent<CanvasGroup>();
            defaultAlpha = canvas.alpha;
            canvas.alpha = hideAlpha;
        }
    }
}

