using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectOnMapScript : MonoBehaviour
{
    public Sprite Sprite_Initial = null;
    public Vector2 MapCoordinate_Initial;
    
    protected SpriteRenderer sr;

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        sr.sprite = Sprite_Initial;
        transform.position = (new MapCoordinate(MapCoordinate_Initial.x, MapCoordinate_Initial.y)).ToVector2();
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
