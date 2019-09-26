using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TimeInGame
{
    public static int Current { get; private set; }
    
    public static void TimeUpdate()
    {
        Current++;
    }
}
