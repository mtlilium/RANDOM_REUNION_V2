using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace DS.UI.WindowAnimation
{
    public class UIAnimationImage : UIAnimation
    {
        private Image image;
        private Color color, defaultColor;

        protected override Tween ShowAnimation()
        {
            return image.DOColor(defaultColor, ShowDuring());
        }

        protected override Tween HideAnimation()
        {
            return image.DOColor(color, HideDuring());
        }

        protected override void Init()
        {
            image = GetComponent<Image>();
            defaultColor = image.color;
            color = Color.clear;
            image.color = color;
        }
    }
}

