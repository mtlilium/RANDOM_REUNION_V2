using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectOnMapScript : MonoBehaviour
{
    public Sprite Sprite_Initial = null;
    public Vector2 Position_Initial;

    public bool IsOnMap = true;

    protected SpriteRenderer sr;

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        sr.sprite = Sprite_Initial;
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
