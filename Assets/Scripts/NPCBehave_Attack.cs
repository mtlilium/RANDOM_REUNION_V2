using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCBehave_Attack : NPCBehavior
{
    [SerializeField]
    Animator anim;
    public override void Init(){
        anim.SetTrigger("AttackTrigger");
    }
    public override void Exit() {
        anim.SetTrigger("StopTrigger");
    }
}
