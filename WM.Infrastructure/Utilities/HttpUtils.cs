using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace WM.Infrastructure.Utilities
{
    public class HttpUtils
    {
        /// <summary>
        /// post请求到指定地址并获取返回的信息内容
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="postData">请求参数</param>
        /// <param name="encodeType">编码类型如：UTF-8</param>
        /// <returns>返回响应内容</returns>
        public static string HttpPost(string URL, string strPostdata, string strEncoding = "UTF-8")
        {
            Encoding encoding = Encoding.UTF8;
            HttpWebRequest request = (System.Net.HttpWebRequest)WebRequest.Create(URL);
            request.Method = "post";
            request.Accept = "text/html, application/xhtml+xml, */*";
            request.ContentType = "application/x-www-form-urlencoded";
            byte[] buffer = encoding.GetBytes(strPostdata);
            request.ContentLength = buffer.Length;
            request.GetRequestStream().Write(buffer, 0, buffer.Length);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            using (StreamReader reader = new StreamReader(response.GetResponseStream(), System.Text.Encoding.GetEncoding(strEncoding)))
            {
                return reader.ReadToEnd();
            }
        }

        /// <summary>
        /// post请求到指定地址并获取返回的信息内容
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="postData">请求参数</param>
        /// <param name="encodeType">编码类型如：UTF-8</param>
        /// <returns>返回响应内容</returns>
        public static string HttpPost_JSON(string URL, string strPostdata, string strEncoding = "UTF-8")
        {
            Encoding encoding = Encoding.UTF8;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(URL);
            request.Method = "post";
            request.Accept = "text/html, application/xhtml+xml, */*";
            request.ContentType = "application/json";
            byte[] buffer = encoding.GetBytes(strPostdata);
            request.ContentLength = buffer.Length;
            request.GetRequestStream().Write(buffer, 0, buffer.Length);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            using (StreamReader reader = new StreamReader(response.GetResponseStream(), System.Text.Encoding.GetEncoding(strEncoding)))
            {
                return reader.ReadToEnd();
            }
        }

        /// <summary>
        /// GBK Post请求
        /// </summary>
        /// <param name="Url"></param>
        /// <param name="postDataStr"></param>
        /// <returns></returns>
        public static string HttpPost_GBK(string Url, string postDataStr)
        {
            System.GC.Collect();//垃圾回收，回收没有正常关闭的http连接

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);
            request.Method = "POST";

            request.ContentType = "application/x-www-form-urlencoded";
            // request.ContentType = "application/xml;charset=utf-8";
            // request.ContentLength = postDataStr.Length;  

            StreamWriter writer = new StreamWriter(request.GetRequestStream(), Encoding.GetEncoding("GBK"));

            writer.Write(postDataStr);
            writer.Flush();
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            string encoding = response.ContentEncoding;
            if (encoding == null || encoding.Length < 1)
            {
                //encoding = "UTF-8"; //默认编码  
                encoding = "GBK"; //默认编码  
            }
            StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding("GBK"));
            string retString = reader.ReadToEnd();
            return retString;
        }

        /// <summary>
        /// get请求数据
        /// </summary>
        /// <param name="url"></param>
        /// <param name="data"></param>
        public static string Get(string url, string postData)
        {
            var returnStr = string.Empty;

            var urlstr = url + postData;
            var request = (HttpWebRequest)WebRequest.Create(urlstr);
            request.Timeout = 50000;
            request.Method = "GET";
            request.ContentType = "application/x-www-form-urlencoded";

            using (var response = (HttpWebResponse)request.GetResponse())
            {
                var stream = response.GetResponseStream();
                var reader = new StreamReader(stream, Encoding.UTF8);
                returnStr = reader.ReadToEnd();
                reader.Close();
                response.Close();
            }

            return returnStr;
        }

        #region 判断远程文件是否存在
        //请求次数
        private static int count = 1;
        /// <summary>
        /// 判断远程文件是否存在
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static bool JudgeFileExists(string url)
        {
            try
            {
                // 创建根据网络地址的请求对象
                var httpWebRequest = (HttpWebRequest)WebRequest.CreateDefault(new Uri(url));
                httpWebRequest.Method = "HEAD";
                httpWebRequest.Timeout = 1000;
                // 返回响应状态是否是成功比较的布尔值
                if (((HttpWebResponse)httpWebRequest.GetResponse()).StatusCode == HttpStatusCode.OK)
                {
                    count = 1;
                    return true;
                }
            }
            catch (Exception ex)
            {
                //Thread.Sleep(3000);
                if (count <= 3)
                {
                    if (JudgeFileExists(url))
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        #endregion

    }
}
