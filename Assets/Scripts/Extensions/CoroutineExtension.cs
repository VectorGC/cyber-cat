using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CoroutineExtension
{
    public static void StartCoroutine(this IEnumerator routine)
    {
        CoroutineUtil.StartCoroutine(routine);
    }
    
    public static void StartCoroutine(this YieldInstruction yieldInstruction)
    {
        CoroutineUtil.StartCoroutine(yieldInstruction);
    }
    
    private class CoroutineUtil : MonoBehaviour
    {
        private static CoroutineUtil _instance;

        private static MonoBehaviour GetInstance()
        {
            if (!_instance)
            {
                var coroutineContainer = new GameObject("CoroutineContainer");
                coroutineContainer = Instantiate(coroutineContainer);
                _instance = coroutineContainer.AddComponent<CoroutineUtil>();
            }

            return _instance;
        }

        public new static void StartCoroutine(IEnumerator routine)
        {
            GetInstance().StartCoroutine(routine);
        }
    
        public static void StartCoroutine(YieldInstruction yieldInstruction)
        {
            GetInstance().StartCoroutine(StartYieldInstruction(yieldInstruction));
        }

        private static IEnumerator StartYieldInstruction(YieldInstruction yieldInstruction)
        {
            yield return yieldInstruction;
        }
    }
}