using ServerData;
using UnityEngine;

namespace WebRequests
{
    public class SendWebRequestBehaviour : MonoBehaviour
    {
        public void SendGetRequest()
        {
            var getTaskRequest = new GetTasksRequest();
            getTaskRequest.SendGetRequest<AvailableTasksData>(taskData =>
            {
                Debug.Log("Поставь сюда точку останову и посмотри как с сервера приходят доступные таски :)");
            });
        }
    }
}