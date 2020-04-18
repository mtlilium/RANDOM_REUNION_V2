using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using UnityEngine;

public class MovableObjectScript : ObjectOnMapScript
{
    Rigidbody2D rb2d;

    float movement = 0.1f;
    directions _state;
    directions state {
        get { return _state; }
        set
        {
            if (value == directions.Undefined)
                return;
            if(stateDic[value] != null)
                sr.sprite = stateDic[value];
            _state = value;
        }
    }

    public directions State_Initial = directions.Down;

    public Sprite Sprite_Up        = null;
    public Sprite Sprite_UpLeft    = null;
    public Sprite Sprite_UpRight   = null;
    public Sprite Sprite_Left      = null;
    public Sprite Sprite_Right     = null;
    public Sprite Sprite_Down      = null;
    public Sprite Sprite_DownLeft  = null;
    public Sprite Sprite_DownRight = null;

    Dictionary<directions, Sprite> stateDic = new Dictionary<directions, Sprite>();

    public Action<directions> ActionsWhenDirecrionChanged;

    public new void Awake()
    {
        GetComponent<ObjectOnMapScript>().Awake();
        rb2d = gameObject.GetComponent<Rigidbody2D>();

        stateDic.Add(directions.Up, Sprite_Up);
        stateDic.Add(directions.UpLeft, Sprite_UpLeft);
        stateDic.Add(directions.UpRight, Sprite_UpRight);
        stateDic.Add(directions.Left, Sprite_Left);
        stateDic.Add(directions.Right, Sprite_Right);
        stateDic.Add(directions.Down, Sprite_Down);
        stateDic.Add(directions.DownLeft, Sprite_DownLeft);
        stateDic.Add(directions.DownRight, Sprite_DownRight);

        state = directions.Down;

        ActionsWhenDirecrionChanged += LookAtDirection;
    }

    private void Start()
    {

    }

    void Move(Vector2 direction)//引数の方向に移動に移動量movementだけ移動
    {
        Move(direction, movement);
    }
    public void Move(Vector2 direction, float q)//引数の方向に移動量Qだけ移動
    {
        rb2d.MovePosition(rb2d.position + direction.normalized * q);
        ActionsWhenDirecrionChanged(DirectionOfDeltaPos(direction));
    }
    
    public void Move(MapCoordinate mapcoordinate)//MapCoordinateのToVector2の方向に移動量movementだけ移動
    {
        Move(mapcoordinate.ToVector2());
    }
    public void Move(MapCoordinate mapcoordinate, float q)//MapCoordinateのToVector2の方向に移動量Qだけ移動
    {
        Move(mapcoordinate.ToVector2(),q);
    }

    public void LookAtDirection(directions direction)//その方向を見る
    {
        state = direction;
    }
    directions DirectionOfDeltaPos(Vector2 vec) {
        if (vec == Vector2.zero) return directions.Undefined;
        //vecにz座標0を付け足したものとupベクトルとの外積でz成分が負なら、vecとupベクトルとの角度も負
        bool radIsPlus = (Vector3.Cross((Vector3)vec, Vector3.up)).z > 0;

        float angle = Vector2.Angle(Vector2.up, vec);

        if (angle < 30.0) return directions.Up;
        if (angle < 75.0) {
            if (radIsPlus)  return directions.UpRight;
            else            return directions.UpLeft;
        }
        if (angle < 105.0) {
            if (radIsPlus)  return directions.Right;
            else            return directions.Left;
        }
        if (angle < 135.0) {
            if (radIsPlus)  return directions.DownRight;
            else            return directions.DownLeft;
        }
        return directions.Down;
    }
    public directions DirectionOfDeltaPos(MapCoordinate d)
    {
        if (d.y > 0)
        {
            if (d.x == 0)
                return directions.UpRight;
            else if (d.x < 0)
                return directions.Right;
            else
                return directions.Up;
        }
        else if (d.y == 0)
        {
            if (d.x < 0)
                return directions.DownRight;
            else if (d.x > 0)
                return directions.UpLeft;
            else
                return directions.Undefined;
        }
        else
        {
            if (d.x == 0)
                return directions.DownLeft;
            else if (d.x < 0)
                return directions.Down;
            else
                return directions.Left;
        }
    }
}

public enum directions
{
    Up,
    UpLeft,
    UpRight,
    Left,
    Right,
    Down,
    DownLeft,
    DownRight,
    Undefined
}