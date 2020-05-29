using System;
using System.Collections.Generic;
using System.Text;
using WM.Infrastructure.Enums;
using WM.Infrastructure.Extensions;
using WM.Infrastructure.Extensions.AutofacManager;
using WM.Infrastructure.Models;
using X.Models.WMDB;

namespace WM.Service.App
{
    public abstract class BaseSerivceDomain<T, TRepository> : IDependency
        where TRepository : X.IRespository.DBSession.IWMDBSession
       //where T : BaseEntity
    {
        protected X.IRespository.DBSession.IWMDBSession repository;
        public BaseSerivceDomain(TRepository repository)
        {
            Response = new WebResponseContent(true);
            this.repository = repository;
        }
        private WebResponseContent Response { get; set; }
        /// <summary>
        /// 通用单表数据删除
        /// </summary>
        /// <param name="keys"></param>
        /// <returns></returns>
        public virtual WebResponseContent Del(KeyOptions options)
        {
            Type entityType = typeof(T);
            var tableName = entityType.Name;
            var keyProperty = entityType.GetKeyProperty();
            if (keyProperty == null || options == null || options.Key.Count == 0) return Response.Error(ResponseType.KeyError);
            //查找key
            var tKey = keyProperty.Name;
            if (string.IsNullOrEmpty(tKey))
                return Response;
            //判断条件
            FieldType fieldType = entityType.GetFieldType();
            string joinKeys = (fieldType == FieldType.Int || fieldType == FieldType.BigInt)
                 ? string.Join(",", options.Key)
                 : $"'{string.Join("','", options.Key)}'";
            //逻辑删除
            string sql = $"update {tableName} set DataStatus=3 where {tKey} in ({joinKeys});";

            Response.Status = repository.Sql_ExecuteCommand(sql) > 0;
            if (Response.Status && string.IsNullOrEmpty(Response.Message)) Response.OK(ResponseType.DelSuccess);
            return Response;
        }
        /// <summary>
        ///通用分页查询
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        public virtual PageGridData<T> GetPageData(PageDataOptions options)
        {
            Type entityType = typeof(T);
            var tableName = entityType.Name;
            PageGridData<T> pageGridData = new PageGridData<T>();
            var where = new StringBuilder($" DataStatus=1");
            var order = new StringBuilder($" Sort desc");
            var query = new StringBuilder($"*");
            int totalCount = 0;
            var list = repository.SqlQueryWithPage<T>(new PageModel
            {
                Table = tableName,
                Order = order.ToString(),
                Where = where.ToString(),
                Query = query.ToString(),
                Pageindex = 1,
                PageSize = 10,
            }, ref totalCount);
            pageGridData.rows = list;
            pageGridData.total = totalCount;
            return pageGridData;
        }
    }
    
}
