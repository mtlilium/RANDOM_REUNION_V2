using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyManager_Behaviour : MonoBehaviour
{
    public Dictionary<string, Action> WhenEnemyDefeated { get; private set; }//Enemy側から呼び出す
    public Dictionary<string, GameObject> enemyNameToPrefab { get; private set; }//
    // Start is called before the first frame update
    private void Awake() {//Startより前にやっておきたい
        SystemClass.enemyManager = this;
    }
    void Start(){
        WhenEnemyDefeated = new Dictionary<string, Action>();
        enemyNameToPrefab = new Dictionary<string, GameObject>();
        var prefabs = Resources.LoadAll<GameObject>("EnemyPrefab/");
        foreach (GameObject prefab in prefabs) {
            enemyNameToPrefab[prefab.name] = prefab;
        }
    }

    public void EnemyGenerateInRandomSpot(string enemyName,int amount,SpawnSpotManager spotManager,Transform parent) {
        if (!enemyNameToPrefab.ContainsKey(enemyName)) {
            Debug.LogError(enemyName+"という名前のEnemyが見つかりません");
            return;
        }
        var prefab = enemyNameToPrefab[enemyName];
        float? colliderRadius = prefab.GetComponent<CircleCollider2D>()?.radius;
        if (colliderRadius == null) {
            Debug.LogError("CircleCollider2DのないPrefabをEnemyとしてランダム生成することはできません(offsetをどうしたらいいか分からないので)");
            return;
        }
        List<SpawnSpotManager.SpawnSpot> spotList=new List<SpawnSpotManager.SpawnSpot>();
        foreach(var spot in spotManager.SpotList) {
            if (spot.radius > colliderRadius) {
                spotList.Add(spot);
            }
        }
        if (spotList.Count == 0) {
            Debug.LogError(enemyName + "を生成するのに十分なspotがありません");
            return;
        }
        for(int count = 0; count < amount; count++) {
            int randomIndex = SystemClass.randGen.Next(spotList.Count);
            Vector2 pos = spotList[randomIndex].RandomPosition(prefab.GetComponent<CircleCollider2D>().radius);//コライダーの大きさ分余裕を持つ
            var obj=Instantiate(prefab, pos, Quaternion.identity, parent);//ランダムなspotのランダムな場所に生成する
            obj.name = enemyName;
        }
    }

    void Update(){
    }
}
