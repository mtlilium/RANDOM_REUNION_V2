using System.Collections;
using System.Collections.Generic;
using UnityEngine;

static public class ObjectGenerator{
    static public void GenerateRandomAtInnerMap(GameObject prefab,InnerMap_Behaviour innerMap) {
        var spotManager = innerMap.GetComponent<SpawnSpotManager>();
        var circleCollider = prefab.GetComponent<CircleCollider2D>();
        if (circleCollider == null) {
            Debug.LogError("CircleCollider2DのないPrefabをランダム生成することはできません(offsetをどうしたらいいか分からないので)");
            return;
        }
        float radius = circleCollider.radius;
        var spotList = new List<SpawnSpotManager.SpawnSpot>();
        foreach (var spot in spotManager.SpotList) {
            if (spot.radius > radius) {
                spotList.Add(spot);
            }
        }
        if (spotList.Count == 0) {
            Debug.LogError(prefab.name + "を生成するのに十分なspotがありません");
            return;
        }
        int randomIndex = SystemClass.randGen.Next(spotList.Count);
        Vector2 pos = spotList[randomIndex].RandomPosition(radius);//コライダーの大きさ分余裕を持つ
        var obj = GameObject.Instantiate(prefab, pos, Quaternion.identity, innerMap.transform);//ランダムなspotのランダムな場所に生成する
        obj.name = prefab.name;// (Clone)を消す
    }
    static public void GenerateRandomAtInnnerMaps() {

    }
}
