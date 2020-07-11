using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestDetail_Behaviour : MonoBehaviour{
    
    public int RemainingTime {
        get => remainingTime;
        set {
            if (remainingTime != 0) {
                Debug.LogAssertion("remainingTimeが初期化以外で外部から更新されました");
            }
            else remainingTime = value;
        }
    }
    int remainingTime=0;
    public void UpdateDeadline() {
        remainingTime--;   
    }
}
