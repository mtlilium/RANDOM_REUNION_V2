using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class System_Behaviour : MonoBehaviour
{
    [SerializeField]
    QuestManager_Behaviour questManager;
    void Start(){
        questManager = transform.Find("QuestManager").GetComponent<QuestManager_Behaviour>();
    }
    bool f = true;
    void Update(){
        if (f) {
            f = false;
            questManager.QuestAccept("Test_MouseSubjugation");
        }
    }
}
