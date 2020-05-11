using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PalameterScript : MonoBehaviour
{
    [SerializeField]
    int hp;//初期値はInspectorから

    AudioClipPlayer audioClipPlayer;
    private void Start() {
        audioClipPlayer = GetComponent<AudioClipPlayer>();
    }
    public void GainDamage(int damage) {
        //audioClipPlayer.Play("damage");
        hp-=damage;
        Debug.Log(damage + "ダメージ");
    }
    public bool IsDefeat() {
        return hp <= 0;
    }
}
