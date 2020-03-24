namespace WM.Infrastructure.Models
{
    public static partial class Extensions
    {
        /*
        /// <summary>
        /// HttpGet 
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="parameters">查询参数</param>
        /// <param name="hederParms">请求头参数</param>
        /// <returns></returns>
        public static string HttpGet(this HttpClient httpClient, string url, IDictionary<string, string> parameters = null, IDictionary<string, string> hederParms = null)
        {
            // 请求参数
            var param = BuildQuery(parameters, "utf-8");
            try
            {
                if (param.Length > 0) url = $"{url}?{param}";

                if (hederParms != null)
                {
                    foreach (var item in hederParms)
                    {
                        httpClient.DefaultRequestHeaders.Add(item.Key, item.Value);
                    }
                }

                var response = httpClient.GetAsync(url).Result;

                if (response.IsSuccessStatusCode)
                {
                    return response.Content.ReadAsStringAsync().Result;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"{DateTime.Now.ToDateTimeString()},请求{url}出现异常,输入参数：{param},异常信息:{ex.Message}");
            }
        }

        /// <summary>
        /// HttpPut 
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="rq">查询参数</param>
        /// <param name="hederParms">请求头参数</param>
        /// <returns></returns>
        public static string HttpPut(this HttpClient httpClient, string url, object rq, List<KeyValuePair<string, string>> hederParms = null)
        {
            var postData = JsonConvert.SerializeObject(rq);
            try
            {
                httpClient.Timeout = TimeSpan.FromSeconds(8);//默认8秒请求超时
                if (hederParms != null)
                {
                    foreach (var item in hederParms)
                    {
                        httpClient.DefaultRequestHeaders.Add(item.Key, item.Value);
                    }
                }
                HttpContent httpContent = new StringContent(postData);

                // 取消Expect协议头，防止请求卡住
                httpClient.DefaultRequestHeaders.ExpectContinue = false;

                httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                httpContent.Headers.ContentType.CharSet = "utf-8";

                var response = httpClient.PutAsync(url, httpContent).Result;

                if (response.IsSuccessStatusCode)
                {
                    return response.Content.ReadAsStringAsync().Result;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"{DateTime.Now.ToDateTimeString()},请求{url}出现异常,输入参数：{postData},异常信息:{ex.Message}");
            }
        }

        /// <summary>
        /// HttpPost
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="rq">查询参数</param>
        /// <param name="hederParms">请求头参数</param>
        /// <returns></returns>
        public static string HttpPost(this HttpClient httpClient, string url, object rq, List<KeyValuePair<string, string>> hederParms = null)
        {
            var postData = JsonConvert.SerializeObject(rq);
            try
            {
                httpClient.Timeout = TimeSpan.FromSeconds(8);//默认8秒请求超时
                if (hederParms != null)
                {
                    foreach (var item in hederParms)
                    {
                        httpClient.DefaultRequestHeaders.Add(item.Key, item.Value);
                    }
                }
                HttpContent httpContent = new StringContent(postData);

                // 取消Expect协议头，防止请求卡住
                httpClient.DefaultRequestHeaders.ExpectContinue = false;

                httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                httpContent.Headers.ContentType.CharSet = "utf-8";

                var response = httpClient.PostAsync(url, httpContent).Result;
                if (response.IsSuccessStatusCode)
                {
                    return response.Content.ReadAsStringAsync().Result;
                }
                return null;

            }
            catch (Exception ex)
            {
                throw new Exception($"{DateTime.Now.ToDateTimeString()},请求{url}出现异常,输入参数：{postData},异常信息:{ex.Message}");
            }
        }

        /// <summary>
        /// HttpPost
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="parameters">查询参数</param>
        /// <param name="hederParms">请求头参数</param>
        /// <returns></returns>
        public static string HttpDelete(this HttpClient httpClient, string url, IDictionary<string, string> parameters = null, List<KeyValuePair<string, string>> hederParms = null)
        {
            // 请求参数
            var param = BuildQuery(parameters, "utf-8");
            try
            {
                if (param.Length > 0) url = $"{url}?{param}";

                httpClient.Timeout = TimeSpan.FromSeconds(8);//默认8秒请求超时

                if (hederParms != null)
                {
                    foreach (var item in hederParms)
                    {
                        httpClient.DefaultRequestHeaders.Add(item.Key, item.Value);
                    }
                }

                // 取消Expect协议头，防止请求卡住
                httpClient.DefaultRequestHeaders.ExpectContinue = false;

                var response = httpClient.DeleteAsync(url).Result;

                if (response.IsSuccessStatusCode)
                {
                    return response.Content.ReadAsStringAsync().Result;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"{DateTime.Now.ToDateTimeString()},请求{url}出现异常,输入参数：{param},异常信息:{ex.Message}");
            }
        }


        /// <summary>
        /// 获取客户Ip
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static string GetClientIP(this HttpContext context)
        {
            var ip = context.Request.Headers["X-Forwarded-For"].FirstOrDefault();
            if (string.IsNullOrEmpty(ip)) ip = context.Connection.RemoteIpAddress.ToString();
            return ip;
        }

        /// <summary>
        /// 获取请求Header
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static string GetHeader(this HttpRequest request, string key)
        {
            var header = request.Headers[key];
            if (header.Count == 0) return string.Empty;
            return header.ToString();
        }


        /// <summary>
        /// 生成 URL 查询参数字符串 例如：“id=100&name=Lucas”
        /// </summary>
        /// <param name="parameters">参数</param>
        /// <param name="encode">编码</param>
        /// <returns></returns>
        private static string BuildQuery(IDictionary<string, string> parameters, string encode)
        {
            StringBuilder postData = new StringBuilder();
            if (parameters == null) return postData.ToString();

            bool hasParam = false;
            IEnumerator<KeyValuePair<string, string>> dem = parameters.GetEnumerator();
            while (dem.MoveNext())
            {
                string name = dem.Current.Key;
                string value = dem.Current.Value;
                // 忽略参数名或参数值为空的参数
                if (!string.IsNullOrEmpty(name))//&& !string.IsNullOrEmpty(value)
                {
                    if (hasParam)
                    {
                        postData.Append("&");
                    }
                    postData.Append(name);
                    postData.Append("=");
                    if (encode == "gb2312")
                    {
                        postData.Append(System.Web.HttpUtility.UrlEncode(value, Encoding.GetEncoding("gb2312")));
                    }
                    else if (encode == "utf8")
                    {
                        postData.Append(System.Web.HttpUtility.UrlEncode(value, Encoding.UTF8));
                    }
                    else
                    {
                        postData.Append(value);
                    }
                    hasParam = true;
                }
            }

            return postData.ToString();
        }

        */
    }
}
