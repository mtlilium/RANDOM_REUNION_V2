using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item 
{
    public string ItemName { get; set; }
    public KindOfItem kind { get; set; }
    public int Weight { get; set; }
    public int Freshness { get; set; }
    public int DefaultSellingPrice { get; set; }
}
