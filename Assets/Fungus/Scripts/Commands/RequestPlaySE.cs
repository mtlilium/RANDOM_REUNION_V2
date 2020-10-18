// This code is part of the Fungus library (http://fungusgames.com) maintained by Chris Gregan (http://twitter.com/gofungus).
// It is released for free under the MIT open source license (https://github.com/snozbot/fungus/blob/master/LICENSE)

using UnityEngine;
using System.Collections;
using System.Reflection;
using System.Collections.Generic;
using System;
using UnityEngine.Events;
using MarkerMetro.Unity.WinLegacy.Reflection;

namespace Fungus
{
    /// <summary>
    /// SE を鳴らします. RANDOM REUNION 仕様 (2020.10.18)
    /// </summary>
    [CommandInfo("Audio",
                 "RequestPlaySE",
                 "SE を鳴らします. RANDOM REUNION 仕様 (2020.10.18)")]
    [AddComponentMenu("")]
    public class RequestPlaySE : Command
    {
        [SerializeField] protected AudioClip seClip;
        [SerializeField] protected bool isLoop = false;
        [SerializeField] protected bool isEvent = false;
        [SerializeField] protected float delay = 0.0f;

        public override void OnEnter()
        {
            AudioManager.SingletonInstance.RequestPlaySE(this.transform.position, seClip, this.gameObject.GetInstanceID(), delay, isLoop, isEvent);
            Continue();
        }
    }
}
