using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;


namespace TF.CommonUtility
{
    /// <summary>
    ///Cookie 的摘要说明
    /// </summary>
    public class Cookie
    {
        public Cookie()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }

        public static string ReadCookie(string cookieName, string itemName)
        {
            if (HttpContext.Current.Request.Cookies[cookieName] == null) return "";
            return Convert.ToString(HttpContext.Current.Request.Cookies[cookieName].Values[itemName]);
        }

        public static void WriteCookie(Page page, string cookieName, string itemName, string itemValue, long ValidSecond)
        {
            HttpCookie cookie = new HttpCookie(cookieName);
            cookie.Expires = DateTime.Now.AddSeconds(ValidSecond);
            cookie.Values.Add(itemName, itemValue);
            page.Response.AppendCookie(cookie);
        }


        #region 新添加的Cookie操作 by lzy 2014-07-21

        /// <summary>
        /// 读cookie值
        /// </summary>
        /// <param name="strName">名称</param>
        /// <returns>cookie值</returns>
        public static string GetCookie(string strName)
        {
            if (HttpContext.Current.Request.Cookies != null && HttpContext.Current.Request.Cookies[strName] != null)
                return UriClass.UrlDecode(HttpContext.Current.Request.Cookies[strName].Value.ToString());
            return "";
        }

        /// <summary>
        /// 写cookie值
        /// </summary>
        /// <param name="strName">名称</param>
        /// <param name="strValue">值</param>
        /// <param name="strValue">过期时间(分钟)</param>
        public static void WriteCookie(string strName, string strValue, int expires)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[strName];
            if (cookie == null)
            {
                cookie = new HttpCookie(strName);
            }
            cookie.Value = UriClass.UrlEncode(strValue);
            cookie.Expires = DateTime.Now.AddMinutes(expires);
            HttpContext.Current.Response.AppendCookie(cookie);
        }

        #endregion
    }
}