using System;
using System.Collections.Generic;
using System.Text;

namespace WM.Infrastructure.Models
{
    public class PageGridData<T>
    {
        public int status { get; set; }
        public string msg { get; set; }
        public int total { get; set; }
        public List<T> rows { get; set; }
        public object summary { get; set; }
        /// <summary>
        /// 可以在返回前，再返回一些额外的数据，比如返回其他表的信息，前台找到查询后的方法取出来
        /// </summary>
        public object extra { get; set; }
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
    public class KeyOptions
    {
        public List<string> Key { get; set; }
    }
    /// <summary>
    /// 保存
    /// </summary>
    public class SaveModel {
        public Dictionary<string, object> MainData { get; set; }
        public List<Dictionary<string, object>> DetailData { get; set; }
        public List<object> DelKeys { get; set; }

        /// <summary>
        /// 从前台传入的其他参数(自定义扩展可以使用)
        /// </summary>
        public object Extra { get; set; }
    }
}
