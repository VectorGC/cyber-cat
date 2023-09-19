using System.Net;
using System.Threading.Tasks;
using NUnit.Framework;
#if UNITY_WEBGL
using ApiGateway.Client.Internal.WebClientAdapters.UnityWebRequest;
#endif

namespace ApiGateway.Client.Tests.Extensions
{
    public class AssertAsync
    {
        public static async Task ThrowsWebException(AsyncTestDelegate testDelegate, HttpStatusCode expected)
        {
            try
            {
                await testDelegate.Invoke();
            }
            catch (WebException exception)
            {
#if UNITY_WEBGL
                var response = (UnityWebException) exception;
                var statusCode = response.StatusCode;
#else
                var response = (HttpWebResponse) exception.Response;
                if (response == null)
                {
                    Assert.Fail($"Expected an web exception with status code {expected} but thrown '{exception}'.");
                    return;
                }

                var statusCode = response.StatusCode;
#endif
                if (statusCode == expected)
                {
                    //Ok! Swallow the exception.
                    return;
                }

                Assert.Fail($"Expected an web exception with status code '{expected}' but thrown with status code {statusCode}.");
                return;
            }

            Assert.Fail($"Expected an web exception with status code '{expected}' but no exception was thrown.");
        }
    }
}