using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
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
            if (!UserContext.Current.IsSuperAdmin)
            {
                where = $" and b.Role_Id = {UserContext.Current.RoleId}";
            }
            var list =await repository.DB.Ado.SqlQueryAsync<M_AdminRoleMenuRP>($@"
                                                                SELECT a.Menu_Id as id,
                                                                a.MenuName as Name,
                                                                a.Icon,
                                                                a.Url,
                                                                 a.ParentId,
                                                                a.Auth, 
                                                                b.AuthValue as permission 
                                                                from Sys_Menu a
                                                                INNER JOIN Sys_RoleAuth b
                                                                on a.Menu_Id=b.Menu_Id
                                                                 where a.DataStatus=1 
                                                               {where}");
            return list;
        }
        /// <summary>
        /// 获取所有菜单
        /// </summary>
        /// <returns></returns>
        public async Task<object> GetMenuList()
        {
            var list = await repository.Sys_Menu.Where(q => q.DataStatus != (byte)EnumDataStatus.Delete).Select(a => new
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
            var list = await repository.Sys_Menu.Where(q => q.DataStatus != (byte)EnumDataStatus.Delete && q.Menu_Id== menuId).Select(p => new
            {
                p.Menu_Id,
                p.ParentId,
                p.MenuName,
                p.Url,
                p.Auth,
                p.Sort,
                p.Icon,
                p.DataStatus,
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
                menu.DataStatus =(byte)EnumDataStatus.Enable;
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
