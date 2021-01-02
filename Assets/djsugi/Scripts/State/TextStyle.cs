using UnityEngine;
using TMPro;
using DG.Tweening;

namespace DS.UI
{
    public class TextStyle : ColorStyle<TextMeshProUGUI>
    {
        public override void SetColor(TextMeshProUGUI target, Color color)
        {
            if (duration == .0f || !Application.isPlaying)
            {
                target.color = color;
            }
            else
            {
                var tween = target.DOColor(color, duration);
                SetTween(tween);
            }

        }
    }
}
