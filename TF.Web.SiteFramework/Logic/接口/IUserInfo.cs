using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TF.WebPlatForm.Entry;
namespace TF.WebPlatForm.Logic
{
    public interface IUserInfo
    {
        /// <summary>
        /// 登录验证
        /// </summary>
        /// <param name="loginInfo">登录信息字符串</param>
        /// <returns></returns>
        loginReplay Login(string loginInfo);
        /// <summary>
        /// 根据tokenid获取用户信息
        /// </summary>
        /// <param name="strTokenID"></param>
        /// <returns></returns>
        userInfo GetUserInfoByTokenID(string strTokenID);
        /// <summary>
        /// 验证是否登录
        /// </summary>
        /// <param name="strTokenID"></param>
        /// <returns></returns>
        bool IsLogin(string strTokenID);
        /// <summary>
        /// 设置密码
        /// </summary>
        /// <param name="strTrainmanNumber">工号</param>
        /// <param name="strOldPwd">旧密码</param>
        /// <param name="strNewPwd">新密码</param>
        /// <returns></returns>
        bool SetPassword(string strTrainmanNumber, string strOldPwd, string strNewPwd);
    }
}
