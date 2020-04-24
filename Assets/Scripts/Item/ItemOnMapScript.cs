using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemOnMapScript : ObjectOnMapScript
{
    public ItemStack itemstack { get; set; }

    public new void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        sr.sprite = Sprite_Initial;
        Position = new MapCoordinate(MapCoordinate_Initial.x, MapCoordinate_Initial.y);
        itemstack = new ItemStack();
    }

    public void Merge(ItemOnMapScript ioms)
    {
        itemstack = ItemStack.Merge(itemstack, ioms.itemstack);
        Destroy(ioms.gameObject);
    }
}
