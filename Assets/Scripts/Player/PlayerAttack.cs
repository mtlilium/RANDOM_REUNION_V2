using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour {
    Animator atackAnimator;
    // Start is called before the first frame update
    void Start(){
        atackAnimator = transform.Find("playerAttack").GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update(){
        if (Input.GetButtonDown("Fire1")) {
            atackAnimator.SetTrigger("AttackTrigger");
        }
    }
}
