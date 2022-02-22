using TasksData.Requests;
using UniRx;
using UnityEngine;

namespace WebRequests
{
    public class SendWebRequestBehaviour : MonoBehaviour
    {
        public void SendGetRequest()
        {
            new GetTasksRequest()
                .SendRequest() // <--- Отправляет запрос. Без этого запрос не отправится.
                .Subscribe(taskData =>
                {
                    Debug.Log(
                        "Поставь сюда точку останову и посмотри как с сервера приходят доступные таски в taskData :)");
                });
        }
    }
}