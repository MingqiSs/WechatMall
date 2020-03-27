using System;
using System.Collections.Generic;
using System.Text;
using WM.Infrastructure.Models;
using WM.Service.App.Dto.WebDto.RP;
using WM.Service.App.Dto.WebDto.RQ;

namespace WM.Service.App.Interface
{
    public interface IProductService
    {
        /// <summary>
        /// 商品类型
        /// </summary>
        /// <returns></returns>
        ResultDto<List<ProductTypeRP>> GetProductTypeList();
        /// <summary>
        /// 商品标签
        /// </summary>
        /// <returns></returns>
        ResultDto<List<ProductTagRP>> GetProductTagList();
        /// <summary>
        /// 商品列表
        /// </summary>
        /// <param name="rq"></param>
        /// <returns></returns>
        ResultDto<PageDto<ProductRP>> GetProductPageList(ProductRQ rq);
    }
}
