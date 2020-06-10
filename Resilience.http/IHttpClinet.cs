using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace Resilience.http
{
    public interface IHttpClinet
    {
        HttpResponseMessage Post<T>(string uri, T item, List<KeyValuePair<string, string>> hederParms = null, string authorizationToken = null, string authorizationMethon = "Bearer");

        HttpResponseMessage Get<T>(string uri, T item, List<KeyValuePair<string, string>> hederParms = null, string authorizationToken = null, string authorizationMethond = "Bearer");
        HttpResponseMessage Put<T>(string uri, T item, List<KeyValuePair<string, string>> hederParms = null, string authorizationToken = null, string authorizationMethond = "Bearer");
    }
}
