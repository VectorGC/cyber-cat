using System.Net;
using NUnit.Framework;

namespace ApiGateway.Client.Tests.Extensions
{
    public static class AssertExtensions
    {
        public static void AreEqual(this WebException webException, HttpStatusCode expected)
        {
            var response = (HttpWebResponse) webException.Response;
            Assert.AreEqual(expected, response.StatusCode);
        }
    }
}