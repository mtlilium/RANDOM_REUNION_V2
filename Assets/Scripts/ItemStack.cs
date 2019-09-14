using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ItemStack : ObjectOnMapScript
{
  LinkedList<Item> itemStack=new LinkedList<Item>();
  public int allWeight{
    set{
      foreach(Item e in ItemStack){
        allWeight+=e.Weight;
      }
    }
    get{
      return allWeight;
    }
  }
  public static void Merge(){

  }

}
