using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ItemStack
{
    public int Weight { get; private set; }
    List<Item> itemStack = new List<Item>();

    public void Add(Item item)
    {
        itemStack.Add(item);
        Weight += item.Weight;
    }
    public void AddRange(IEnumerable<Item> ie)
    {
        foreach (var x in ie)
            Add(x);
    }

    public void Remove(Item item)
    {
        itemStack.Remove(item);
        Weight -= item.Weight;
    }
    public void RemoveAt(int i)
    {
        Item item = itemStack[i];
        Weight -= item.Weight;
        itemStack.Remove(item);
    }
    public static ItemStack Merge(ItemStack s1, ItemStack s2)
    {
        ItemStack s3 = new ItemStack();
        s3.AddRange(s1.itemStack);
        s3.AddRange(s2.itemStack);
        return s3;
    }
}
