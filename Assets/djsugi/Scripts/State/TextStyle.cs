using UnityEngine;
using TMPro;

namespace DS.UI
{
    public class TextStyle : ColorStyle<TextMeshProUGUI>
    {
        public override void SetColor(TextMeshProUGUI target, Color color)
        {
            target.color = color;
        }
    }
}
