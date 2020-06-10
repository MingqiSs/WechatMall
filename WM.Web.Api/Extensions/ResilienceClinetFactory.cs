using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Polly;
using Resilience.http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace WM.Web.Api.Extensions
{
    /// <summary>
    /// 
    /// </summary>
    public class ResilienceClinetFactory
    {
        private readonly ILogger<ResilienceHttpClient> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly HttpClient _httpClient;
        private readonly int _retryCount;
        private readonly int _exceptionCount;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="httpContextAccessor"></param>
        /// <param name="retryCount"></param>
        /// <param name="exceptionCount"></param>
        public ResilienceClinetFactory(ILogger<ResilienceHttpClient> logger,
                                           IHttpContextAccessor httpContextAccessor,
                                           HttpClient httpClient,
                                          int retryCount,
                                          int exceptionCount)
        {
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
            _httpClient = httpClient;
            _retryCount = retryCount;
            _exceptionCount = exceptionCount;
        }
        /// <summary>
        /// 构建HttpClient
        /// </summary>
        /// <returns></returns>
        public ResilienceHttpClient GetResilienceHttpClient() =>
          new ResilienceHttpClient(origin => CreatePolicy(origin), _logger, _httpContextAccessor, _httpClient);
        /// <summary>
        /// 创建Policy
        /// </summary>
        /// <param name="origin"></param>
        /// <returns></returns>
        private Policy[] CreatePolicy(string origin)
        {
            return new Policy[] {
                Policy.Handle<HttpRequestException>()
                .WaitAndRetry(_retryCount,
                              retyAttempt=>TimeSpan.FromSeconds(2),//Math.Pow(2,retyAttempt)
                              (exception,timeSpan,retryCount,context)=>
                              {
                                  var msg=$"第{retryCount}次重试"+
                                  $"of {context.PolicyKey},{exception}";
                                  _logger.LogError(exception,msg);
                              }),
                Policy.Handle<HttpRequestException>()
                .CircuitBreaker(_exceptionCount,TimeSpan.FromMinutes(1),(exception,duratuon)=>{
                    _logger.LogError("熔断开启");
                },()=>{
                      _logger.LogError("熔断重置");
                })
            };
        }
    }
}
