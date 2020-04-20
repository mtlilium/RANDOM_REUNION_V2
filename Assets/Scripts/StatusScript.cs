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
        StartCoroutine(InvincibleEndCoroutine());
    }
    IEnumerator InvincibleEndCoroutine() {
        yield return new WaitForSeconds(1.0f);
        IsInvincible = false;
    }
    private void Update() {
        
    }

}
