using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConversationPerson2Person : ConversationClass
{
    void OnTriggerEnter2D(Collider2D other)
    {
        OnTrigger(other);
    }

    void OnTrigger(Collider2D other)
    {
        if (other.tag == "Player")
        {
            StaticConversationControll.Action(ConversationName);
        }
    }
}
