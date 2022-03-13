using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SyncCatFromEditorWin : MonoBehaviour
{

    void Start()
    {
        StartCoroutine(WaitForLoadScene());
    }
    IEnumerator WaitForLoadScene()
    {
        yield return new WaitForEndOfFrame();
        CatPositionSaver.LoadAndSync();
        yield return null;
    }
}
