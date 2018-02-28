using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;


namespace TF.YA.Sync
{
    /// <summary>
    /// 请求发送数据
    /// </summary>
    public class RequData
    {

        public string DefaultUserAgent { get; set; }
        public string UserAgent { get; set; }
        public int? Timeout { get; set; }
        public string Encoding { get; set; }
        public CookieCollection Cookies { get; set; }
        public string ParamName { get; set; }
        public string ParamValue { get; set; }

    }
    /// <summary>
    /// 返回结果定义
    /// </summary>
    public class RespData
    {
        public string Success { get; set; }
        public string ResultText { get; set; }
        public DeptClsList Data { get; set; }
    }
}
