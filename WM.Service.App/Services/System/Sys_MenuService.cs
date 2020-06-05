using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WM.Infrastructure.Extensions;
using WM.Infrastructure.Models;
using WM.Infrastructure.UserManager;
using WM.Service.App.Dto.ManagerDto.RP;
using WM.Service.App.Interface.System;
using X.Models.WMDB;

namespace WM.Service.App.Services.System
{
    /// <summary>
    /// 
    /// </summary>
    public class Sys_MenuService : BaseSerivceDomain<Sys_Menu, X.IRespository.DBSession.IWMDBSession>, ISys_MenuService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="repository"></param>
        public Sys_MenuService(X.IRespository.DBSession.IWMDBSession repository) : base(repository)
        {
        }

        /// <summary>
        /// 获取用户当前菜单
        /// </summary>
        /// <returns></returns>
        public async Task<List<M_AdminRoleMenuRP>>  GetCurrentMenuList()
        {
            var where = string.Empty;
          
            var list =await repository.Sys_Menu.Where(q=>q.Enable==(byte)EnumDataStatus.Enable).ToListAsync();
           
            var menu = (from a in GetPermissions(UserContext.Current.RoleId)
                       join b in list
                       on a.Menu_Id equals b.Menu_Id
                       orderby b.Sort descending
                       select new M_AdminRoleMenuRP
                       {
                           Id = a.Menu_Id,
                           Name = b.MenuName,
                           Url = b.Url,
                           ParentId = b.ParentId,
                           Icon = b.Icon,
                           Actions=a.Actions,
                           Permission = a.Actions.Select(s => s.Value).ToArray()
                       }).ToList();
            return menu;
        }
        /// <summary>
        /// 获取用户菜单权限
        /// </summary>
        /// <returns></returns>
        private List<Permissions> GetPermissions(int role_Id)
        {
            var where = string.Empty;
            if (!UserContext.Current.IsSuperAdmin)
            {
                where = $" and b.Role_Id = {role_Id}";
            }
            var list = repository.Sql_Query<Permissions>($@"SELECT a.Menu_Id,a.ParentId,a.TableName,a.Auth, b.AuthValue from Sys_Menu a
                                                            INNER JOIN Sys_RoleAuth b
                                                            on a.Menu_Id = b.Menu_Id
                                                             where a.Enable = 1
                                                            {where}");

            List<Permissions> MenuActionToArray(List<Permissions> permissions)
            {
                permissions.ForEach(x =>
                {
                    try
                    {
                        x.Actions= string.IsNullOrEmpty(x.Auth)
                        ? new List<Sys_Actions>()
                        : x.Auth.DeserializeObject<List<Sys_Actions>>().ToList();

                        //x.UserAuthArr = string.IsNullOrEmpty(x.Auth)
                        //? new string[0]
                        //: x.Auth.DeserializeObject<List<Sys_Actions>>().Select(s => s.Value).ToArray();

                    }
                    catch { }
                    finally
                    {
                        if (x.Actions == null)
                        {
                            x.Actions = new List<Sys_Actions>();
                        }
                    }
                });
                return permissions;
            }

            return MenuActionToArray(list);
        }
         
        /// <summary>
        /// 获取所有菜单
        /// </summary>
        /// <returns></returns>
        public async Task<object> GetMenuList()
        {
            var list = await repository.Sys_Menu.Where(q => q.Enable != (byte)EnumDataStatus.Delete).Select(a => new
            {
                id = a.Menu_Id,
                parentId = a.ParentId,
                name = a.MenuName,
                orderNo = a.Sort
            }).ToListAsync();
            return list;
        }
        /// <summary>
        /// 获取所有菜单
        /// </summary>
        /// <returns></returns>
        public async Task<object> GetTreeInfo(int menuId)
        {
            var list = await repository.Sys_Menu.Where(q => q.Enable != (byte)EnumDataStatus.Delete && q.Menu_Id== menuId).Select(p => new
            {
                p.Menu_Id,
                p.ParentId,
                p.MenuName,
                p.Url,
                p.Auth,
                p.Sort,
                p.Icon,
                p.Enable,
                p.CreateDate,
                p.Creator,
                p.TableName,
                p.ModifyDate,
                p.Modifier,
            }).FirstAsync();
            return list;
        }
        /// <summary>
        /// 获取所有菜单
        /// </summary>
        /// <returns></returns>
        public async Task<WebResponseContent> Save(Sys_Menu menu)
        {
            WebResponseContent webResponse = new WebResponseContent();
            if (menu == null) return webResponse.Error("没有获取到提交的参数");
            if (menu.Menu_Id > 0 && menu.Menu_Id == menu.ParentId) return webResponse.Error("父级ID不能是当前菜单的ID");
            var isSave = false;
            if (menu.Menu_Id <= 0)
            {
                menu.CreateDate = DateTime.Now;
                menu.Creator = UserContext.Current.UserName;
                isSave =await repository.Sys_Menu.AddAsync(menu);
            }
            else
            {
                //2020.05.07新增禁止选择上级角色为自己
                if (menu.Menu_Id == menu.ParentId)
                {
                    return WebResponseContent.Instance.Error($"父级id不能为自己");
                }
                if (repository.Sys_Menu.IsExists(x => x.ParentId == menu.Menu_Id && menu.ParentId == x.Menu_Id))
                {
                    return WebResponseContent.Instance.Error($"不能选择此父级id，选择的父级id与当前菜单形成依赖关系");
                }
                menu.ModifyDate = DateTime.Now;
                menu.Modifier = UserContext.Current.UserName;
                menu.Enable =(byte)EnumDataStatus.Enable;
                isSave = await repository.Sys_Menu.UpdateAsync(menu);

            }
            if (isSave)
            {
                webResponse.OK("保存成功", menu);
            }
            return webResponse;
        }
    }
   
}
