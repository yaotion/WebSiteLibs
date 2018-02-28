using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Xml.Linq;
using System.Web.Caching;
using System.Linq;

namespace TF.Web.WebAPI
{
    /// <summary>
    /// 接口定义
    /// </summary>
    public class APIItem
    {
        public string APIName = "";
        public string APIBrief = "";
        public string TypeName = "";
        public string MethodName = "";
        public string AssemblyName = "";
        public ApiManager.ParamDef input;
        public ApiManager.ParamDef output;
    }
    /// <summary>
    ///ApiManager 的摘要说明
    /// </summary>
    public class ApiManager
    {
        string filePath = System.Web.HttpContext.Current.Server.MapPath("/接口文档.xml");

        public ApiManager()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            // 
        }

        /// <summary>
        /// 从接口文档.XML获取接口定义
        /// </summary>
        /// <returns></returns>
        public List<APIItem> GetApiList()
        {
            List<APIItem> apiItems = new List<APIItem>();
            //string xmlPath = System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "\\接口文档.xml";
            XElement root = XElement.Load(filePath);
            XElement eProject = root.Element("Project");
            XElement eDataList = eProject.Element("DataList");
            XElement eApiList = eProject.Element("APIList");
            IEnumerable<XElement> eApiTypes = eApiList.Elements("APIType");
            if (eApiTypes != null)
            {
                foreach (XElement ele in eApiTypes)
                {
                    IEnumerable<XElement> apiList = ele.Elements("APIItem");
                    if (apiList != null)
                    {
                        foreach (XElement xEl in apiList)
                        {
                            APIItem item = new APIItem();
                            item.APIName = xEl.Attributes("APIName").FirstOrDefault().Value;
                            item.APIBrief = xEl.Attributes("APIBrief").FirstOrDefault().Value;
                            item.MethodName = xEl.Attributes("MethodName").FirstOrDefault().Value;
                            item.AssemblyName = ele.Attributes("AssemblyName").FirstOrDefault().Value;
                            item.TypeName = xEl.Attributes("TypeName").FirstOrDefault().Value;
                            XElement xInput = xEl.Element("InputData");
                            XElement xOutput = xEl.Element("OutputData");
                            if (xInput != null)
                            {
                                item.input = GetParamDefFromElement(xInput);
                            }
                            if (xOutput != null)
                            {
                                item.output = GetParamDefFromElement(xOutput);
                            }
                            apiItems.Add(item);
                        }
                    }
                }
            }
            return apiItems;
        }

        //刷新接口列表，并缓存
        public void RefreshApiList()
        {
            List<APIItem> apiItems = GetApiList();
            CacheApiList(apiItems);
        }

        /// <summary>
        /// 缓存改变回调函数
        /// 目的是当接口文档.XML文件发生改变的时候，重新加载接口文档定义。通常发生在接口更新的时候
        /// </summary>
        /// <param name="key"></param>
        /// <param name="reason"></param>
        /// <param name="expensiveObject"></param>
        /// <param name="dependency"></param>
        /// <param name="absoluteExpiration"></param>
        /// <param name="slidingExpiration"></param>
        private void CacheItemUpdateCallback(string key, CacheItemUpdateReason reason, out Object expensiveObject, out CacheDependency dependency,
    out DateTime absoluteExpiration,
    out TimeSpan slidingExpiration)
        {
            expensiveObject = GetApiList();
            absoluteExpiration = Cache.NoAbsoluteExpiration;
            dependency = new CacheDependency(filePath);
            slidingExpiration = TimeSpan.FromHours(1.0);
        }
        /// <summary>
        /// 缓存接口列表
        /// </summary>
        /// <param name="items"></param>
        private void CacheApiList(List<APIItem> items)
        {
            CacheDependency dependency = new CacheDependency(filePath);
            Cache cache = HttpRuntime.Cache;
            cache.Insert("apilist", items, dependency, Cache.NoAbsoluteExpiration, TimeSpan.FromHours(1.0), CacheItemUpdateCallback);
        }
        /// <summary>
        /// 参数定义
        /// </summary>
        public class ParamDef
        {
            public string ObjectName = "";
            public string ObjectBrief = "";
            public string ObjectRemark = "";
            public string TypeName = "";
            public string TypeSort = "0";
            public List<PropertyObject> properties;
        }
        public class PropertyObject
        {
            public string ObjectName = "";
            public string ObjectBrief = "";
            public string ObjectRemark = "";
            public string TypeName = "";
            public string TypeSort = "1";
        }
        /// <summary>
        /// 从XML节点获取数据定义
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        private ParamDef GetParamDefFromElement(XElement element)
        {
            ParamDef param = new ParamDef();
            System.Diagnostics.Debug.Assert(element != null);
            param.ObjectBrief = element.Attributes("ObjectBrief").FirstOrDefault().Value;
            param.ObjectName = element.Attributes("ObjectName").FirstOrDefault().Value;
            param.ObjectRemark = element.Attributes("ObjectRemark").FirstOrDefault().Value;
            param.TypeName = element.Attributes("TypeName").FirstOrDefault().Value;
            param.TypeSort = element.Attributes("TypeSort").FirstOrDefault().Value;
            IEnumerable<XElement> subElements = element.Elements("PropertyObject");
            PropertyObject p = null;
            if (subElements != null && subElements.Count() > 0)
            {
                param.properties = new List<PropertyObject>();
                foreach (XElement elm in subElements)
                {
                    p = new PropertyObject();
                    p.ObjectBrief = elm.Attributes("ObjectBrief").FirstOrDefault().Value;
                    p.ObjectName = elm.Attributes("ObjectName").FirstOrDefault().Value;
                    p.ObjectRemark = elm.Attributes("ObjectRemark").FirstOrDefault().Value;
                    p.TypeName = elm.Attributes("TypeName").FirstOrDefault().Value;
                    p.TypeSort = element.Attributes("TypeSort").FirstOrDefault().Value;
                    param.properties.Add(p);
                }
            }
            return param;
        }


        public static string WebSiteConnectionString = TF.CommonUtility.XmlClass.Read("XmlConfig.xml", "/XmlConfig/ConData/WebSiteConnectionString");
    }



    /// <summary>
    /// 接口站返回结果类型
    /// </summary>
    public class InterfaceOutPut
    {
        //1代表成功，非1代表失败
        public int Success = 1;
        public string ResultText = "返回成功";
        public object Data = null;
    }

    /// <summary>
    /// 接口站输入类型
    /// </summary>
    public class InterfaceInPut
    {
        //接口名称
        public string DataType = "";
        //接口输入数据
        public string Data = "";
    }
}
