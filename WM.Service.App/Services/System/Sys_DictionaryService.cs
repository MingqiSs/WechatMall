using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WM.Service.App.Interface.System;
using X.Models.WMDB;

namespace WM.Service.App.Services.System
{
    /// <summary>
    /// 
    /// </summary>
    public class Sys_DictionaryService : BaseSerivceDomain<Sys_Dictionary, X.IRespository.DBSession.IWMDBSession>, ISys_DictionaryService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="repository"></param>
        public Sys_DictionaryService(X.IRespository.DBSession.IWMDBSession repository) : base(repository)
        {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dicNos"></param>
        /// <returns></returns>
        public async Task<object> GetVueDictionary(string[] dicNos)
        {
            if (dicNos == null || dicNos.Count() == 0) return new string[] { };
            var list = await repository.Sys_Dictionary.Where(q => q.Enable == 1).ToListAsync();
            var dicConfig = new List<Sys_Dictionary>();
            foreach (var dicNo in dicNos)
            {
                var dic = list.Where(q => q.DicNo == dicNo).FirstOrDefault();
                if (dic != null) dicConfig.Add(dic);
            }
            object GetSourceData(int dic_ID, string dbSql)
            {
                if (string.IsNullOrEmpty(dbSql))
                {
                    return repository.Sys_DictionaryList.Where(q => q.Dic_ID == dic_ID).Select(q => new
                    {
                        key = q.DicValue,
                        value = q.DicName,
                    }).ToList();
                }
                else
                {
                    return repository.Sql_Query<object>(dbSql);
                }
            };

            return dicConfig.Select(item => new
            {
                item.DicNo,
                item.Config,
                data = GetSourceData(item.Dic_ID, item.DbSql)
            }).ToList(); ;
        }
         
    }
}
