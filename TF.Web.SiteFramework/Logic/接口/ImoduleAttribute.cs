using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TF.WebPlatForm.Entry;
namespace TF.WebPlatForm.Logic
{
   public interface IModuleAttribute
    {
       /// <summary>
       /// 模块信息列表
       /// </summary>
       /// <returns></returns>
       List<ModuleInfo> GetModuleList();
       
       /// <summary>
       ///获取模块的web附属信息 
       /// </summary>
       /// <returns></returns>
       List<WebModuleInfo> GetWebModuleList();
    }
}
