using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusScript : MonoBehaviour
{
    Animator invincibleAnimator;
    private void Start() {
        invincibleAnimator = this.GetComponent<Animator>();
    }
    public bool IsInvincible {//無敵状態かどうか
        get;
        private set;
    }
    public void EnInvincible() {
        IsInvincible = true;
        invincibleAnimator?.SetTrigger("Invincible");
        StartCoroutine(InvincibleEndCoroutine());
    }
    IEnumerator InvincibleEndCoroutine() {
        yield return new WaitForSeconds(1.0f);
        IsInvincible = false;
    }

}
