using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;

public static class DebugLogWrapper{
    [Conditional("UNITY_EDITOR")]
    public static void Log(object message) {
        UnityEngine.Debug.Log(message);
    }
    public static void LogError(object message) {
        UnityEngine.Debug.LogError(message);
    }
}
