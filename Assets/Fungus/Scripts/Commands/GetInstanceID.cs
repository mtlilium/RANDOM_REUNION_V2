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
    /// Invokes a method of a component via reflection. Supports passing multiple parameters and storing returned values in a Fungus variable.
    /// </summary>
    [CommandInfo("Valiable",
                 "Get InstanceID",
                 "GameObjectのInstanceIDを取得します")]
    [AddComponentMenu("")]
    public class GetInstanceID : Command
    {
        [SerializeField]
        [VariableProperty(typeof(IntegerVariable))]
        protected Variable inOutVar;

        [SerializeField] protected GameObject obj;

        public override void OnEnter()
        {
            var ioi = inOutVar as IntegerVariable;

            var flowChart = GetFlowchart();
            ioi.Value = obj.gameObject.GetInstanceID();
            Continue();
        }
    }
}
