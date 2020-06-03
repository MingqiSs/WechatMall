using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
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
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<List<M_AdminRoleMenuRP>>  GetCurrentMenuList()
        {
            var where = string.Empty;
            if (!UserContext.Current.IsSuperAdmin)
            {
                where = $" and b.Role_Id = {UserContext.Current.RoleId}";
            }
            var list = repository.Sql_Query<M_AdminRoleMenuRP>($@"
                                                                SELECT a.Menu_Id as id,
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
    }
}
