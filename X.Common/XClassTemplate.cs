using System;
using System.Collections.Generic;
using System.Text;

namespace X.Common
{
    public class XClassTemplate
    {
        /*
         *namespace EF.Respository.Sons
{
    using System;
    public partial class PayBankBase:BaseRespository<EF.Entitys.PayBankBase>,EF.IRespository.Sons.IPayBankBase
    {
    
    } 
         * 
         */
        #region XTemplate

        public static string IRespositorySessionPropertyTemplate = PropertySpace + "X.IRespository.Sons.{DBName}.I{ClassName} {ClassName} {get;}" + "\r\n";
        public static string IRespositorySessionClassTemplate = "{using}\r\n" + "namespace {Namespace}\r\n" + "{\r\n" +
                                                ClassSpace + "public partial interface I{DBName}Session:X.IRespository.IRespositorySession\r\n" +
                                                ClassSpace + "{\r\n" +
                                                "{Content}\r\n" +
                                                ClassSpace + "}\r\n" + "}\r\n";

        public static string IRespositoryClassTemplate = ClassSpace + "public partial interface I{ClassName}:IBaseRespository<X.Models.{DBName}.{ClassName}>\r\n" +
                                               ClassSpace + "{\r\n" +
                                               ClassSpace + "}\r\n";
        public static string IRespositoryTemplate = "{using}\r\n" +
                                               "namespace {Namespace}.{DBName}\r\n" +
                                               "{\r\n" +
                                                "{Content}\r\n" +
                                                "}\r\n";

        public static string RespositorySessionPropertyTemplate = PropertySpace + "public X.IRespository.Sons.{DBName}.I{ClassName} {ClassName}" + "\r\n" +
                                               PropertySpace + "{\r\n" +
                                               ClassSpace + PropertySpace + "get { return new X.Respository.Sons.{DBName}.{ClassName}(); }" + "\r\n" +
                                               PropertySpace + "}\r\n";
        public static string RespositorySessionClassTemplate = "{using}\r\n" + "using SqlSugar;" + "\r\n" +
                                                "namespace {Namespace}\r\n" +
                                                "{\r\n" +
                                                "public partial class {DBName}Session : X.Respository.RespositorySession, X.IRespository.DBSession.I{DBName}Session" + "\r\n" +
                                                ClassSpace + "{\r\n" +
                                                PropertySpace + "public override SqlSugarClient DB" + "\r\n" +
                                                PropertySpace + "{\r\n" +
                                                PropertySpace + ClassSpace + "get" + "\r\n" +
                                                PropertySpace + ClassSpace + "{\r\n" +
                                                PropertySpace + ClassSpace + ClassSpace + "return DBOperation.GetClient_{DBName}();" + "\r\n" +
                                                PropertySpace + ClassSpace + "}\r\n" +
                                                PropertySpace + "}\r\n" +
                                                "{Content}\r\n" +
                                                ClassSpace + "}\r\n" +
                                                ClassSpace + "}\r\n";

        public static string RespositoryClassTemplate = ClassSpace + "public partial class {ClassName}:BaseRespository<X.Models.{DBName}.{ClassName}>,X.IRespository.Sons.{DBName}.I{ClassName}\r\n" +
                                               ClassSpace + "{\r\n" +
                                                PropertySpace + "public override SqlSugarClient db" + "\r\n" +
                                                PropertySpace + "{\r\n" +
                                                PropertySpace + ClassSpace + "get" + "\r\n" +
                                                PropertySpace + ClassSpace + "{\r\n" +
                                                PropertySpace + ClassSpace + ClassSpace + "return DBOperation.GetClient_{DBName}();" + "\r\n" +
                                                PropertySpace + ClassSpace + "}\r\n" +
                                                PropertySpace + "}\r\n" +
                                               ClassSpace + "}\r\n";

        public static string RespositoryTemplate = "{using}\r\n" + "using SqlSugar;" + "\r\n" +
                                               "namespace {Namespace}.{DBName}\r\n" +
                                               "{\r\n" +
                                                "{Content}\r\n" +
                                                "}\r\n";
        #endregion

        #region Template
        public static string ClassTemplate = "{using}\r\n" +
                                               "namespace {Namespace}\r\n" +
                                               "{\r\n" +
                                                //"{ClassDescription}{SugarTable}\r\n" +
                                                ClassSpace + "[Serializable]\r\n" +
                                                ClassSpace + "public partial class {ClassName}\r\n" +
                                                ClassSpace + "{\r\n" +
                                                PropertySpace + "public {ClassName}(){{Constructor}}\r\n\r\n" +
                                                "{Columns}\r\n" +
                                                ClassSpace + "}\r\n" +
                                                "}\r\n";

        public static string ClassDescriptionTemplate =
                                                ClassSpace + "///<summary>\r\n" +
                                                ClassSpace + "///{ClassDescription}" +
                                                ClassSpace + "///</summary>\r\n";

        public static string PropertyTemplate = //PropertySpace + "{SugarColumn}\r\n" +
                                                PropertySpace + "public {PropertyType} {PropertyName} { get; set; }\r\n\r\n";

        public static string PropertyDescriptionTemplate =
                                                PropertySpace + "/// <summary>\r\n" +
                                                PropertySpace + "/// Desc:{PropertyDescription}\r\n" +
                                                PropertySpace + "/// Default:{DefaultValue}\r\n" +
                                                PropertySpace + "/// Nullable:{IsNullable}\r\n" +
                                                PropertySpace + "/// </summary>\r\n";

        public static string ConstructorTemplate = PropertySpace + " this.{PropertyName} ={DefaultValue};\r\n";

        public static string UsingTemplate = "using System;\r\n" +
                                               "using System.Linq;\r\n" +
                                               "using System.ComponentModel.DataAnnotations;\r\n" +
                                               "using System.Text;" + "\r\n";
        #endregion

        #region Replace Key
        public const string KeyUsing = "{using}";
        public const string KeyNamespace = "{Namespace}";
        public const string KeyClassName = "{ClassName}";
        public const string KeyColumns = "{Columns}";
        public const string KeyIsNullable = "{IsNullable}";
        public const string KeySugarTable = "{SugarTable}";
        public const string KeyConstructor = "{Constructor}";
        public const string KeySugarColumn = "{SugarColumn}";
        public const string KeyPropertyType = "{PropertyType}";
        public const string KeyPropertyName = "{PropertyName}";
        public const string KeyDefaultValue = "{DefaultValue}";
        public const string KeyClassDescription = "{ClassDescription}";
        public const string KeyPropertyDescription = "{PropertyDescription}";
        public const string KeyContent = "{Content}";
        public const string KeyDBName = "{DBName}";
        #endregion

        #region Replace Value
        public const string ValueSugarTable = "\r\n" + ClassSpace + "[SugarTable(\"{0}\")]";
        public const string ValueSugarCoulmn = "\r\n" + PropertySpace + "[SugarColumn({0})]";
        #endregion

        #region Space
        public const string PropertySpace = "           ";
        public const string ClassSpace = "    ";
        #endregion
    }
}
