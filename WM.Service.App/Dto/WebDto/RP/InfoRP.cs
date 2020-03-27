using System;
using System.Collections.Generic;
using System.Text;

namespace WM.Service.App.Dto.WebDto.RP
{
    public class DistrictListRP : LVRP
    {
        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<LVRP> Children { get; set; }
    }
}
