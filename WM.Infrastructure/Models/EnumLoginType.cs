using System;
using System.Collections.Generic;
using System.Text;

namespace WM.Infrastructure.Models
{
    /// <summary>
    /// 登入类型
    /// </summary>
    public enum EnumLoginType : byte
    {
        None = 0,
        /// <summary>
        /// 登入模拟账户
        /// </summary>
        Simulate = 1,
        /// <summary>
        /// 真实账户
        /// </summary>
        Reality = 2
    }
    /// <summary>
    /// 第三方注册类型
    /// </summary>
    public enum EnumThirdRegisterType
    {
        /// <summary>
        /// Google
        /// </summary>
        Google=1,
        /// <summary>
        /// FaceBook
        /// </summary>
        FaceBook=2,
        /// <summary>
        /// QQ
        /// </summary>
        QQ=3
    }
}
