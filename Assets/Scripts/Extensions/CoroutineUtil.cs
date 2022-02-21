using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CoroutineExtension
{
    public static void StartCoroutine(this IEnumerator routine)
    {
        CoroutineUtil.StartCoroutine(routine);
    }

    public static void StartCoroutine(this YieldInstruction yieldInstruction, Action callback = null)
    {
        CoroutineUtil.StartCoroutine(yieldInstruction, callback);
    }

    private class CoroutineUtil : MonoBehaviour
    {
        private static CoroutineUtil _instance;

        private static MonoBehaviour GetInstance()
        {
            if (_instance)
            {
                return _instance;
            }

            var coroutineContainer = new GameObject(nameof(CoroutineUtil));
            _instance = coroutineContainer.AddComponent<CoroutineUtil>();

            return _instance;
        }

        public new static void StartCoroutine(IEnumerator routine)
        {
            GetInstance().StartCoroutine(routine);
        }

        public static void StartCoroutine(YieldInstruction yieldInstruction, Action callback = null)
        {
            GetInstance().StartCoroutine(StartYieldInstruction(yieldInstruction, callback));
        }

        private static IEnumerator StartYieldInstruction(YieldInstruction yieldInstruction, Action callback = null)
        {
            yield return yieldInstruction;
            callback?.Invoke();
        }
    }
}