using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace TF.CommonUtility
{
    /// <summary>
    ///TF.CommonUtility.ObjectConvertClass 的摘要说明
    /// </summary>
    public class ObjectConvertClass 
    {
        public ObjectConvertClass()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }

        /// <summary> 精度</summary>
        /// <param name="o"></param>
        /// <param name="len">保留长度</param>
        /// <returns></returns>
        public static decimal ext_decimalRound(object o, int len)
        {
            return decimal.Round(Convert.ToDecimal(static_ext_decimal(o)), len);
        }
        /// <summary> 数据类型转换（对象转整型 失败返回0 ）</summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public static int static_ext_int(object o)
        {
            if (o != null && o != "undefined")
            {
                try
                {
                    return int.Parse(static_ext_string(o));
                }
                catch (Exception ex)
                {
                    return 0;
                }
            }
            else
            {
                return 0;
            }
        }
        /// <summary> 数据类型转换（对象转整型 失败返回0 ）</summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public static int? static_ext_Int(object o)
        {
            if (o != null && o != "undefined")
            {
                try
                {
                    return int.Parse(static_ext_string(o));
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }
        /// <summary> 数据类型转换（对象转整型 失败返回0 ） </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public static double static_ext_double(object o)
        {
            if (o != null && o != "undefined")
            {
                try
                {
                    return double.Parse(static_ext_string(o));
                }
                catch (Exception ex)
                {
                    return 0;
                }
            }
            else
            {
                return 0;
            }
        }
        /// <summary>数据类型转换（对象转整型 失败返回null ）</summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public static decimal? static_ext_decimal(object o)
        {
            if (o != null && o != "undefined")
            {
                try
                {
                    return decimal.Parse(static_ext_string(o));
                }
                catch (Exception)
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        /// <summary>数据类型转换（对象转整型 失败返回null ）</summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public static decimal static_ext_Decimal(object o)
        {
            if (o != null && o != "undefined")
            {
                try
                {
                    return decimal.Parse(static_ext_string(o));
                }
                catch (Exception)
                {
                    return 0;
                }
            }
            else
            {
                return 0;
            }
        }

        /// <summary>数据类型转换为字符串</summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public static string static_ext_string(object o)
        {
            if (o != null)
            {
                try
                {
                    if (o.ToString().Trim() == "undefined")
                    {
                        return "";
                    }
                    else
                    {
                        return o.ToString().Trim();
                    }
                }
                catch (Exception ex)
                {
                    return "";
                }
            }
            else
            {
                return "";
            }
        }
        /// <summary>数据类型转换为日期</summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public static DateTime? static_ext_date(object o)
        {
            if (o != null)
            {
                try
                {
                    return Convert.ToDateTime(o);
                }
                catch (Exception)
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }
        /// <summary>数据类型转换为日期</summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public static DateTime static_ext_Date(object o)
        {
            if (o != null)
            {
                try
                {
                    return Convert.ToDateTime(o);
                }
                catch (Exception)
                {
                    return DateTime.Now;
                }
            }
            else
            {
                return DateTime.Now;
            }
        }
        /// <summary>自定义时间格式</summary>
        /// <param name="o"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static string static_ext_date1(object o, string format)
        {
            if (o != null)
            {
                try
                {
                    return Convert.ToDateTime(o) > DateTime.Parse("1899-12-30") ? Convert.ToDateTime(o).ToString(format) : "";
                }
                catch (Exception)
                {
                    return null;
                }
            }
            else
            {
                return "";
            }
        }
        #region 新添加的方法 by lzy 2014-11-13-16-16

        /// <summary>
        /// 将字符串转换为Int32类型
        /// </summary>
        /// <param name="expression">要转换的字符串</param>
        /// <param name="defValue">缺省值</param>
        /// <returns>转换后的int类型结果</returns>
        public static int StrToInt(string expression, int defValue)
        {
            if (string.IsNullOrEmpty(expression) || expression.Trim().Length >= 11 || !Regex.IsMatch(expression.Trim(), @"^([-]|[0-9])[0-9]*(\.\w*)?$"))
                return defValue;

            int rv;
            if (Int32.TryParse(expression, out rv))
                return rv;

            return Convert.ToInt32(StrToFloat(expression, defValue));
        }

        /// <summary>
        /// string型转换为float型
        /// </summary>
        /// <param name="strValue">要转换的字符串</param>
        /// <param name="defValue">缺省值</param>
        /// <returns>转换后的int类型结果</returns>
        public static float StrToFloat(string expression, float defValue)
        {
            if ((expression == null) || (expression.Length > 10))
                return defValue;

            float intValue = defValue;
            if (expression != null)
            {
                bool IsFloat = Regex.IsMatch(expression, @"^([-]|[0-9])[0-9]*(\.\w*)?$");
                if (IsFloat)
                    float.TryParse(expression, out intValue);
            }
            return intValue;
        }
        /// <summary>数据类型转换（对象转整型 失败返回null ）</summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public static float? static_ext_float(object o)
        {
            if (o != null && o != "undefined")
            {
                try
                {
                    return float.Parse(static_ext_string(o));
                }
                catch (Exception)
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }
        /// <summary>数据类型转换为字符串</summary>
        /// <param name="o"></param>
        /// <returns>“”字符串返回null</returns>
        public static string static_ext_string_null(object o)
        {
            if (o != "")
            {
                try
                {
                    if (o.ToString().Trim() == "undefined")
                    {
                        return null;
                    }
                    else
                    {
                        return o.ToString().Trim();
                    }
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        #endregion
    }
}


