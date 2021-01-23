using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using DG.Tweening;
using NaughtyAttributes;

namespace DS.UI.ContentUpdate
{
    public class ContentUnderlineUpdater : ContentUpdater
    {
        public GameObject underline;
        public Vector2 offset;

        public float during = 1f;
        private Sequence seq;
        private Vector2 origin;

        public override void UpdateContents(IEnumerable<ContentInfo> contents)
        {
            var selected = contents.Where(c => c.index.IsSelected()).FirstOrDefault();
            var position = selected.gameObject.transform.localPosition + (Vector3)offset;

#if UNITY_EDITOR
            if (Application.isPlaying)
            {
                Animate(underline, position);
            }
            else
            {
                underline.transform.localPosition = position;
            }
#else
            Animate(underline, position);
#endif


        }

        private void Animate(GameObject gameObject, Vector3 position)
        {

            var rect = gameObject.GetComponent<RectTransform>();

            if (seq == null || !seq.active || !seq.IsPlaying())
            {
                origin = rect.sizeDelta;
            }

            Vector2 delta = position - gameObject.transform.localPosition;
            delta = new Vector2(Mathf.Abs(delta.x), Mathf.Abs(delta.y));

            seq = DOTween.Sequence();
            seq.Append(rect.DOSizeDelta(origin + delta, during / 2f).SetEase(Ease.Linear).SetRelative(false));
            seq.Append(rect.DOSizeDelta(origin, during / 2f).SetEase(Ease.Linear).SetRelative(false));
            seq.SetLink(this.gameObject).SetUpdate(true);

            underline.transform.DOLocalMove(position, during).SetEase(Ease.Linear).SetUpdate(true);
        }
    }
}

