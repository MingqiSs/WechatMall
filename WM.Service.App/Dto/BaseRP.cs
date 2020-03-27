using System;
using System.Collections.Generic;
using System.Text;

namespace WM.Service.App.Dto
{
    /// <summary>
    /// 
    /// </summary>
    public class KVIntRP
    {
        /// <summary>
        /// ID
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// Name
        /// </summary>
        public string Name { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class KVRP
    {
        /// <summary>
        /// key
        /// </summary>
        public string Key { get; set; }
        /// <summary>
        /// Value
        /// </summary>
        public string Value { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class LVRP
    {
        /// <summary>
        /// 
        /// </summary>
        public string Value { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Label { get; set; }
    }
}
