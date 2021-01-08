using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DS.UI
{
    public class UIContent : MonoBehaviour
    {
        public ContentInfo.Index index;
    }

    public class ContentInfo
    {
        public class Index
        {
            public int previous;
            public int start, end;
            public int selected, self;

            public bool IsTop() => start == self;
            public bool IsBottom() => end == self;
            public bool IsSelected() => selected == self;

            public int RelativeIndex() => self - selected;
            public int ChangedIndex() => selected - previous;
            public int RelativeIndexSign() => (RelativeIndex() == 0) ? 0 : (int)Mathf.Sign(RelativeIndex());
        }

        public Index index;
        public GameObject gameObject;
    }
}
