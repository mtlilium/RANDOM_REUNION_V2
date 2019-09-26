using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemOnMapScript : ObjectOnMapScript
{
    public ItemStack itemstack = new ItemStack();

    public void Merge(ItemOnMapScript ioms)
    {
        itemstack = ItemStack.Merge(itemstack, ioms.itemstack);
        Destroy(ioms.gameObject);
    }
}
