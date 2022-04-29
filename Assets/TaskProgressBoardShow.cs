using UnityEngine;

public class TaskProgressBoardShow : MonoBehaviour
{
    public async void ShowTaskProgressBoard()
    {
        await TaskProgressBoard.ShowTaskProgressBoard();
    }
}
