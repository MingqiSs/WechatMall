using System;
using System.Collections.Generic;
using System.Text;

namespace WM.Service.App.Dto.ManagerDto.RP
{
    /// <summary>
    /// 
    /// </summary>
    public class M_AdminUserRP
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        public string id { get; set; }
        /// <summary>
        /// 管理员名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 电话
        /// </summary>
        public string Moblie { get; set; }
        /// <summary>
        /// 角色Id 1:超级管理员,2:财务,3:运营
        /// </summary>
        public int RoleId { get; set; }
        /// <summary>
        /// 角色code
        /// </summary>
        public string RoleCode { get; set; }
        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// 菜单集合
        /// </summary>
        public List<M_AdminRoleMenuRP> Menus { get; set; }
    }

    public class M_AdminRoleMenuRP
    {
        /// <summary>
        /// id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 跳转地址
        /// </summary>
        public string Path { get; set; }
        /// <summary>
        /// Icon
        /// </summary>
        public string Icon { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 父节点
        /// </summary>
        public int ParentID { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        //public int Sort { get; set; }
        /// <summary>
        /// 下级菜单
        /// </summary>
        public List<M_AdminRoleMenuRP> Children { get; set; }
    }
}
