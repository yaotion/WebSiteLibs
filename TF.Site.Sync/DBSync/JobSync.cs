using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Quartz;
using Common.Logging;

namespace TB.YA.Soft.DBSync
{
    [PersistJobDataAfterExecution]
    [DisallowConcurrentExecution]

    public class JobSync : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            ILog logger = Common.Logging.LogManager.GetLogger(context.JobDetail.Key.Name);


            string strSourceConn = context.JobDetail.JobDataMap.GetString("SourceConn");
            string strStoreConn = context.JobDetail.JobDataMap.GetString("StoreConn");
            string ObjectName = context.JobDetail.JobDataMap.GetString("ObjectName");
            logger.Info(string.Format("获取参数【数据源地址:SourceConn={0}】", strSourceConn));
            logger.Info(string.Format("获取参数【本地存储地址:StoreConn={0}】", strStoreConn));
            logger.Info(string.Format("获取参数【对象名称:ObjectName={0}】", ObjectName));
            TF.YA.Soft.DBSyncUser uSource = new TF.YA.Soft.DBSyncUser(strSourceConn);
            TF.YA.Soft.DBSyncObject uStore = new TF.YA.Soft.DBSyncObject(strStoreConn);
            logger.Info("开始执行更新");
            try
            {
                TF.YA.Soft.SyncUtils.Update(ObjectName, uSource, uStore);
                logger.Info("更新完毕");
            }
            catch (Exception e)
            {
                logger.Error("更新错误:" + e.Message);
            }
        }
    }
}
