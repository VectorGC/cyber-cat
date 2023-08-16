using JetBrains.Annotations;
using Pada1.BBCore;
using Pada1.BBCore.Framework;
using Pada1.BBCore.Tasks;
using UnityEngine;

namespace BehaviourNodes.Actions.Basic
{
    [UsedImplicitly]
    [Action("DebugLog")]
    [Help("Prints a message (param Text) on the Unity Console output.")]
    public class DebugLog : BasePrimitiveAction
    {
        [InParam("Text", DefaultValue = "Default Message")]
        [Help("Message to be written")]
        public string Message { get; set; }

        public override TaskStatus OnUpdate()
        {
            Debug.Log(Message);
            return TaskStatus.COMPLETED;
        }
    }
}