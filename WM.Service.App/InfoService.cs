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
using WM.Infrastructure.Extensions.AutofacManager;
using WM.Infrastructure;

namespace WM.Service.App
{
  public class InfoService: BaseSerivce , IInfoService
    {
        private readonly X.IRespository.DBSession.IWMDBSession _ibll;
        private readonly IUserDomainService _userDomainService;
        public InfoService(X.IRespository.DBSession.IWMDBSession ibll , IUserDomainService userDomainService)
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
           var aa= AutofacContainerModule.GetService<IUserService>().GetUserInfo("");
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
    }
}
