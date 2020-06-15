using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using WM.Infrastructure.Config;
using WM.Infrastructure.Enums;
using WM.Infrastructure.Extensions;
using WM.Infrastructure.Extensions.AutofacManager;
using WM.Infrastructure.Localization;
using WM.Infrastructure.Models;
using WM.Infrastructure.UserManager;
using X.Models.WMDB;

namespace WM.Service.App
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TRepository"></typeparam>
    public abstract class BaseSerivceDomain<T, TRepository> : IDependency
        where TRepository : X.IRespository.DBSession.IWMDBSession
        where T : class, new()
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
          var key=entityType.GetKeyProperty().Name;
            PageGridData<T> pageGridData = new PageGridData<T>();

            //ValidatePageOptions
            var where = new StringBuilder($" (Enable=1 or Enable=0) ");
            var order = new StringBuilder($"{key} desc");
            if (!string.IsNullOrWhiteSpace(options.Sort))//默认排序字段
            {
                var property = entityType.GetProperty(options.Sort);
                if (property != null)
                {
                    options.Order ??= "desc";
                    if (!(options.Order.ToLower() == "desc" || options.Order.ToLower() == "asc")) options.Order = "desc";
                    order = new StringBuilder($"{property.Name}  {options.Order}");
                }
            }
            if (!string.IsNullOrWhiteSpace(options.Wheres))
            {
                try
                {
                    var searchParametersList = options.Wheres.DeserializeObject<List<SearchParameters>>();
                    //判断列的数据类型数字，日期的需要判断值的格式是否正确
                    for (int i = 0; i < searchParametersList.Count; i++)
                    {
                        SearchParameters x = searchParametersList[i];
                        x.DisplayType = x.DisplayType.GetDBCondition();
                        if (string.IsNullOrEmpty(x.Value))
                        {
                            continue;
                        }
                        PropertyInfo property = entityType.GetProperties().Where(c => c.Name.ToUpper() == x.Name.ToUpper()).FirstOrDefault();
                        if (property != null)
                        {
                            //property.PropertyType == typeof(string)
                            where.Append($" and {property.Name}='{x.Value}' ");
                        }
                    }
                }
                catch { }
            }

            var query = new StringBuilder($"*");
            int totalCount = 0;
            var list = repository.SqlQueryWithPage<T>(new PageModel
            {
                Table = tableName,
                Order = order.ToString(),
                Where = where.ToString(),
                Query = query.ToString(),
                Pageindex = options.Page,
                PageSize = options.Rows,
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
            if (saveModel.MainData.ContainsKey("modifier")) saveModel.MainData.Remove("modifier");
            if (saveModel.MainData.ContainsKey("modifyDate")) saveModel.MainData.Remove("modifyDate");
            saveModel.MainData.Add("modifier", userInfo.UserName);
            saveModel.MainData.Add("modifyDate", DateTime.Now);


            T mainEntity = saveModel.MainData.DicToEntity<T>();
         // var list= entityType.GetEditField().Where(c => saveModel.MainData.Keys.Contains(c)).ToArray();
            Response.Status = repository.Update(mainEntity);
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
            if (saveModel == null
               || saveModel.MainData == null
               || saveModel.MainData.Count == 0)
                return Response.Set(ResponseType.ParametersLack, false);

            Type entityType = typeof(T);
            //string validReslut = entityType.ValidateDicInEntity(saveModel.MainData, true);
            //if (!string.IsNullOrEmpty(validReslut)) return Response.Error(validReslut);
            var keyProperty = entityType.GetKeyProperty();
            if (keyProperty == null) return Response.Error(ResponseType.KeyError);
            //查找key
            var tKey = keyProperty.Name;
            if (string.IsNullOrEmpty(tKey))
                return Response;

            //判断key类型是否为uuid
            if (keyProperty.PropertyType == typeof(string)|| keyProperty.PropertyType == typeof(Guid))
            {
                saveModel.MainData.Add(keyProperty.Name, Guid.NewGuid());
            }
            else
            {
                saveModel.MainData.Remove(keyProperty.Name);
            }

            //设置修改时间,修改人的默认值
            var userInfo = UserContext.Current.UserInfo;
            // 设置默认字段的值"CreateID", "Creator", "CreateDate"，"ModifyID", "Modifier", "ModifyDate"
            if(saveModel.MainData.ContainsKey("creator")) saveModel.MainData.Remove("creator");
            if (saveModel.MainData.ContainsKey("createDate")) saveModel.MainData.Remove("createDate");
            saveModel.MainData.Add("creator", userInfo.UserName);
            saveModel.MainData.Add("createDate", DateTime.Now);

            T mainEntity = saveModel.MainData.DicToEntity<T>();

            Response.Status = repository.Add(mainEntity);
            saveModel.MainData[keyProperty.Name] = keyProperty.GetValue(mainEntity);
            if (Response.Status) return Response.OK(ResponseType.SaveSuccess);
            return Response;
        }


        /// <summary>
        /// 获取用户权限
        /// </summary>
        /// <param name="role_Id"></param>
        /// <returns></returns>
        public List<Permissions> GetPermissions(int role_Id)
        {
            var where = string.Empty;
            var list = new List<Permissions>();
            if (UserContext.IsRoleIdSuperAdmin(role_Id))
            {
                list = repository.Sys_Menu.Where(q => q.Enable == (byte)EnumDataStatus.Enable)
                                  .Select(q => new Permissions
                                  {
                                      Menu_Id = q.Menu_Id,
                                      Menu_Name = q.MenuName,
                                      ParentId = q.ParentId,
                                      TableName = q.TableName,
                                      MenuAuth = q.Auth,
                                      UserAuth = q.Auth,
                                  }).ToList();
                list.ForEach(x =>
                {
                    try
                    {
                        x.UserAuthArr = string.IsNullOrEmpty(x.UserAuth)
                          ? new string[0]
                          : x.UserAuth.DeserializeObject<List<Sys_Actions>>().Select(s => s.Value).ToArray();

                    }
                    catch { }
                    finally
                    {
                        if (x.UserAuthArr == null)
                        {
                            x.UserAuthArr = new string[0];
                        }
                    }
                });
            }
            else
            {
                list = repository.Sql_Query<Permissions>($@"SELECT a.Menu_Id,a.ParentId,a.MenuName as Menu_Name,a.TableName,a.Auth as MenuAuth, b.AuthValue as UserAuth from Sys_Menu a
                                                            INNER JOIN Sys_RoleAuth b
                                                            on a.Menu_Id = b.Menu_Id
                                                             where a.Enable = 1  and b.Role_Id = {role_Id}");

                list.ForEach(x =>
                {
                    try
                    {
                        x.UserAuthArr = string.IsNullOrEmpty(x.UserAuth)
                       ? new string[0]
                       : x.UserAuth.Split(",");
                    }
                    catch { }
                    finally
                    {
                        if (x.UserAuthArr == null)
                        {
                            x.UserAuthArr = new string[0];
                        }
                    }
                });
            }
            list.ForEach(x =>
            {
                try
                {
                    x.TableName = (x.TableName ?? "").ToLower();

                    x.Actions = string.IsNullOrEmpty(x.MenuAuth)
                      ? new List<Sys_Actions>()
                      : x.MenuAuth.DeserializeObject<List<Sys_Actions>>().ToList();

                }
                catch { }
                finally
                {
                    if (x.Actions == null) x.Actions = new List<Sys_Actions>();
                }
                x.Actions = x.Actions.Where(q => x.UserAuthArr.Contains(q.Value)).ToList();
            });
            return list;
        }
    }
}
