using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

namespace DS.UI.ContentUpdate
{
    public class ContentVerticalUpdater : ContentUpdater
    {
        public float height;
        public float spacing;

        [Header("Animation")]
        public bool customCurve;
        [HideIf("customCurve")]
        public Ease ease;
        [ShowIf("customCurve")]
        public AnimationCurve curve;
        public float during = 1f;

        public override void UpdateContents(IEnumerable<ContentInfo> contents)
        {
            foreach (var item in contents)
            {
                item.gameObject.SetActive(true);
                var space = item.index.RelativeIndexSign() * spacing;
                var positionY = item.index.RelativeIndex() * height + space;

#if UNITY_EDITOR
                if (Application.isPlaying)
                {
                    Animate(item.gameObject, positionY);
                }
                else
                {
                    var position = item.gameObject.transform.localPosition;
                    position.y = positionY;
                    item.gameObject.transform.localPosition = position;
                }
#else
            Animate(item.gameObject, positionY);
#endif
            }
        }

        private void Animate(GameObject gameObject, float volume)
        {
            var tween = gameObject.transform
                    .DOLocalMoveY(volume, during)
                    .SetEase(ease)
                    .SetUpdate(true);

            if (customCurve) tween.SetEase(curve);
        }
    }
}
