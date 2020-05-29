using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace WM.Infrastructure.Extensions
{
  public static  class ConvertJsonExtension
    {
        /// <summary>
        /// 序列化
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="formatDate"></param>
        /// <returns></returns>
        public static string Serialize(this object obj, JsonSerializerSettings formatDate = null)
        {
            if (obj == null) return null;
            formatDate = formatDate ?? new JsonSerializerSettings
            {
                DateFormatString = "yyyy-MM-dd HH:mm:ss"
            };

            return JsonConvert.SerializeObject(obj, formatDate);
        }

    }
}
