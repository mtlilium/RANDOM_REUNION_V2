using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using DG.Tweening;
using NaughtyAttributes;

namespace DS.UI.ContentUpdate
{
    public class ContentHeroUpdater : ContentUpdater
    {
        public Vector3 normal = Vector3.one, 
                         hero = Vector3.one * 1.2f;
        public float during = 1f;

        private Tween tween;
        private Transform selected;

        public override void UpdateContents(IEnumerable<ContentInfo> contents)
        {
            if (selected != null)
            {
                selected.localScale = normal;
            }

            var info = contents.Where(c => c.index.IsSelected()).FirstOrDefault();
            if (info == null) return;
            selected = info.gameObject.transform;

#if UNITY_EDITOR
            if (Application.isPlaying)
            {
                Animate(selected);
            }
            else
            {
                selected.localScale = hero;
            }
#else
            Animate(selected);
#endif
        }

        private void Animate(Transform transform)
        {
            if (transform != null)
            {
                if (tween != null && !tween.IsComplete()) tween.Complete();

                transform
                    .DOScale(normal, during)
                    .SetUpdate(true);
            }

            tween = transform
                .DOScale(hero, during)
                .SetUpdate(true);
        }
    }
}
