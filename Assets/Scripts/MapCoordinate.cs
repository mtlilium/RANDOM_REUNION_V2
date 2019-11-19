using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCoordinate
{
    public float x;
    public float y;

    static public readonly float GridScale_Vertical   = 0.5f;
    static public readonly float GridScale_Horizontal = 1;

    public MapCoordinate(float _x, float _y)
    {
        x = _x;
        y = _y;
    }
    public MapCoordinate(Vector2 vec)
    {
        x = vec.x;
        y = vec.y;
    }

    public static MapCoordinate operator + (MapCoordinate lhs, MapCoordinate rhs)
    {
        return new MapCoordinate(lhs.x + rhs.x, lhs.y + rhs.y);
    }
    public static MapCoordinate operator - (MapCoordinate lhs, MapCoordinate rhs)
    {
        return new MapCoordinate(lhs.x - rhs.x, lhs.y - rhs.y);
    }
    public static explicit operator MapCoordinateInt(MapCoordinate operand)
    {
        return (MapCoordinateInt)(new MapCoordinate(operand.x, operand.y));
    }

    public Vector2 ToVector2()
    {
        return new Vector2(
            (-x + y) * GridScale_Horizontal / 2,
            (x + y) * GridScale_Vertical / 2
        );
    }
    public Vector2 ToVector2(bool onGrid)
    {
        if (onGrid)
        {
            return ToVector2();
        }
        else
        {
            return new Vector2(
                (-x + y) * GridScale_Horizontal / 2,
                (x + y + 1) * GridScale_Vertical / 2
            );
        }
    }

    public static MapCoordinate FromVector2(Vector2 vec)//Vector2から変換
    {
        return new MapCoordinate(
            vec.y / GridScale_Vertical - vec.x / GridScale_Horizontal,
            vec.y / GridScale_Vertical + vec.x / GridScale_Horizontal
            );
    }
    
    public new string ToString()
    {
        return $"{x},{y}";
    }
}

public class MapCoordinateInt
{
    public int x;
    public int y;

    public MapCoordinateInt(int _x, int _y)
    {
        x = _x;
        y = _y;
    }
    public MapCoordinateInt(float _x, float _y)
    {
        x = Mathf.RoundToInt(_x);
        y = Mathf.RoundToInt(_y);
    }
    public MapCoordinateInt(Vector2Int vec)
    {
        x = vec.x;
        y = vec.y;
    }
    public MapCoordinateInt(Vector2 vec)
    {
        x = Mathf.RoundToInt(vec.x);
        y = Mathf.RoundToInt(vec.y);
    }

    public static MapCoordinateInt operator +(MapCoordinateInt lhs, MapCoordinateInt rhs)
    {
        return new MapCoordinateInt(lhs.x + rhs.x, lhs.y + rhs.y);
    }
    public static MapCoordinateInt operator -(MapCoordinateInt lhs, MapCoordinateInt rhs)
    {
        return new MapCoordinateInt(lhs.x - rhs.x, lhs.y - rhs.y);
    }

    public static explicit operator MapCoordinate(MapCoordinateInt operand)
    {
        return new MapCoordinate(operand.x, operand.y);
    }

    public Vector2 ToVector2()
    {
        return ((MapCoordinate)this).ToVector2();
    }
    public Vector2 ToVector2(bool onGrid)
    {
        return ((MapCoordinate)this).ToVector2(onGrid);
    }
    public Vector2Int ToVector2Int()
    {
        return new Vector2Int(x, y);
    }
    public static MapCoordinateInt FromVector2(Vector2 vec)//Vector2から変換
    {
        return (MapCoordinateInt)(new MapCoordinate(vec.x, vec.y));
    }

    public new string ToString()
    {
        return $"{x},{y}";
    }
}