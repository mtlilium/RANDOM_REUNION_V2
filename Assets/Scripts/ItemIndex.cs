using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ItemIndex
{
  public static Sprite GetSprite(string itemname){
    return Resources.Load<Sprite>("Data/Sprite/"+itemname);
  }

  public static Item.KindOfItem GetKind(string itemname){
    return Item.KindOfItem.off;
  }
}
