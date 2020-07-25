using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

namespace DS.UI.WindowAnimation
{
    public class UIAnimationSlide : UIAnimation
    {
        [Header("Volume")]
        public bool enableVector;
        [HideIf("enableVector"), Slider(0f, 1f)]
        public float magnitude = 1f;
        [HideIf("enableVector")]
        public float angle = 0f;
        [ShowIf("enableVector")]
        public Vector2 direction;

        private Vector2 screenSize;
        private Vector3 position, defaultPosition;

        protected override Tween ShowAnimation()
        {
            return transform.DOLocalMove(defaultPosition, ShowDuring());
        }

        protected override Tween HideAnimation()
        {
            return transform.DOLocalMove(position, HideDuring());
        }

        protected override void Init()
        {
            screenSize = new Vector2(Screen.width, Screen.height);
            defaultPosition = transform.localPosition;
            if (!enableVector) direction = Quaternion.Euler(0, 0, angle) * Vector2.right * magnitude;
            position = defaultPosition + (Vector3)(screenSize * direction);
            transform.localPosition = position;
        }
    }
}
