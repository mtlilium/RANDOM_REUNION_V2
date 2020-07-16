using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSpotManager : MonoBehaviour
{
    public struct SpawnSpot {
        public Vector2 position;
        public float radius;
        public Vector2 RandomPosition(float offset) {//offset分半径を縮めた円の中からランダムな点を返す
            float r = (radius-offset)*(float)SystemClass.randGen.NextDouble();
            float angle = (float)SystemClass.randGen.NextDouble() * 360f-180f;//-180°~180°の範囲にするため180引く
            return position +(Vector2)(Quaternion.Euler(0,0,angle) * (Vector2.up * r));
        }
    }
    public List<SpawnSpot> SpotList { get; private set;}
    void Awake() { 
        SpotList = new List<SpawnSpot>();
        Transform parentTransform=transform.Find("SpawnSpots");
        foreach (var tF in parentTransform.GetComponentsInChildren<Transform>(false)) {//check 本来はSpawnSpotのGameObjectは見えないように非Activeにしておくが、デバッグ中一時的に機能させたくない時に非アクティブを使えるよう、falseにしてある
            if (tF == parentTransform) continue;
            SpotList.Add(new SpawnSpot { position = tF.position, radius = tF.lossyScale.x });//元のSpriteが半径1の円なので、scaleがそのまま半径になる
        }
        this.gameObject.SetActive(false);//SpotMangerが外から参照されるので、初期化のためにAwakeだけ呼ぶ
    }
}
