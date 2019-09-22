using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ItemStack : ObjectOnMapScript
{
  LinkedList<Item> itemStack=new LinkedList<Item>();
  public int allWeight{
    set{
      foreach(Item e in itemStack){
        allWeight+=e.Weight;
      }
    }
    get{
      return allWeight;
    }
  }
  public static Itemstack Merge(ItemStack bag1,Itemstack bag2){
    ItemStack bag3;
    foreach(Item e in bag1.itemStack){
      bag3.itemStack.Addfirst(e);
    }
    foreach(Item e in bag2.itemStack){
      bag3.itemStack.Addfirst(e);
    }
    return bag3;
  }

}
