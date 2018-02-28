using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using jsonnet = Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace TF.CommonUtility
{
    /// <summary>
    /// JSON操作帮助类
    /// </summary>
    public class JsonHelper
    {
        #region 常用方法

        /// <summary>
        /// 序列化单个对象为JSON字符串
        /// </summary>
        /// <param name="value">要序列化的简单对象</param>
        /// <returns>string</returns>
        public static string SerializeSingleObject(object value)
        {
            string json = SerializeObject(value);
            json = json.Replace(":null", ":\"\"");
            return json;
        }

        /// <summary>
        /// 序列化List为JSON字符串(支持嵌套)
        /// </summary>
        /// <param name="value">要序列化的简单对象</param>
        /// <returns>string</returns>
        public static string SerializeList(object value)
        {
            string json = jsonnet.JsonConvert.SerializeObject(value, new jsonnet.JsonSerializerSettings
            {
                ReferenceLoopHandling = jsonnet.ReferenceLoopHandling.Serialize
            });
            //json = json.Replace(":null", ":\"\"");
            return json;
        }

        /// <summary>
        /// 返回JQuery EasyUI DataGrid JSON分页字符串
        /// </summary>
        /// <param name="dt">数据源</param>
        /// <param name="total">数据总个数</param>
        /// <returns>string</returns>
        public static string DataGridPagerJson(DataTable dt, int total)
        {
            if (total == 0)
                return "{\"total\":0,\"rows\":[]}";
            else
                return "{\"total\":" + total + ",\"rows\":" + SerializeObject(dt) + "}";

        }

        /// <summary>
        /// 接口类调用的DataTable转JSON字符串
        /// </summary>
        /// <param name="dt">数据源</param>
        /// <returns>string</returns>
        public static string DataTableToJsonForAPI(DataTable dt)
        {
            return "{\"Success\":1,\"ResultText\":\"\",\"Total\":" + dt.Rows.Count + ",\"Items\":" + SerializeObject(dt) + "}";
        }

        /// <summary>
        /// JSON字符串转List集合
        /// </summary>
        /// <typeparam name="T">任意简单类型</typeparam>
        /// <param name="json">要反序列化的JSON字符串</param>
        /// <returns>T对象的List集合</returns>
        public static List<T> JsonToList<T>(string json)
        {
            return DeserializeObject<List<T>>(json);
        }

        #endregion

        #region 基础方法

        /// <summary>
        /// 序列化对象为JSON字符串(支持序列化简单对象和DataTable)
        /// </summary>
        /// <param name="value">简单对象(实体类)</param>
        /// <returns>JSON字符串</returns>
        public static string SerializeObject(object value)
        {
            IsoDateTimeConverter dateFormat = new IsoDateTimeConverter();
            DBNullCreationConverter nullFormat = new DBNullCreationConverter();
            dateFormat.DateTimeFormat = "yyyy-MM-dd HH:mm:ss";
            return jsonnet.JsonConvert.SerializeObject(value, dateFormat, nullFormat);
        }

        /// <summary>
        /// 反序列化JSON字符串为任意指定的T(简单对象)
        /// </summary>
        /// <typeparam name="T">任意简单类型</typeparam>
        /// <param name="json">要反序列化的JSON字符串</param>
        /// <returns>T(任意简单类型)</returns>
        public static T DeserializeObject<T>(string json)
        {
            return jsonnet.JsonConvert.DeserializeObject<T>(json, new jsonnet.JsonSerializerSettings
            {
                ReferenceLoopHandling = jsonnet.ReferenceLoopHandling.Ignore
            });
        }

        #endregion
    }

    /// <summary>
    /// 自定义格式化器
    /// (对DBNull的转换处理，此处只写了转换成JSON字符串的处理，JSON字符串转对象的未处理)
    /// </summary>
    public class DBNullCreationConverter : jsonnet.JsonConverter
    {
        /// <summary>
        /// 是否允许转换
        /// </summary>
        public override bool CanConvert(Type objectType)
        {
            bool canConvert = false;
            switch (objectType.FullName)
            {
                case "System.DBNull":
                    canConvert = true;
                    break;
            }
            return canConvert;
        }

        public override object ReadJson(jsonnet.JsonReader reader, Type objectType, object existingValue, jsonnet.JsonSerializer serializer)
        {
            return existingValue;
        }

        public override void WriteJson(jsonnet.JsonWriter writer, object value, jsonnet.JsonSerializer serializer)
        {
            writer.WriteValue(string.Empty);
        }

        public override bool CanRead
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        /// 是否允许转换JSON字符串时调用
        /// </summary>
        public override bool CanWrite
        {
            get
            {
                return true;
            }
        }
    }
}