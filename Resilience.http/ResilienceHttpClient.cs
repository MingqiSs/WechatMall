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
    /// PollyHelp
    /// </summary>
    public class ResilienceHttpClient : IHttpClinet
    {  //poll创建器
        private readonly Func<string, IEnumerable<Policy>> _policyCreator; //Polly
        //polly缓存器
        private readonly ConcurrentDictionary<string, PolicyWrap> _policyWraps;//Polly.Wrap
        private readonly ILogger<ResilienceHttpClient> _logger; //Microsoft.Extensions.Logging
        private IHttpContextAccessor _httpContextAccessor; //microsoft.aspnetcore.http.Abstractions
        private readonly HttpClient _httpClient;
        public ResilienceHttpClient(Func<string, IEnumerable<Policy>> policyCreator,
                                 ILogger<ResilienceHttpClient> logger,
                                 IHttpContextAccessor httpContextAccessor,
                                 HttpClient httpClient
                                 )
        {
            _policyCreator = policyCreator;
            _policyWraps = new ConcurrentDictionary<string, PolicyWrap>();
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
            return HttpInVoker(origin, () =>
            {
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
                    //var =response.Content.ReadAsStringAsync();
                    var msg = $"请求接口:{uri},接口参数:{ requestData},状态码:{response.StatusCode}";
                    _logger.LogInformation(msg);
                    ////是否尝试重试
                    //if (isTryRetry)
                  throw new HttpRequestException(msg);
                }
                return response;
               
            });
        }
        private T HttpInVoker<T>(string origin, Func<T> action)
        {
            var normalizeOrigin = NormalizeOrigin(origin);
            if (!_policyWraps.TryGetValue(normalizeOrigin, out PolicyWrap policyWrap))
            {
                policyWrap = Policy.Wrap(_policyCreator(normalizeOrigin).ToArray());
                _policyWraps.TryAdd(normalizeOrigin, policyWrap);
            }
            return policyWrap.Execute(action);
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
            var authorizationHeader = _httpContextAccessor.HttpContext?.Request?.Headers["Authorization"];
            if (!string.IsNullOrEmpty(authorizationHeader))
            {
                requestMessage.Headers.Add("Authorization", new List<string>() { authorizationHeader });
            }
        }
    }
}
