using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace WM.Infrastructure.Enums
{
    public enum ResponseType
    {
        [Description("服务器异常")]
        ServerError = 1,
        LoginExpiration = 302,
        ParametersLack = 303,
        TokenExpiration,
        PINError,
        NoPermissions,
        NoRolePermissions,
        LoginError,
        AccountLocked,
        [Description("登录成功")]
        LoginSuccess,
        [Description("保存成功")]
        SaveSuccess,
        AuditSuccess,
        OperSuccess,
        RegisterSuccess,
        ModifyPwdSuccess,
        [Description("编辑成功")]
        EidtSuccess,
        [Description("删除成功")]
        DelSuccess,
        NoKey,
        NoKeyDel,
        KeyError,
        Other
    }
}
