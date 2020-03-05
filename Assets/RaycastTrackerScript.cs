using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastTrackerScript: MonoBehaviour { 
    //destination 追いかける目的地 Raycastの投影目標
    [SerializeField]
    Transform dest;

    //Rayの最大到達距離
    [SerializeField]
    float serchRange=3.0f;

    //対象を発見中、この距離まで近づいたら行動する（敵なら「攻撃する」など）
    [SerializeField]
    float behaveRange=0.5f;

    //目的地に向かうスピード
    [SerializeField]
    float speed=0.02f;

    enum TrackingState {
        ROAMING,//歩き回ってる
        APPROACHING,//対象に近づく
        BEHAVING//行動する（敵なら「攻撃する」など）
    };
    TrackingState nowTrackingState;

    Dictionary<TrackingState, Action> stateToActionDict;

    private void Start() {
        stateToActionDict = new Dictionary<TrackingState, Action> {
            {TrackingState.ROAMING      ,Roam},
            {TrackingState.APPROACHING  ,Approach},
            {TrackingState.BEHAVING     ,Behave }
        };
    }
    private void Update() {
        if (RayHitToPlayer()) {
            nowTrackingState = TrackingState.APPROACHING;
            Vector2 hereVec2 = transform.position;
            Vector2 destVec2 = dest.position;
            if (Vector2.Distance(hereVec2, destVec2) <= behaveRange) {
                nowTrackingState = TrackingState.BEHAVING;
            }
        }
        else {
            nowTrackingState = TrackingState.ROAMING;
        }
        stateToActionDict[nowTrackingState]();
        
    }
    bool RayHitToPlayer() {
        Vector2 hereVec2 = transform.position;
        Vector2 destVec2 = dest.position;

        //当たり判定(circle collider)の半径
        float colliderRadius=GetComponent<CircleCollider2D>().radius;

        //circlecolliderの外側４点(上下左右)からRayを照射
        int[] dirX = { 0, 1, 0, -1 };
        int[] dirY = { 1, 0, -1, 0 };
        bool hitToPlayer = false;
        for (int i = 0; i < 4; i++) {
            Vector2 rayOrigin = hereVec2 + new Vector2(dirX[i], dirY[i])*(colliderRadius+Mathf.Epsilon);
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, destVec2 - rayOrigin, serchRange);
            if (hit.collider?.tag == "Player"){
                hitToPlayer = true;
            }
        }
        //どれか一つでもあたってれば追いかける
        return hitToPlayer;
    }

    //TrackingStateに対応する関数3つ(Roam,Approach,Behave)のうち、
    //Approachだけはオーバーライド不必要と判断
    protected virtual void Roam() {

    }
    void Approach() {
        Vector2 hereVec2 = transform.position;
        Vector2 destVec2 = dest.position;
        transform.position=hereVec2+(destVec2 - hereVec2).normalized * speed;
        //もしかしたら↓AddForce↓の方がいいかも
        //GetComponent<Rigidbody2D>().AddForce((destVec2 - hereVec2).normalized * speed);
    }
    protected virtual void Behave() {

    }
    

    private void OnDrawGizmosSelected() {
        Gizmos.color = new Color(0.8f, 0.3f, 0.3f, 0.8f);
        Gizmos.DrawWireSphere(transform.position, serchRange);
    }
}
