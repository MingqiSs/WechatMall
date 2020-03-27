using System;
using System.Collections.Generic;
using System.Text;
using WM.Infrastructure.Models;
using WM.Service.App.Dto.WebDto.RP;

namespace WM.Service.App.Interface
{
   public interface IOrderService
    {
        /// <summary>
        /// 查看购物车信息
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        ResultDto<OrderCradRP> GetOrderCardInfo(string uid);
    }
}
