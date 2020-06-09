using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WM.Infrastructure.Config;
using WM.Infrastructure.Dapper;
using WM.Infrastructure.DBManager;
using WM.Infrastructure.Extensions;
using WM.Infrastructure.Extensions.AutofacManager;
using WM.Infrastructure.Models;

namespace WM.Infrastructure.UserManager
{
    public class UserContext : DapperBaseMysql
    {
        public static string ConnectionString = AppSetting.GetConfig("ConnectionStrings:WMDB");
        public override string NewConnect => ConnectionString;

        /// 为了尽量减少redis或Memory读取,保证执行效率,将UserContext注入到DI，
        /// </summary>
        public static UserContext Current
        {
            get
            {
                return Context.RequestServices.GetService(typeof(UserContext)) as UserContext;
            }
        }

        private static Microsoft.AspNetCore.Http.HttpContext Context
        {
            get
            {
                return Utilities.HttpContext.Current;
            }
        }
        public AdminUser UserInfo
        {
            get
            {
                return Context.User.GetAdminToken();
            }
        }
        /// <summary>
        /// 角色ID为1的默认为超级管理员
        /// </summary>
        public bool IsSuperAdmin
        {
            get { return IsRoleIdSuperAdmin(RoleId); }
        }
        /// <summary>
        /// 角色ID为1的默认为超级管理员
        /// </summary>
        public static bool IsRoleIdSuperAdmin(int roleId)
        {
            return roleId == 1;
        }
        public string UserName
        {
            get { return UserInfo.UserName; }
        }

        public string UserTrueName
        {
            get { return UserInfo.UserTrueName; }
        }

        public string Token
        {
            get { return UserInfo.Token; }
        }

        public int RoleId
        {
            get { return UserInfo.Role_Id; }
        }

        /// <summary>
        /// 获取用户权限
        /// </summary>
        /// <returns></returns>
        public List<Permissions> GetPermissions(int role_Id)
        {
            var where = string.Empty;
            var list = new List<Permissions>();
            if (IsRoleIdSuperAdmin(role_Id))
            {
                using (var conn = GetOpenConnection())
                {
                    list = conn.Query<Permissions>($@"SELECT a.Menu_Id,a.ParentId,a.MenuName as Menu_Name,a.TableName,a.Auth as MenuAuth, a.Auth as UserAuth from Sys_Menu a
                                                             where a.Enable = 1 ").ToList();
                }
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
                using (var conn = GetOpenConnection())
                {
                    list= conn.Query<Permissions>($@"SELECT a.Menu_Id,a.ParentId,a.MenuName as Menu_Name,a.TableName,a.Auth as MenuAuth, b.AuthValue as UserAuth from Sys_Menu a
                                                            INNER JOIN Sys_RoleAuth b
                                                            on a.Menu_Id = b.Menu_Id
                                                             where a.Enable = 1  and b.Role_Id = {role_Id}").ToList();
                }
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
