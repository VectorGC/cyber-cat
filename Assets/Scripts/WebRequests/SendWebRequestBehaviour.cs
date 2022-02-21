using UnityEngine;
using WebRequests.Extensions;
using WebRequests.Requests.GetTasksData;

namespace WebRequests
{
    public class SendWebRequestBehaviour : MonoBehaviour
    {
        public void SendGetRequest()
        {
            new GetTasksRequest()
                .OnResponse(taskData =>
                {
                    Debug.Log(
                        "Поставь сюда точку останову и посмотри как с сервера приходят доступные таски в taskData :)");
                })
                .SendRequest(); // <--- Отправляет запрос. Без этого запрос не отправится.
        }
    }
}