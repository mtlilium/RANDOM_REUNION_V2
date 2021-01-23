using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace DS.UI.WindowAnimation
{
    public class UIAnimationRotate : UIAnimation
    {
        [Header("Volume")]
        public Vector3 rotate;

        private Quaternion rotation, defaultRotation;

        protected override Tween ShowAnimation()
        {
            return transform
                .DOLocalRotate(defaultRotation.eulerAngles, ShowDuring());
        }

        protected override Tween HideAnimation()
        {
            return transform
                .DOLocalRotate(rotation.eulerAngles, HideDuring());
        }

        protected override void Init()
        {
            defaultRotation = transform.localRotation;
            rotation = defaultRotation * Quaternion.Euler(-rotate);
            transform.localRotation = rotation;
        }
    }
}

