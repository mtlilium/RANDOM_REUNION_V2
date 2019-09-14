using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    string ItemName;
    enum KindOfItem{
      important,
      food
    }
    int Weight;
    int Freshness;
    int DefaultSellingPrice;
}
