using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Web;
using Authentication;
using Extensions.RestClientExt;
using Observers;
using Proyecto26;
using Proyecto26.Common;
using TasksData;
using TasksData.Requests;
using UnityEditor;
using UnityEngine;
using WebRequests;
using WebRequests.Extensions;
using WebRequests.Requesters;

public class TestTokenReceiver : MonoBehaviour
{
    public const string DEFAULT_DOMAIN = "https://kee-reel.com/cyber-cat/login";
    public const string DEFAULT_DOMAIN2 = "https://kee-reel.com/cyber-cat";

    private static string GetUriDomain(IWebRequest webRequest)
    {
        if (webRequest is IGetUriHandler uriHandler)
        {
            return uriHandler.GetUriDomain();
        }

        return DEFAULT_DOMAIN;
    }

    public static Uri GetUri(NameValueCollection QueryParams)
    {
        var domain = DEFAULT_DOMAIN;
        var builder = new UriBuilder(domain)
        {
            Query = QueryParams.ToString()
        };

        return builder.Uri;
    }

    private async void Awake()
    {
        var login = "test123@gmail.com";
        var password = "123456qwer";

        var req = new RequestHelper
        {
            Uri = DEFAULT_DOMAIN,
            Params =
            {
                ["email"] = login,
                ["pass"] = password
            }
        };

        var token = await RestClient.Get<TokenSession>(req).ToUniTask();

        var request = new RequestHelper
        {
            Uri = DEFAULT_DOMAIN2,
            Params =
            {
                ["token"] = token
            }
        };

        await RestClient.Get<TasksData.TasksData>(request).ToUniTask();
    }
}