using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace WM.Service.App.Interface.System
{
   public interface ISys_DictionaryService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dicNos"></param>
        /// <returns></returns>
        Task<object> GetVueDictionary(string[] dicNos);
    }
}
