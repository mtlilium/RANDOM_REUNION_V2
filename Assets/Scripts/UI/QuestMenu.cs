using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public static class QuestMenu {
    static DS.UI.Tab tab;

    public static void Init(DS.UI.Tab _tab) {
        tab = _tab;
        QuestHeaderGenerator.Init(tab.headerContainer.transform);
        QuestDetailGenerator.Init(tab.contentContainer.transform);
    }
    public static void AddNewQuestUI(QuestInfo info) {
        QuestHeaderGenerator.Generate(info.displayName);
        QuestDetailGenerator.Generate(info);
        tab.LinkTabHeader();
    }

    public static void EnCleared(string name) {
        QuestHeaderGenerator.EnCleared(name);
    }

    public static class QuestHeaderGenerator {
        static Transform parentTransform;
        static Dictionary<string, GameObject> nowValidHeader = null;
        const int headerPosX = 280;
        const int headerOffset = 20;
        public static void Init(Transform parent) {
            parentTransform = parent;
            nowValidHeader = new Dictionary<string, GameObject>();
        }
        public static void Generate(string displayName) {
            GameObject obj = GameObject.Instantiate(QuestDatabase.questHeaderPrefab, parentTransform);
            var text = obj.GetComponentInChildren<Text>();
            text.text = displayName;
            var rect = obj.GetComponent<RectTransform>();
            var array = nowValidHeader.ToArray();
            float topPosY = -120;
            if (array.Length > 0) { //既にあるheaderの中で一番上の座標を見て、その上になるように
                float minY = float.PositiveInfinity;
                foreach (float posY in nowValidHeader.Values.Select(x => x.GetComponent<RectTransform>().sizeDelta.y)) {
                    minY = Mathf.Min(minY, posY);
                }
                topPosY = minY;
            }
            rect.anchoredPosition = new Vector2(headerPosX, topPosY + (rect.sizeDelta.y + headerOffset));
            nowValidHeader.Add(displayName, obj);
        }
        public static void EnCleared(string displayName) {
            nowValidHeader[displayName].GetComponent<Image>().color = Color.black;
        }
    }

    public static class QuestDetailGenerator {
        static Transform parentTransform;
        public static void Init(Transform parent) {
            parentTransform = parent;
        }
        public static void Generate(QuestInfo info) {
            GameObject obj = GameObject.Instantiate(QuestDatabase.questDetailPrefab, parentTransform);
            var detailBehaviour = obj.GetComponent<QuestDetail_Behaviour>();
            if (detailBehaviour == null) {
                Debug.LogError("QuestDetail_Behaviour.GenerateDetailで、QuestDetail_Behaivourがアタッチされていないprefabを生成しようとしました");
                return;
            }
            var textList = obj.GetComponentsInChildren<UnityEngine.UI.Text>();
            foreach (var text in textList) {
                switch (text.gameObject.name) {
                    case "name":
                        text.text = info.displayName;
                        break;
                    case "target":
                        text.text = info.target;
                        break;
                    case "place":
                        text.text = info.place;
                        break;
                    case "reward":
                        text.text = info.reward;
                        break;
                    case "detail":
                        text.text = info.detail;
                        break;
                    default: break;
                }
            }
            //detailBehaviour.RemainingTime = info.deadLine - TimeInGame.Current;
        }
    }

}
