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

    public static MapCoordinate operator + (MapCoordinate lhs, MapCoordinate rhs)
    {
        return new MapCoordinate(lhs.x + rhs.x, lhs.y + rhs.y);
    }
    public static MapCoordinate operator - (MapCoordinate lhs, MapCoordinate rhs)
    {
        return new MapCoordinate(lhs.x - rhs.x, lhs.y - rhs.y);
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
