using Legacy_do_not_use_it;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;

public class LoadCodeEditor : MonoBehaviour
{
    [SerializeField] private TaskUnitFolder taskFolder;

    public void Load()
    {
        CodeEditor.OpenSolution(taskFolder).Forget();
    }
}
