using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusScript : MonoBehaviour
{
    public bool IsInvincible {//無敵状態かどうか
        get;
        private set;
    }
    public void EnInvincible() {
        IsInvincible = true;
    }
    
}
