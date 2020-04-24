using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PalameterScript : MonoBehaviour
{
    [SerializeField]
    int hp;//初期値はInspectorから
    public void GainDamage(int damage) {
        hp-=damage;
        Debug.Log(damage + "ダメージ");
    }
    public bool IsDefeat() {
        return hp <= 0;
    }
}
