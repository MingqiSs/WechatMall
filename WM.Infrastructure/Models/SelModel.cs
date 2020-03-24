using System;
using System.Collections.Generic;
using System.Text;

namespace WM.Infrastructure.Models
{
    /// <summary>
    /// 返回下拉列表集合
    /// </summary>
    public class SelModel
    {
        public string Type { get; set; }
        public List<SelItem> Items { get; set; }
    }
    public class SelItem
    {
        public string Key { get; set; }
        public string Value { get; set; }
    }
}
