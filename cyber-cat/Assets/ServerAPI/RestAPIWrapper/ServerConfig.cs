using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RestAPIWrapper
{
    public static class ServerConfig
    {
        public static APIVersion VersionOfAPI { get; private set; } = APIVersion.V1;

        public enum APIVersion
        {
            Serverless,
            V1,
            V2,
        }
    }
}

