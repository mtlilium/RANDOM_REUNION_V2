using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using UnityEngine;

public class PersonScript : MovableObjectScript
{
    [SerializeField]
    public string PersonalName;
    [SerializeField]
    public CircleCollider2D InteractionTrigger;
    protected PersonalInteraction personalInteraction;
    //public ItemStack inventory { get; private set;}

    public new void Awake()
    {
        GetComponent<MovableObjectScript>().Awake();
        Sprite_Initial = Sprite_Down;
        personalInteraction = new PersonalInteraction(InteractionTrigger);
        ActionsWhenDirecrionChanged += personalInteraction.DirectionalUpdate;
    }
    new private void Update() {
        base.Update();
    }
}

public class PersonalInteraction
{
    CircleCollider2D InteractionTrigger;
    public PersonalInteraction(CircleCollider2D interactionTrigger) {
        InteractionTrigger = interactionTrigger;
    }

    static Vector2 OffsetNormal = new Vector2(0, -0.25f);
    static Dictionary<directions, Vector2> OffsetByDirection = new Dictionary<directions, Vector2>() {
        { directions.Down     ,new Vector2(      0f,-0.3333f)},
        { directions.DownLeft ,new Vector2(-0.4243f,-0.2121f)},
        { directions.DownRight,new Vector2( 0.4243f,-0.2121f)},
        { directions.Left     ,new Vector2(   -0.6f,      0f)},
        { directions.Right    ,new Vector2(    0.6f,      0f)},
        { directions.Up       ,new Vector2(      0f, 0.3333f)},
        { directions.UpLeft   ,new Vector2(-0.4243f, 0.2121f)},
        { directions.UpRight  ,new Vector2( 0.4243f, 0.2121f)},
        { directions.Undefined,new Vector2(      0f,      0f)},
    };

    public void DirectionalUpdate(directions dir)
    {
        if(dir!=directions.Undefined)
            InteractionTrigger.offset = OffsetByDirection[dir] + OffsetNormal;
    }
}
