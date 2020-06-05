using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WM.Infrastructure.Extensions.AutofacManager;
using WM.Infrastructure.Models;
using WM.Infrastructure.UserManager;
using WM.Service.App.Interface.System;
using X.Models.WMDB;

namespace WM.Service.App.Services.System
{
    /// <summary>
    /// 角色
    /// </summary>
    public class Sys_RoleService : BaseSerivceDomain<Sys_Role, X.IRespository.DBSession.IWMDBSession>, ISys_RoleService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="repository"></param>
        public Sys_RoleService(X.IRespository.DBSession.IWMDBSession repository) : base(repository)
        {

        }
        /// <summary>
        /// 编辑权限时获取当前用户下的所有角色与当前用户的菜单权限
        /// </summary>
        /// <returns></returns>
        public async Task<WebResponseContent> GetCurrentTreePermission()
        {
            int roleId = UserContext.Current.RoleId;
            var menus = await Task.Run(() => AutofacContainerModule.GetService<ISys_MenuService>().GetCurrentMenuList());
           
            var data = menus.Select(x => new UserPermissions
            {
                Id = x.Id,
                Pid = x.ParentId,
                Text = x.Name,
                Actions =x.Actions,
            });
            return new WebResponseContent().OK(null, new
            {
                tree = data,
                roles = await GetAllChildren(roleId)
            });
        }
        /// <summary>
        /// 获取当前角色下的所有角色
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public async Task<List<RoleNodes>> GetAllChildren(int roleId)
        {
          var roles = await repository.Sys_Role.Where(q => q.Enable == 1 && q.Role_Id > 1)
                               .Select(q => new RoleNodes
                               {
                                   Id = q.Role_Id,
                                   ParentId = q.ParentId,
                                   RoleName = q.RoleName
                               }).ToListAsync();
            if (roleId == 1)
            {
                return roles;
            }
            var rolesChildren = GetChildren(roles,roleId);
            return rolesChildren;
        }
        /// <summary>
        /// 递归获取所有子节点权限
        /// </summary>
        /// <param name="roles"></param>
        /// <param name="roleId"></param>
        /// <returns></returns>
        private List<RoleNodes> GetChildren(List<RoleNodes> roles, int roleId)
        {
            var rolesChildren = new List<RoleNodes>();
            roles.ForEach(x =>
            {
                if (x.ParentId == roleId)
                {
                    rolesChildren.Add(x);
                   // GetChildren(x,x.Id);
                }
            });
            return rolesChildren;
        }


    }
    /// <summary>
    /// 
    /// </summary>
    public class RoleNodes
    {
        /// <summary>
        /// 
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int ParentId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string RoleName { get; set; }
    }
    /// <summary>
    /// 
    /// </summary>
    public class UserPermissions
    {
        /// <summary>
        /// 
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int Pid { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Text { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<Sys_Actions> Actions { get; set; }
    }
}
