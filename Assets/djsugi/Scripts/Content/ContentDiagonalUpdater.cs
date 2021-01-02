using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using System.Linq;

namespace DS.UI.ContentUpdate
{
    public class ContentDiagonalUpdater : ContentUpdater
    {
        [Header("Amount of movement")]
        [MinValue(1f)]
        public int indexScale;
        public Vector2 magnitude = new Vector2(100, 100);

        [Header("Movement Curve")]
        public AnimationCurve x = AnimationCurve.Linear(0, 0, 1, 1);
        public AnimationCurve y = AnimationCurve.Linear(0, 0, 1, 1);
        public Vector2 curveCenter = new Vector2(0.5f, 0.5f);

        [Header("Animation")]
        public bool useHighQuality;
        public float during = 1f;

        private Dictionary<int, Vector3> points = new Dictionary<int, Vector3>();

        public override void UpdateContents(IEnumerable<ContentInfo> contents)
        {
            foreach (var item in contents)
            {
                item.gameObject.SetActive(true);
                var index = item.index.RelativeIndex();

#if UNITY_EDITOR
                if (Application.isPlaying)
                {
                    if(useHighQuality) AnimateHighQuarity(item.gameObject, item.index);
                    else Animate(item.gameObject, index);
                }
                else
                {
                    item.gameObject.transform.localPosition = ScaledCurvePosition(index);
                }
#else
                if(useHighQuality) AnimateHighQuarity(item.gameObject, item.index);
                else Animate(item.gameObject, index);
#endif
            }
        }

        private void Animate(GameObject gameObject, int index)
        {
            gameObject.transform
                .DOLocalMove(ScaledCurvePosition(index), during)
                .SetEase(Ease.Linear)
                .SetUpdate(true);
        }

        private void AnimateHighQuarity(GameObject gameObject, ContentInfo.Index index)
        {
            int shift = Mathf.Abs(index.ChangedIndex());
            int sign = (int)Mathf.Sign(index.ChangedIndex());
            var indexs = Enumerable.Range(0, shift)
                .Select(i => index.RelativeIndex() + (shift - 1 - i) * sign)
                .Select(i => Mathf.Clamp(i, -indexScale/2, indexScale/2));

            if (shift == 0) return;

            var target = indexs
                .Select(i => {
                    if(!points.ContainsKey(i))
                    {
                        points.Add(i, ScaledCurvePosition(i));
                    }
                    return points[i];
                })
                .ToArray();

            gameObject.transform
                .DOLocalPath(target, during)
                .SetEase(Ease.Linear)
                .SetUpdate(true);
        }

        private float NormalizedIndex(int index) => (index / (float)indexScale) + curveCenter.x;

        private Vector3 ScaledCurvePosition(int index) => ScaledCurvePosition(NormalizedIndex(index));

        private Vector3 ScaledCurvePosition(float time)
        {
            var offset = curveCenter.y;
            var curve = new Vector2(x.Evaluate(time) - offset, y.Evaluate(time) - offset);
            return curve * magnitude * (float)indexScale;
        }
    }
}
