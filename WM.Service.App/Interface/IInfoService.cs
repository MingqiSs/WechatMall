using System;
using System.Collections.Generic;
using System.Text;
using WM.Infrastructure.Models;
using WM.Service.App.Dto.WebDto.RP;

namespace WM.Service.App.Interface
{
    public interface IInfoService
    {
        /// <summary>
        /// 获取省市区
        /// </summary>
        /// <returns></returns>
        ResultDto<List<DistrictListRP>> GetDistrictList();
    }
}
