using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Polly;
using Polly.Wrap;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;

namespace Resilience.http
{
    /// <summary>
    /// StandardHttpClient
    /// </summary>
    public class StandardHttpClient : IHttpClinet
    { 
        private readonly ILogger<StandardHttpClient> _logger; //Microsoft.Extensions.Logging
        private IHttpContextAccessor _httpContextAccessor; //microsoft.aspnetcore.http.Abstractions
        private readonly HttpClient _httpClient;
        public StandardHttpClient(Func<string, IEnumerable<Policy>> policyCreator,
                                 ILogger<StandardHttpClient> logger,
                                 IHttpContextAccessor httpContextAccessor,
                                 HttpClient httpClient
                                 )
        {
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
            _httpClient = httpClient;

        }
        public HttpResponseMessage Post<T>(string uri, T item, List<KeyValuePair<string, string>> hederParms = null, string authorizationToken = null, string authorizationMethond = "Bearer")
        {
            return DoMethod(HttpMethod.Post, uri, item, hederParms, authorizationToken, authorizationMethond);
        }
        public HttpResponseMessage Get<T>(string uri, T item, List<KeyValuePair<string, string>> hederParms = null, string authorizationToken = null, string authorizationMethond = "Bearer")
        {
            return DoMethod(HttpMethod.Get, uri, item, hederParms, authorizationToken, authorizationMethond);
        }
        public HttpResponseMessage Put<T>(string uri, T item, List<KeyValuePair<string, string>> hederParms = null, string authorizationToken = null, string authorizationMethond = "Bearer")
        {
            return DoMethod(HttpMethod.Put, uri, item, hederParms, authorizationToken, authorizationMethond);
        }
        private HttpResponseMessage DoMethod<T>(HttpMethod method, string uri, T item, List<KeyValuePair<string, string>> hederParms = null, string authorizationToken = null, string authorizationMethond = "Bearer")
        {
            var origin = GetOriginFromUrl(uri);
            var requestMessage = new HttpRequestMessage(method, uri);
            //设置请求头
            SetAuthorizationHeader(requestMessage);
            if (hederParms != null)
            {
                foreach (var parm in hederParms)
                {
                    requestMessage.Headers.Remove(parm.Key);
                    requestMessage.Headers.Add(parm.Key, parm.Value);
                }
            }
            var requestData = JsonConvert.SerializeObject(item);
            requestMessage.Content = new StringContent(requestData, Encoding.UTF8, "application/json");
            if (authorizationToken != null)
            {
                requestMessage.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue(authorizationMethond, authorizationToken);
            }
            var response = _httpClient.SendAsync(requestMessage).Result;
            if (!response.IsSuccessStatusCode)
            {
                var msg = $"请求接口:{uri},接口参数:{ requestData},状态码:{response.StatusCode}";
                _logger.LogInformation(msg);
                //  throw new HttpRequestException(msg);
            }
            return response;
        }
        private static string NormalizeOrigin(string origin)
        {
            return origin?.Trim().ToLower();
        }
        private static string GetOriginFromUrl(string uri)
        {
            var url = new Uri(uri);
            var orgin = $"{url.Scheme}://{url.DnsSafeHost}:{url.Port}";
            return orgin;
        }
        /// <summary>
        /// 设置请求头
        /// </summary>
        /// <param name="requestMessage"></param>
        private void SetAuthorizationHeader(HttpRequestMessage requestMessage)
        {
            var authorizationHeader = _httpContextAccessor.HttpContext.Request.Headers["Authorization"];
            if (!string.IsNullOrEmpty(authorizationHeader))
            {
                requestMessage.Headers.Add("Authorization", new List<string>() { authorizationHeader });
            }
        }
    }
}
