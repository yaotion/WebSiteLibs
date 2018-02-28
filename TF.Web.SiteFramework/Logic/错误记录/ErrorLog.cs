using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TF.WebPlatForm.DBUtils;
using TF.WebPlatForm.Entry;

namespace TF.WebPlatForm.Logic
{
   public class ErrorLog
    {
        public static void ErrorSign(string strErrorContent)
        {

            //插入数据库
            DBWebPlatForm_Dat_ErrorLog dal = new DBWebPlatForm_Dat_ErrorLog(ConData.WebSiteConnectionString);
            WebPlatForm_Dat_ErrorLog model = new WebPlatForm_Dat_ErrorLog();
            model.strID = Guid.NewGuid().ToString("D").ToUpper();
            model.strErrorContent = strErrorContent;
            model.strType = "serverError";
            model.strClientIP = System.Web.HttpContext.Current.Request.UserHostAddress;
            model.dtAddTime = DateTime.Now;
            //登录人信息
            userInfo ui = UserInformation.LoginUser;
            model.strAddName = ui.strTrianmanName;
            model.strAddNumber = ui.strTrianmanNumber;
            dal.Add(model);

        }
    }
}
