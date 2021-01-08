using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

namespace DS.UI.ContentUpdate
{
    public class ContentHorizontalUpdater : ContentUpdater
    {
        public float width;
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
                var positionX = item.index.RelativeIndex() * width + space;

#if UNITY_EDITOR
                if (Application.isPlaying)
                {
                    Animate(item.gameObject, positionX);
                }
                else
                {
                    var position = item.gameObject.transform.localPosition;
                    position.x = positionX;
                    item.gameObject.transform.localPosition = position;
                }
#else
            Animate(item.gameObject, positionX);
#endif
            }
        }

        private void Animate(GameObject gameObject, float volume)
        {
            var tween = gameObject.transform
                    .DOLocalMoveX(volume, during)
                    .SetEase(ease)
                    .SetUpdate(true);

            if (customCurve) tween.SetEase(curve);
        }
    }
}
