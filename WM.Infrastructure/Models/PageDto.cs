using System;
using System.Collections.Generic;
using System.Text;

namespace WM.Infrastructure.Models
{
    /// <summary>
    /// 分页列表(For API)
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PageDto
    {
        /// <summary>
        /// 当前页面索引
        /// </summary>
        public int pi { get; set; }

        /// <summary>
        /// 页面数据数量
        /// </summary>
        public int ps { get; set; }

        /// <summary>
        /// 全部数据总数
        /// </summary>
        public long tc { get; set; }

        /// <summary>
        /// 页面总数
        /// </summary>
        public int pt
        {
            get
            {
                return ps > 0 ? (int)Math.Ceiling((double)tc / ps) : 0;
            }
        }

    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PageDto<T>
    {
        /// <summary>
        /// 
        /// </summary>
        public PageDto pg { get; set; }

        /// <summary>
        /// 分页列表
        /// </summary>
        public List<T> lst { get; set; }
        /// <summary>
        /// 其他数据
        /// </summary>
        public object data { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        public PageDto(int pageIndex, int pageSize)
        {
            pg = new PageDto { pi = pageIndex, ps = pageSize };
        }

    }
    public class PageDataOptions
    {
        public int Page { get; set; }
        public int Rows { get; set; }
        public int Total { get; set; }
        public string TableName { get; set; }
        public string Sort { get; set; }
        /// <summary>
        /// 排序方式
        /// </summary>
        public string Order { get; set; }
        public string Wheres { get; set; }
        public bool Export { get; set; }
        public object Value { get; set; }
    }
    public class SearchParameters
    {
        public string Name { get; set; }
        public string Value { get; set; }
        public string DisplayType { get; set; }
    }
}
