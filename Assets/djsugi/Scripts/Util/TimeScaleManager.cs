using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DS.Util
{
    public class TimeScaleManager : EnvironmentMonoBehaviour<TimeScaleManager>
    {
        private float timeScale;
        private int nest = 0;

        public bool IsPausing()
        {
            return nest > 0;
        }

        public void Pause()
        {
            Debug.Log($"[Time Scale] pause");
            if (nest <= 0)
            {
                timeScale = Time.timeScale;
            }
            nest++;
            Time.timeScale = 0;
        }

        public void Restart()
        {
            Debug.Log($"[Time Scale] restart");
            nest--;
            if (nest <= 0)
            {
                Time.timeScale = timeScale;
                nest = 0;
            }
        }

        public void ForceRestart()
        {
            Debug.Log($"[Time Scale] force restart");
            nest = 0;
            Time.timeScale = timeScale;
        }
    }
}
