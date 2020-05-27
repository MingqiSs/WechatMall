using System;
using System.Collections.Generic;
using System.Text;
using WM.Infrastructure.Models;
using WM.Service.App.Dto;
using WM.Service.App.Dto.WebDto.RP;
using WM.Service.App.Interface;
using WM.Service.Domain.Interface;
using System.Linq;
using WM.Service.App.Dto.WebDto.RQ;
using WM.Service.Domain.Entities;

namespace WM.Service.App
{
  public class ProductService : BaseSerivce , IProductService
    {
        private readonly X.IRespository.DBSession.IWMDBSession _ibll;
        private readonly IUserDomainService _userDomainService;
        public ProductService(X.IRespository.DBSession.IWMDBSession ibll , IUserDomainService userDomainService)
        {
            _ibll = ibll;
            _userDomainService = userDomainService;
        }

        /// <summary>
        /// 获取省市区列表
        /// </summary>
        /// <returns></returns>
        public ResultDto<List<DistrictListRP>> GetDistrictList()
        {
            //  var result = new List<DistrictListRP>();
            var ctis = _ibll.cm_city.ToList();
            var province = _ibll.cm_province.ToList();
            var result = province.Select(q => new DistrictListRP
            {
                Label = q.Name,
                Value = q.ID.ToString(),
                Children = ctis.Where(j => j.ProvinceID == q.ID).Select(j => new LVRP
                {
                    Label = j.Name,
                    Value = j.ID.ToString(),
                }).ToList()
            }).ToList();
            return Result(result);
        }

        /// <summary>
        /// 获取商品类型
        /// </summary>
        /// <returns></returns>
        public ResultDto<List<ProductTypeRP>> GetProductTypeList()
        {
           var list= _ibll.wm_product_type.Where(q => q.DataStatus == (byte)DataStatus.Enable)
                         .Select(q=>new ProductTypeRP { 
                         ID=q.ID,
                         Name=q.Name,
                         Sort=q.Sort
                         }).OrderBy(q=>q.Sort).ToList();
            return Result(list);
        }
        /// <summary>
        /// 获取商品标签
        /// </summary>
        /// <returns></returns>
        public ResultDto<List<ProductTagRP>> GetProductTagList()
        {
            var list = _ibll.wm_product_tag.Where(q => q.DataStatus == (byte)DataStatus.Enable)
                          .Select(q => new ProductTagRP
                          {
                              ID = q.ID,
                              Name = q.TagName,
                              Sort = q.Sort
                          }).OrderBy(q => q.Sort).ToList();
            return Result(list);
        }
        /// <summary>
        /// 获取商品列表
        /// </summary>
        /// <returns></returns>
        public ResultDto<PageDto<ProductRP>> GetProductPageList(ProductRQ rq)
        {
            var aaa = L("OperationSucceeded");
               var result = new PageDto<ProductRP>(rq.pi, rq.ps) { lst = new List<ProductRP>() };
            
            int totalCount = 0;
            var list = _ibll.wm_product.Where(q => q.DataStatus == (byte)EnumDataStatus.Enable)
                           .WhereIF(rq.ProductTypeID > 0,q=>q.ProductTypeID==rq.ProductTypeID)
                           .WhereIF(!rq.Keywords.IsNullOrWhiteSpace(),q=>q.Name.Contains(rq.Keywords))
                          //.WhereIF(rq.ProductTagID > 0, q => q.ProductTypeID == rq.ProductTagID)
                          .Select(q => new ProductRP
                          {
                              ID = q.ID,
                              Describe=q.Describe,
                              Hot=q.Hot,
                              Icon=q.Icon,
                              Image=q.Image,
                              Inventory=q.Inventory,
                              Name=q.Name,
                              Price=q.Price,
                              ProductTypeID=q.ProductTypeID,
                              Remark=q.Remark,
                              Sales=q.Sales,
                              Sort = q.Sort
                          }).OrderBy(q => q.Sort)
                          .ToPageList(rq.pi, rq.ps, ref totalCount);
            result.pg.tc = totalCount;
            result.lst = list;
            return Result(result);
        }
    }
}
