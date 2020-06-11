using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WM.Infrastructure.Enums;
using WM.Infrastructure.Extensions;
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
            //当前用户的权限
            var permissions = UserContext.Current.Permissions;

            var data = permissions.Select(x => new UserPermissions
            {
                Id = x.Menu_Id,
                Pid = x.ParentId,
                Text = x.Menu_Name,
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
        ///获取用户权限菜单
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public async Task<WebResponseContent> GetUserTreePermission(int roleId)
        {
            if (!UserContext.IsRoleIdSuperAdmin(roleId) && UserContext.Current.RoleId != roleId)
            {
                if (!(await GetAllChildren(UserContext.Current.RoleId)).Exists(x => x.Id == roleId))
                {
                    return Response.Error("没有权限获取此角色的权限信息");
                }
            }
            //权限用户权限查询所有的菜单信息
            var menus = UserContext.Current.GetPermissions(roleId);
            
            var data = menus.Select(x => new UserPermissions
            {
                Id = x.Menu_Id,
                Pid = x.ParentId,
                Text = x.Menu_Name,
                Actions =x.Actions,
            });
            return Response.OK(null, data);
        }
        /// <summary>
        /// 保存用戶权限
        /// </summary>
        /// <param name="userPermissions"></param>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public async Task<WebResponseContent> SavePermission(List<UserPermissions> userPermissions, int roleId) {
            string message = "";
            try
            {
                var user = UserContext.Current.UserInfo;
                if (!(await GetAllChildren(user.Role_Id)).Exists(x => x.Id == roleId))
                    return Response.Error("没有权限修改此角色的权限信息");
                //当前用户的权限
                var permissions = UserContext.Current.Permissions;

                List<int> originalMeunIds = new List<int>();
                //被分配角色的权限
                List<Sys_RoleAuth> roleAuths = await repository.Sys_RoleAuth.Where(x => x.Role_Id == roleId).ToListAsync();
                List<Sys_RoleAuth> updateAuths = new List<Sys_RoleAuth>();
                foreach (UserPermissions x in userPermissions)
                {
                    var per = permissions.Where(p => p.Menu_Id == x.Id).FirstOrDefault();
                    //不能分配超过当前用户的权限
                    if (per == null) continue;
                    //per.UserAuthArr.Contains(a.Value)校验权限范围
                    string[] arr = x.Actions == null || x.Actions.Count == 0
                      ? new string[0]
                      : x.Actions.Where(a => per.UserAuthArr.Contains(a.Value))
                      .Select(s => s.Value).ToArray();

                    //如果当前权限没有分配过，设置Auth_Id默认为0，表示新增的权限
                    var auth = roleAuths.Where(r => r.Menu_Id == x.Id).Select(s => new { s.Auth_Id, s.AuthValue, s.Menu_Id }).FirstOrDefault();
                    string newAuthValue = string.Join(",", arr);
                    //权限没有发生变化则不处理
                    if (auth == null || auth.AuthValue != newAuthValue)
                    {
                        updateAuths.Add(new Sys_RoleAuth()
                        {
                            Role_Id = roleId,
                            Menu_Id = x.Id,
                            AuthValue = string.Join(",", arr),
                            Auth_Id = auth == null ? 0 : auth.Auth_Id,
                            ModifyDate = DateTime.Now,
                            Modifier = user.UserName,
                            CreateDate = DateTime.Now,
                            Creator = user.UserName
                        });
                    }
                    else
                    {
                        originalMeunIds.Add(auth.Menu_Id);
                    }

                }
                //更新权限
                repository.Sys_RoleAuth.UpdateRange(updateAuths.Where(x => x.Auth_Id > 0).ToList());
                //新增的权限
                repository.Sys_RoleAuth.AddRange(updateAuths.Where(x => x.Auth_Id <= 0).ToList());

                //获取权限取消的权限
                int[] authIds = roleAuths.Where(x => userPermissions.Select(u => u.Id)
                 .ToList().Contains(x.Menu_Id) || originalMeunIds.Contains(x.Menu_Id))
                .Select(s => s.Auth_Id)
                .ToArray();
                List<Sys_RoleAuth> delAuths = roleAuths.Where(x => x.AuthValue != "" && !authIds.Contains(x.Auth_Id)).ToList();
                delAuths.ForEach(x =>
                {
                    x.AuthValue = "";
                });
                //将取消的权限设置为""
                repository.Sys_RoleAuth.UpdateRange(delAuths);

                int addCount = updateAuths.Where(x => x.Auth_Id <= 0).Count();
                int updateCount = updateAuths.Where(x => x.Auth_Id > 0).Count();
               
                Response.OK($"保存成功：新增加配菜单权限{addCount}条,更新菜单{updateCount}条,删除权限{delAuths.Count()}条");
            }
            catch (Exception ex)
            {
                message = "异常信息：" + ex.Message + ex.StackTrace + ",";
            }
            finally
            {
               // Logger.Info($"权限分配置:{message}{webResponse.Message}");
            }

            return Response;

        }
        /// <summary>
        /// 修改系统用户资料
        /// </summary>
        /// <param name="saveModel"></param>
        /// <returns></returns>
        public override WebResponseContent Update(SaveModel saveModel)
        {

            if (saveModel == null)
                return Response.Error(ResponseType.ParametersLack);
            if (saveModel.MainData.Count <= 1) return Response.Error("系统没有配置好编辑的数据，请检查model!");

            //设置修改时间,修改人的默认值
            var userInfo = UserContext.Current.UserInfo;
            // 设置默认字段的值"CreateID", "Creator", "CreateDate"，"ModifyID", "Modifier", "ModifyDate"
            saveModel.MainData.Add("Modifier", userInfo.UserName);
            saveModel.MainData.Add("ModifyDate", DateTime.Now);
            var model = saveModel.MainData.DicToEntity<Sys_Role>();

            Response.Status = repository.Sys_Role.Update(q => new Sys_Role
            {
                Dept_Id = model.Dept_Id,
                DeptName=model.DeptName,
            }, q => q.Role_Id == model.Role_Id);
            if (Response.Status) return Response.OK(ResponseType.EidtSuccess);

            return Response;
        }
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="saveModel"></param>
        /// <returns></returns>
        public override WebResponseContent Add(SaveModel saveModel)
        {
            if (saveModel == null)
                return Response.Error(ResponseType.ParametersLack);
            if (saveModel.MainData.Count <= 1) return Response.Error("系统没有配置好编辑的数据，请检查model!");

            //设置修改时间,修改人的默认值
            var userInfo = UserContext.Current.UserInfo;
            // 设置默认字段的值"CreateID", "Creator", "CreateDate"，"ModifyID", "Modifier", "ModifyDate"
            //  saveModel.MainData.Add("Creator", userInfo.UserName);
            // saveModel.MainData.Add("CreateDate", DateTime.Now);
            var  model = saveModel.MainData.DicToEntity<Sys_Role>();
            model.Creator = userInfo.UserName;
            model.CreateDate = DateTime.Now;
            Response.Status = repository.Sys_Role.Add(model);
            if (Response.Status) return Response.OK(ResponseType.SaveSuccess);

            return Response;
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
