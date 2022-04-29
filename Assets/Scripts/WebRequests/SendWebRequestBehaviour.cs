using TasksData.Requests;
using UniRx;
using UnityEngine;
using WebRequests.Requesters;

namespace WebRequests
{
    public class SendWebRequestBehaviour : MonoBehaviour
    {
        public void SendGetRequest()
        {
            new GetTasksRequest()
                .SendWWWGetObject() // <--- Отправляет запрос. Без этого запрос не отправится.
                .Subscribe(taskData =>
                {
                    Debug.Log(
                        "Поставь сюда точку останову и посмотри как с сервера приходят доступные таски в taskData :)");
                });
        }
    }
}