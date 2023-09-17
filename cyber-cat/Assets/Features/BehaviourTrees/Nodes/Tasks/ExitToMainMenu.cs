using Bonsai;
using Bonsai.Core;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

[BonsaiNode("Tasks/")]
public class ExitToMainMenu : Task
{
    public override Status Run()
    {
        SceneManager.LoadSceneAsync("MainMenu").ToUniTask().Forget();
        return Status.Success;
    }
}