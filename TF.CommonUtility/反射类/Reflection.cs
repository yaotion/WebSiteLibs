using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Web;
using System.Web.UI;

namespace TF.CommonUtility
{
    
    public class customReflection
    {
        public customReflection()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }
        /// <summary>
        /// 反射方法调用
        /// </summary>
        /// <param name="DllName">dll名称</param>
        /// <param name="ClassName">类名称</param>
        /// <param name="MethodName">方法名称</param>
        /// <param name="args">参数</param>
        /// <returns></returns>
        public static object InvokeMethod(string DllName,string ClassName,string MethodName,object[] args)
        {
            var asm = Assembly.LoadFile(HttpContext.Current.Server.MapPath("/Bin/" + DllName));
            var type = asm.GetType(ClassName);
            var instance = asm.CreateInstance(ClassName);
            var method = type.GetMethod(MethodName);
            return method.Invoke(instance, args);
        }
        /// <summary>
        /// 反射属性调用
        /// </summary>
        /// <param name="DllName">dll名称</param>
        /// <param name="ClassName">类名称</param>
        /// <param name="MethodName">方法名称</param>
        /// <returns></returns>
        public static object InvokeProperty(string DllName, string ClassName, string MethodName)
        {
            var asm = Assembly.LoadFile(HttpContext.Current.Server.MapPath("/Bin/" + DllName));
            var type = asm.GetType(ClassName);
            var instance = asm.CreateInstance(ClassName);
            PropertyInfo TeaNameProperty = type.GetProperty(MethodName);
            return TeaNameProperty.GetValue(instance, null);
        }
    }
}
