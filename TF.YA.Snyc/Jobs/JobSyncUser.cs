using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Quartz;
using Common.Logging;
using System.Threading;


namespace TF.YA.Sync
{
    [PersistJobDataAfterExecution]
    [DisallowConcurrentExecution]
    public class JobSyncUser : IJob
    {
        
        public void Execute(IJobExecutionContext context)
        {
            var logger = LogManager.GetLogger(context.JobDetail.Key.Name);
            logger.Info("开始接收参数!");
            string PublicURL = context.JobDetail.JobDataMap.GetString("PublicURL");
            string constr = context.JobDetail.JobDataMap.GetString("ConnURL");
            if (!string.IsNullOrEmpty(constr))
                PublicVar.Connstr = constr;

            String UserID = context.JobDetail.JobDataMap.GetString("UserID");
            String UserName = context.JobDetail.JobDataMap.GetString("UserName");

            logger.Info("接口地址:PublicURL:" + PublicURL + "");
            logger.Info("客户端编号:UserID:" + UserID + "");
            logger.Info("客户端名称:UserName:" + UserName + "");

            logger.Info("获取数据库地址:" + PublicVar.Connstr + "");
                                        
            try
            {
                logger.Info("用户差异数据同步");

                int retuser = LCSyncUser.SyncUser(logger,PublicURL, UserID, "User", UserName);

                logger.Info("用户差异数据同步完成" + retuser.ToString() + "条");
            }
            catch (Exception error)
            {
                logger.Error("用户差异数据同步异常:" + error.Message);                
            }
           

        }
    }
}
