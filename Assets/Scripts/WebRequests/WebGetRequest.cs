using System;
using System.Collections.Specialized;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using ServerData;
using UnityEngine;
using WebRequests.Observers;

namespace WebRequests
{
    public class SendWebRequestBehaviour : MonoBehaviour
    {
        [SerializeField] private string query;

        public void SendGetRequest()
        {
            var getTaskRequest = new GetTasksRequest();
            getTaskRequest.SendGetRequest<AvailableTasksData>(str =>
            {
                var t = 10;
            });
        }
    }
}