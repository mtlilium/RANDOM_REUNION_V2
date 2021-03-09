using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using UnityEngine;

public class MovableObjectScript : ObjectOnMapScript{
    Rigidbody2D rb2d;
    [SerializeField]
    float movement = 0.1f;
    directions _state;
    directions state {
        get { return _state; }
        set
        {
            if (value == directions.Undefined)
                return;
            //if (stateDic[value] != null)
                // if(Time.timeScale!=0)
                //    sr.sprite = stateDic[value];
            _state = value;
        }
    }
    [SerializeField]
    bool moveAnimated=false;

    Animator animator;

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

    Dictionary<directions, Vector2> stateToXYDic = new Dictionary<directions, Vector2>();

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
        sr.sprite = stateDic[state];

        
        ActionsWhenDirecrionChanged += LookAtDirection;
        if (moveAnimated) {
            animator = GetComponent<Animator>();
            ActionsWhenDirecrionChanged += (dir) => {
                if (dir != directions.Undefined) animator.CrossFadeInFixedTime(Enum.GetName(typeof(directions), dir), 0);
            };
            stateToXYDic.Add(directions.Up,         new Vector2( 0, 1));
            stateToXYDic.Add(directions.UpRight,    new Vector2( 1, 1));
            stateToXYDic.Add(directions.Right,      new Vector2( 1, 0));
            stateToXYDic.Add(directions.DownRight,  new Vector2( 1,-1));
            stateToXYDic.Add(directions.Down,       new Vector2( 0,-1));
            stateToXYDic.Add(directions.DownLeft,   new Vector2(-1,-1));
            stateToXYDic.Add(directions.Left,       new Vector2(-1, 0));
            stateToXYDic.Add(directions.UpLeft,     new Vector2(-1, 1));
        }
        else {
            ActionsWhenDirecrionChanged += (newDir) => {
                if (Time.timeScale != 0 && newDir != directions.Undefined) sr.sprite = stateDic[newDir];
            };
        }
    }

    bool moved = false;
    bool isIdle = false;
    protected void Update() {
        if (moveAnimated) {
            if (moved) {
                isIdle = false;
            }
            else if (!isIdle) {
                animator.CrossFadeInFixedTime("Idle_Blend", 0);
                isIdle = true;
            }
            moved = false;
        }
    }

    void Move(Vector2 direction)//引数の方向に移動に移動量movementだけ移動
    {
        Move(direction, movement);
    }
    public void Move(Vector2 direction, float q)//引数の方向に移動量Qだけ移動
    {
        Vector2 normalizedDir = direction.normalized;
        rb2d.MovePosition(rb2d.position + normalizedDir * q);
        directions dir = DirectionOfDeltaPos(direction);
        if (state!=dir) ActionsWhenDirecrionChanged(dir);
        if (moveAnimated) {
            if (direction != Vector2.zero) moved = true; //動いていたらtrue 動いてない場合にanimationを停止させるための処理
            if (dir != directions.Undefined) {
                Vector2 laseMove = stateToXYDic[dir];
                animator.SetFloat("LastMoveX", laseMove.x);
                animator.SetFloat("LastMoveY", laseMove.y);
                if (isIdle) {
                    animator.CrossFadeInFixedTime("Walk_Blend", 0);
                }
            }
        }
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