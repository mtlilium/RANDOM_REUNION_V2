using NaughtyAttributes;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System.IO;

namespace DS.UI.util
{
    public class BoxDrawer : MonoBehaviour
    {
#if UNITY_EDITOR

        [InfoBox("A tool on the editor that helps you set up sprites.\n" +
                 "This component will be disabled during play.")]
        public string root = "UI/Element/";

        public DrawOption.Theme theme;
        private Dictionary<DrawOption.Theme, string> set_list =
            new Dictionary<DrawOption.Theme, string>()
            {
                { DrawOption.Theme.Normal, "normal" },
                { DrawOption.Theme.Round, "rect" },
                { DrawOption.Theme.Circle, "circle" },
                { DrawOption.Theme.Diamond, "diamond" },
                { DrawOption.Theme.Capsule, "capsule" },
            };

        public Color fill_color = Color.white;


        [BoxGroup("Shadow")]
        public bool use_shadow;
        [BoxGroup("Shadow")][ShowIf("use_shadow")]
        public DrawOption.Shadow shadow;
        private Dictionary<DrawOption.Shadow, string> shadow_list =
            new Dictionary<DrawOption.Shadow, string>() 
            {
                { DrawOption.Shadow.Shadow, "shadow" },
                { DrawOption.Shadow.Inner_Shadow, "shadow_inner" },
                { DrawOption.Shadow.Blur, "blur" },
                { DrawOption.Shadow.Inner_Blur, "blur_inner" },
            };

        [BoxGroup("Stroke")]
        public bool use_stroke;
        [BoxGroup("Stroke")][ShowIf("use_stroke")]
        public Color stroke_color = Color.white;
        [BoxGroup("Stroke")][ShowIf("use_stroke")]
        public DrawOption.Stroke stroke;
        private Dictionary<DrawOption.Stroke, string> stroke_list =
            new Dictionary<DrawOption.Stroke, string>()
            {
                { DrawOption.Stroke.Thin , "stroke_thin" },
                { DrawOption.Stroke.Normal , "stroke" },
                { DrawOption.Stroke.Bold , "stroke_bold" },
            };


        //[SerializeField]
        private Image image_fill, image_shadow, image_stroke;


        private void Awake()
        {
            if (Application.isPlaying)
            {
                Destroy(this);
            }
        }


        private Image init_child(string name)
        {
            var images = GetComponentsInChildren<Image>();
            if(images.Any(i => i.gameObject.name == name))
            {
                return images.First(i => i.gameObject.name == name);
            }

            var obj = new GameObject(name);
            obj.transform.SetParent(transform);

            var rect = obj.AddComponent<RectTransform>();
            rect.localPosition = Vector2.zero;
            rect.anchorMin = Vector2.zero;
            rect.anchorMax = Vector2.one;
            rect.sizeDelta = Vector2.zero;

            var image = obj.AddComponent<Image>();
            image.type = Image.Type.Sliced;

            return image;
        }

        private Sprite LoadSprite(string path)
        {
            return Resources.Load<Sprite>(Path.Combine(root, set_list[theme], path));
        }

        private void SetFill()
        {
            image_fill = image_fill ? image_fill : init_child("fill");

            image_fill.sprite = LoadSprite("fill");
            image_fill.color = fill_color;
            image_fill.SetAllDirty();
        }

        private void SetShadow()
        {
            image_shadow = image_shadow ? image_shadow : init_child("shadow");

            image_shadow.enabled = use_shadow;
            image_shadow.sprite = LoadSprite(shadow_list[shadow]);
            image_shadow.SetAllDirty();
        }

        private void SetStroke()
        {
            image_stroke = image_stroke ? image_stroke : init_child("stroke");

            image_stroke.enabled = use_stroke;
            image_stroke.color = stroke_color;
            image_stroke.sprite = LoadSprite(stroke_list[stroke]);
            image_stroke.SetAllDirty();
        }

        public void Reload()
        {
            SetFill();
            SetShadow();
            SetStroke();
        }

        private void OnValidate()
        {
            Reload();
        }
#endif

    }
}
