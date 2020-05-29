using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        }
    }
}