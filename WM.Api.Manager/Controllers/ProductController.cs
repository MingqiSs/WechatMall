using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WM.Infrastructure.Controllers.Basic;
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
    public class ProductController : ApiBaseController<IProductDomainService>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="service"></param>
        public ProductController(IProductDomainService service)
                 : base(service)
        {
           var aa= this.Service.GetProductById("");
        }
        /// <summary>
        ///登录
        /// </summary>
        /// <returns></returns>
        [HttpPost, Route("GetProductById"), AllowAnonymous]
        public IActionResult GetProductById()
        {
            var aa = this.Service.GetProductById("d0f92fae-6ff6-11ea-abec-0242ac110002");
            return Ok(aa);
        }
    }
}