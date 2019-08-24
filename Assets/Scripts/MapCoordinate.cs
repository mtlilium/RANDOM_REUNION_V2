using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCoordinate{
    public float x;
    public float y;

    static public readonly int CHIP_WIDTH = 192;
    static public readonly int CHIP_HEIGHT = 64;

    public static int ConstOfPositionTransform { get; } = 192;

    public MapCoordinate(float _x, float _y)
    {
        x = _x;
        y = _y;
    }
    public MapCoordinate(Vector2 v)
    {
        x = v.x;
        y = v.y;
    }

    public static MapCoordinate operator + (MapCoordinate lhs, MapCoordinate rhs)
    {
        return new MapCoordinate(lhs.x + rhs.x, lhs.y + rhs.y);
    }
    public static MapCoordinate operator - (MapCoordinate lhs, MapCoordinate rhs)
    {
        return new MapCoordinate(lhs.x - rhs.x, lhs.y - rhs.y);
    }

    public Vector2 ToVector2(){//Vector2に変換
        return new Vector2(
            (-x + y)/ (ConstOfPositionTransform / CHIP_WIDTH),
            ( x + y)/ (ConstOfPositionTransform / CHIP_HEIGHT)
        );
    }
    public static MapCoordinate FromVector2(Vector2 vec)//Vector2から変換
    {
        return new MapCoordinate(
            (vec.y / CHIP_HEIGHT - vec.x / CHIP_WIDTH) * ConstOfPositionTransform / 2,
            (vec.y / CHIP_HEIGHT + vec.x / CHIP_WIDTH) * ConstOfPositionTransform / 2
            );
    }

    public float Depth(){//マップチップの標準的な深さに変換
        return x + y;
    }
    public Vector3 ToVector3(){//Vector3に変換
        return new Vector3(
            (-x + y) / (ConstOfPositionTransform / CHIP_WIDTH),
            (x + y) / (ConstOfPositionTransform / CHIP_HEIGHT),
             Depth()
        );
    }

    public new string ToString()
    {
        return $"{x},{y}";
    }
}
