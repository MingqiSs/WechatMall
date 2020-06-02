using System;
using System.Collections.Generic;
using System.Text;
using WM.Infrastructure.Extensions.AutofacManager;
using WM.Infrastructure.Models;

namespace WM.Infrastructure.UserManager
{
    public class UserContext
    {
        /// <summary>
        /// 为了尽量减少redis或Memory读取,保证执行效率,将UserContext注入到DI，
        /// 每个UserContext的属性至多读取一次redis或Memory缓存从而提高查询效率
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
        //private static ICacheService CacheService
        //{
        //    get { return GetService<ICacheService>(); }
        //}

        //private static T GetService<T>() where T : class
        //{
        //    return AutofacContainerModule.GetService<T>();
        //}
      
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
            get { return IsRoleIdSuperAdmin(this.RoleId); }
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
        public List<Permissions> GetPermissions(int roleId)
        {
            //if (IsRoleIdSuperAdmin(roleId))
            //{
            //    var permissions = DBServerProvider.DbContext.Set<Sys_Menu>().Where(x => x.Enable == 1).Select(a => new Permissions
            //    {
            //        Menu_Id = a.Menu_Id,
            //        ParentId = a.ParentId,
            //        //2020.05.06增加默认将表名转换成小写，权限验证时不再转换
            //        TableName = (a.TableName ?? "").ToLower(),
            //        //MenuAuth = a.Auth,
            //        UserAuth = a.Auth,
            //    }).ToList();
            //    return MenuActionToArray(permissions);
            //}
            //ICacheService cacheService = CacheService;
            //string roleKey = roleId.GetRoleIdKey();

            ////角色有缓存，并且当前服务器的角色版本号与redis/memory缓存角色的版本号相同直接返回静态对象角色权限
            //string currnetVeriosn = "";
            //if (rolePermissionsVersion.TryGetValue(roleId, out currnetVeriosn)
            //    && currnetVeriosn == cacheService.Get(roleKey))
            //{
            //    return rolePermissions.ContainsKey(roleId) ? rolePermissions[roleId] : new List<Permissions>();
            //}

            ////锁定每个角色，通过安全字典减少锁粒度，否则多个同时角色获取缓存会导致阻塞
            //object objId = objKeyValue.GetOrAdd(roleId.ToString(), new object());
            ////锁定每个角色
            //lock (objId)
            //{
            //    if (rolePermissionsVersion.TryGetValue(roleId, out currnetVeriosn)
            //        && currnetVeriosn == cacheService.Get(roleKey))
            //    {
            //        return rolePermissions.ContainsKey(roleId) ? rolePermissions[roleId] : new List<Permissions>();
            //    }

            //    //没有redis/memory缓存角色的版本号或与当前服务器的角色版本号不同时，刷新缓存
            //    var dbContext = DBServerProvider.DbContext;
            //    List<Permissions> _permissions = (from a in dbContext.Set<Sys_Menu>()
            //                                      join b in dbContext.Set<Sys_RoleAuth>()
            //                                      on a.Menu_Id equals b.Menu_Id
            //                                      where b.Role_Id == roleId //&& a.ParentId > 0
            //                                      && b.AuthValue != ""
            //                                      orderby a.ParentId
            //                                      select new Permissions
            //                                      {
            //                                          Menu_Id = a.Menu_Id,
            //                                          ParentId = a.ParentId,
            //                                          //2020.05.06增加默认将表名转换成小写，权限验证时不再转换
            //                                          TableName = (a.TableName ?? "").ToLower(),
            //                                          MenuAuth = a.Auth,
            //                                          UserAuth = b.AuthValue ?? ""
            //                                      }).ToList();
            //    ActionToArray(_permissions);
            //    string _version = cacheService.Get(roleKey);
            //    //生成一个唯一版本号标识
            //    if (_version == null)
            //    {
            //        _version = DateTime.Now.ToString("yyyyMMddHHMMssfff");
            //        //将版本号写入缓存
            //        cacheService.Add(roleKey, _version);
            //    }
            //    //刷新当前服务器角色的权限
            //    rolePermissions[roleId] = _permissions;

            //    //写入当前服务器的角色最新版本号
            //    rolePermissionsVersion[roleId] = _version;
            //    return _permissions;
            //}
            return new List<Permissions>(); 
        }
            //public void LogOut(int userId)
            //{
            //   // CacheService.Remove(userId.GetUserIdKey());
            //}
        }
}
