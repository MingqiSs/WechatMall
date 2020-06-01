using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WM.Api.Manager.Filter;
using WM.Infrastructure.Controllers.Basic;
using WM.Infrastructure.Filters;
using WM.Service.App;
using WM.Service.App.Interface;
using WM.Service.Domain.Interface;

namespace WM.Api.Manager.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ApiBaseController<IProductService>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="service"></param>
        public ProductController(IProductService service)
                 : base(service)
        {
        }
        /// <summary>
        ///获取商品类型
        /// </summary>
        /// <returns></returns>
        [CheckRoleAuthorizeFilter]
        [HttpPost, Route("GetProductTypeList")]
       // [ApiActionPermission(Infrastructure.Enums.ActionPermissionOptions.Search)]
        public IActionResult GetProductTypeList()
        {
            var r = Service.GetProductTypeList();
           
            return Ok(r);
        }
    }
}