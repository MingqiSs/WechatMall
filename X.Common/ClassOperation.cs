using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace X.Common
{
    /// <summary>
    /// 此类用于创建ORM相关实体类型初始化
    /// </summary>
    public class ClassOperation
    {
        /// <summary>
        /// 2.0 初始化数据库表实体对象
        /// </summary>
        /// <param name="tables"></param>
        /// <param name="path"></param>
        /// <param name="namespaces"></param>
        public static void InitModel(Dictionary<DbTableInfo, List<DbColumnInfo>> tables, string path, string namespaces)
        {
            var tableStr = new StringBuilder();
            var columnStr = new StringBuilder();
            foreach (var item in tables)
            {
                tableStr.Clear();
                var table = XClassTemplate.ClassTemplate
                    .Replace(XClassTemplate.KeyUsing, XClassTemplate.UsingTemplate)
                    .Replace(XClassTemplate.KeyNamespace, namespaces)
                    .Replace(XClassTemplate.KeyClassName, item.Key.Name)
                    .Replace(XClassTemplate.KeyConstructor, "");

                columnStr.Clear();
                foreach (var p in item.Value)
                {
                    columnStr.Append(XClassTemplate.PropertyDescriptionTemplate
                        .Replace(XClassTemplate.KeyPropertyDescription, p.ColumnDescription)
                        .Replace(XClassTemplate.KeyDefaultValue, p.DefaultValue)
                        .Replace(XClassTemplate.KeyIsNullable, $"{p.IsNullable}"));

                    columnStr.Append(XClassTemplate.PropertyTemplate
                        .Replace(XClassTemplate.KeyPropertyType, GetStringByType(p.DataType, p.IsNullable))
                        .Replace(XClassTemplate.KeyPropertyName, p.DbColumnName));
                }

                table = table.Replace(XClassTemplate.KeyColumns, columnStr.ToString());
                tableStr.Append(table);

                FileHelper.CreateFile($"{path}{item.Key.Name}.cs", tableStr.ToString(), Encoding.UTF8);
            }
        }

        /// <summary>
        /// 获得类型
        /// </summary>
        /// <param name="type"></param>
        /// <param name="isNullable"></param>
        /// <returns></returns>
        static string GetStringByType(string type, bool isNullable)
        {
            var cType = type;
            switch (type.ToLower())
            {
                case "varchar":
                case "mediumtext":
                    cType = "string";
                    break;
                case "bit":
                    cType = "bool";
                    break;
                case "tinyint":
                    cType = "byte";
                    break;
                case "bigint":
                    cType = "long";
                    break;
                case "datetime":
                    cType = "DateTime";
                    break;
                default:
                    break;
            }

            if (isNullable && cType != "string") cType = $"{cType}?";
            return cType;
        }

        /// <summary>
        /// 获得类型
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        static Type GetTypeByString(string type)
        {
            switch (type.ToLower())
            {
                case "system.boolean":
                    return Type.GetType("System.Boolean", true, true);
                case "system.byte":
                    return Type.GetType("System.Byte", true, true);
                case "system.sbyte":
                    return Type.GetType("System.SByte", true, true);
                case "system.char":
                    return Type.GetType("System.Char", true, true);
                case "system.decimal":
                    return Type.GetType("System.Decimal", true, true);
                case "system.double":
                    return Type.GetType("System.Double", true, true);
                case "system.single":
                    return Type.GetType("System.Single", true, true);
                case "system.int32":
                    return Type.GetType("System.Int32", true, true);
                case "system.uint32":
                    return Type.GetType("System.UInt32", true, true);
                case "system.int64":
                    return Type.GetType("System.Int64", true, true);
                case "system.uint64":
                    return Type.GetType("System.UInt64", true, true);
                case "system.object":
                    return Type.GetType("System.Object", true, true);
                case "system.int16":
                    return Type.GetType("System.Int16", true, true);
                case "system.uint16":
                    return Type.GetType("System.UInt16", true, true);
                case "system.string":
                    return Type.GetType("System.String", true, true);
                case "system.datetime":
                case "datetime":
                    return Type.GetType("System.DateTime", true, true);
                case "system.guid":
                    return Type.GetType("System.Guid", true, true);
                default:
                    return Type.GetType(type, true, true);
            }
        }

        /// <summary>
        /// 2.1 初始化仓储数据访问层接口
        /// </summary>
        /// <param name="path"></param>
        /// <param name="namespaces"></param>
        public static void InitIRespository(List<string> tablenames, string path, string namespaces, string dbname = "DB")
        {
            StringBuilder sb = new StringBuilder();
            foreach (string item in tablenames)
            {
                sb.Append(XClassTemplate.IRespositoryClassTemplate.Replace(XClassTemplate.KeyClassName, item).Replace(XClassTemplate.KeyDBName, dbname));
            }
            string result = XClassTemplate.IRespositoryTemplate.Replace(XClassTemplate.KeyContent, sb.ToString())
                .Replace(XClassTemplate.KeyUsing, XClassTemplate.UsingTemplate)
                .Replace(XClassTemplate.KeyNamespace, namespaces)
                .Replace(XClassTemplate.KeyDBName, dbname);
            var filePath = path + string.Format("I{0}.cs", dbname);
            FileHelper.CreateFile(filePath, result, Encoding.UTF8);
        }

        /// <summary>
        /// 2.1 初始化仓储数据访问层接口
        /// </summary>
        /// <param name="path"></param>
        /// <param name="namespaces"></param>
        public static void InitIRespositorySession(List<string> tablenames, string path, string namespaces, string dbname = "DB")
        {
            StringBuilder sb = new StringBuilder();
            foreach (string item in tablenames)
            {
                sb.Append(XClassTemplate.IRespositorySessionPropertyTemplate.Replace(XClassTemplate.KeyClassName, item).Replace(XClassTemplate.KeyDBName, dbname));
            }
            string result = XClassTemplate.IRespositorySessionClassTemplate.Replace(XClassTemplate.KeyContent, sb.ToString())
                .Replace(XClassTemplate.KeyUsing, XClassTemplate.UsingTemplate)
                .Replace(XClassTemplate.KeyNamespace, namespaces)
                .Replace(XClassTemplate.KeyDBName, dbname);
            var filePath = path + string.Format("I{0}Session.cs", dbname);
            FileHelper.CreateFile(filePath, result, Encoding.UTF8);
        }

        /// <summary>
        /// 3.1 初始化仓储数据访问层
        /// </summary>
        /// <param name="path"></param>
        /// <param name="namespaces"></param>
        public static void InitRespository(List<string> tablenames, string path, string namespaces, string dbname = "DB")
        {
            StringBuilder sb = new StringBuilder();
            foreach (string item in tablenames)
            {
                sb.Append(XClassTemplate.RespositoryClassTemplate.Replace(XClassTemplate.KeyClassName, item).Replace(XClassTemplate.KeyDBName, dbname));
            }
            string result = XClassTemplate.RespositoryTemplate.Replace(XClassTemplate.KeyContent, sb.ToString())
                .Replace(XClassTemplate.KeyUsing, XClassTemplate.UsingTemplate)
                .Replace(XClassTemplate.KeyNamespace, namespaces)
                .Replace(XClassTemplate.KeyDBName, dbname);
            var filePath = path + string.Format("{0}.cs", dbname);
            FileHelper.CreateFile(filePath, result, Encoding.UTF8);
        }
        /// <summary>
        /// 3.2 初始化仓储数据访问层
        /// </summary>
        /// <param name="path"></param>
        /// <param name="namespaces"></param>
        public static void InitRespositorySession(List<string> tablenames, string path, string namespaces, string dbname = "DB")
        {
            StringBuilder sb = new StringBuilder();
            foreach (string item in tablenames)
            {
                sb.Append(XClassTemplate.RespositorySessionPropertyTemplate.Replace(XClassTemplate.KeyClassName, item).Replace(XClassTemplate.KeyDBName, dbname));
            }
            string result = XClassTemplate.RespositorySessionClassTemplate.Replace(XClassTemplate.KeyContent, sb.ToString())
                .Replace(XClassTemplate.KeyUsing, XClassTemplate.UsingTemplate)
                .Replace(XClassTemplate.KeyNamespace, namespaces)
                 .Replace(XClassTemplate.KeyDBName, dbname);
            var filePath = path + string.Format("{0}Session.cs", dbname);
            FileHelper.CreateFile(filePath, result, Encoding.UTF8);
        }


    }
}
