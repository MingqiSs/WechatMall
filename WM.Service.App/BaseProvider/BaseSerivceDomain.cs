using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Text;
using WM.Infrastructure.Config;
using WM.Infrastructure.Enums;
using WM.Infrastructure.Extensions;
using WM.Infrastructure.Extensions.AutofacManager;
using WM.Infrastructure.Localization;
using WM.Infrastructure.Models;
using WM.Infrastructure.UserManager;

namespace WM.Service.App
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TRepository"></typeparam>
    public abstract class BaseSerivceDomain<T, TRepository> : IDependency
        where TRepository : X.IRespository.DBSession.IWMDBSession
        //where T : BaseEntity
    {
        /// <summary>
        /// 
        /// </summary>
        protected X.IRespository.DBSession.IWMDBSession repository;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="repository"></param>
        public BaseSerivceDomain(TRepository repository)
        {
            Response = new WebResponseContent(true);
            this.repository = repository;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="R"></typeparam>
        /// <param name="inData"></param>
        /// <returns></returns>
        public ResultDto<R> Result<R>(R inData)
        {
            return new ResultDto<R>(inData);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="R"></typeparam>
        /// <param name="inData"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public ResultDto<R> Result<R>(R inData, string msg)
        {
            return new ResultDto<R>(inData) { Msg = msg };
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="R"></typeparam>
        /// <param name="inData"></param>
        /// <param name="code"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public ResultDto<R> Result<R>(R inData, ResponseCode code, string msg)
        {
            return new ResultDto<R>(inData) { Ec = code, Msg = msg };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="code"></param>
        /// <param name="errorMsg"></param>
        /// <returns></returns>
        public ResultDto<R> Result<R>(ResponseCode code, string errorMsg)
        {
            return new ResultDto<R>(code, errorMsg);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="errorMsg"></param>
        /// <returns></returns>
        public ResultDto<R> Result<R>(Exception ex, string errorMsg = "")
        {
            return new ResultDto<R>(ResponseCode.sys_exception, errorMsg);
        }
        /// <summary>
        /// 获取 <paramref name="name"/> 对应的本地化字符串。
        /// </summary>
        /// <param name="name">本地化资源的名称。</param>
        /// <returns>返回本地化字符串。</returns>
        public static string L(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return name;
            try
            {
                var Language = WM.Infrastructure.Utilities.HttpContext.Current.Request.Headers["Language"].ToString();
                LocalizationHelper.InitializeLanguage(Language);
                var value = LocalizationHelper.GetString(name);
                return value;
            }
            catch (Exception ex)
            {
                var logger = AutofacContainerModule.GetService<ILogger<BaseSerivce<TRepository>>>();
                logger?.LogError(ex, $"多语言名称[{name}]未找到!");
                //写日志
            }
            return name;
        }

        public WebResponseContent Response { get; set; }
        /// <summary>
        /// 通用单表数据删除
        /// </summary>
        /// <param name="options"></param>
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
            string sql = $"update {tableName} set Enable=2 where {tKey} in ({joinKeys});";

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
            var where = new StringBuilder($" Enable=1");
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
        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="saveModel"></param>
        /// <returns></returns>
        public virtual WebResponseContent Update(SaveModel saveModel)
        {
            if (saveModel == null)
                return Response.Error(ResponseType.ParametersLack);
            if (saveModel.MainData.Count <= 1) return Response.Error("系统没有配置好编辑的数据，请检查model!");
            Type entityType = typeof(T);
            var keyProperty = entityType.GetKeyProperty();
            if (keyProperty == null) return Response.Error(ResponseType.KeyError);
            //查找key
            var tKey = keyProperty.Name;
            if (string.IsNullOrEmpty(tKey))
                return Response;
            //设置修改时间,修改人的默认值
            var userInfo = UserContext.Current.UserInfo;
            // 设置默认字段的值"CreateID", "Creator", "CreateDate"，"ModifyID", "Modifier", "ModifyDate"
            saveModel.MainData.Add("Modifier", userInfo.UserName);
            saveModel.MainData.Add("ModifyDate", DateTime.Now);
            T mainEntity = saveModel.MainData.DicToEntity<T>();

          var list= entityType.GetEditField().Where(c => saveModel.MainData.Keys.Contains(c)).ToArray();
            // repository.Sys_User.Update(q => q.UserName = saveModel.MainData.Keys["userName"]);
            //var isSave= repository.DB.Updateable<X.Models.WMDB.Sys_User>(q=>q.).ExecuteCommand()>0;
            string sql = @$"update {entityType.Name} set  where {tKey}";

            return Response;
        }
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="saveModel"></param>
        /// <returns></returns>
        public virtual WebResponseContent Add(SaveModel saveModel)
        {
            if (saveModel == null)
                return Response.Error(ResponseType.ParametersLack);
            if (saveModel.MainData.Count <= 1) return Response.Error("系统没有配置好编辑的数据，请检查model!");
            Type entityType = typeof(T);
            var keyProperty = entityType.GetKeyProperty();
            if (keyProperty == null) return Response.Error(ResponseType.KeyError);
            //查找key
            var tKey = keyProperty.Name;
            if (string.IsNullOrEmpty(tKey))
                return Response;
            //设置修改时间,修改人的默认值
            var userInfo = UserContext.Current.UserInfo;
            // 设置默认字段的值"CreateID", "Creator", "CreateDate"，"ModifyID", "Modifier", "ModifyDate"
            saveModel.MainData.Add("Modifier", userInfo.UserName);
            saveModel.MainData.Add("ModifyDate", DateTime.Now);
            T mainEntity = saveModel.MainData.DicToEntity<T>();

            var list = entityType.GetEditField().Where(c => saveModel.MainData.Keys.Contains(c)).ToArray();
            // repository.Sys_User.Update(q => q.UserName = saveModel.MainData.Keys["userName"]);
            //var isSave= repository.DB.Updateable<X.Models.WMDB.Sys_User>(q=>q.).ExecuteCommand()>0;
            string sql = @$"update {entityType.Name} set  where {tKey}";

            return Response;
        }
    }
}
