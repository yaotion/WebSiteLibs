using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.Logging;
using Quartz;

namespace TF.YA.Statistics
{
    [PersistJobDataAfterExecution]
    [DisallowConcurrentExecution]
    public class JobStatistics : IJob
    {
        
        public void Execute(IJobExecutionContext context)
        {
            var logger = LogManager.GetLogger(context.JobDetail.Key.Name);
            try
            {
                string DiDianID = context.JobDetail.JobDataMap.GetString("DiDianID");
                string LocalDBConn = context.JobDetail.JobDataMap.GetString("LocalDBConn");
                string YAConn = context.JobDetail.JobDataMap.GetString("YAConn");
                string JCConn = context.JobDetail.JobDataMap.GetString("JCConn");
                string JCJWDID = context.JobDetail.JobDataMap.GetString("JCJWDID");
                string JCBJConn = context.JobDetail.JobDataMap.GetString("JCBJConn");
                string JCTJConn = context.JobDetail.JobDataMap.GetString("JCTJConn");
                
               


                logger.Info("获取地点编号DiDianID:" + DiDianID);
                logger.Info("获取本地数据库连接LocalDBConn:" + LocalDBConn);
                logger.Info("获取运安数据库连接YAConn:" + YAConn);
                logger.Info("获取机车数据库连接JCConn:" + JCConn);
                logger.Info("获取机车机务段编号JCJWDID:" + JCJWDID);
                logger.Info("获取机车统计数据库连接JCTJConn:" + JCTJConn);

                logger.Info("开始同步天气");
                LCWeatherSync.ExecSync(logger, DiDianID, LocalDBConn);
                logger.Info("同步天气完成");

                logger.Info("开始同步乘务信息");
                LCPlanSync.ExecSync(logger, YAConn, LocalDBConn);
                logger.Info("同步乘务信息完成");

                logger.Info("开始同步机车数据");
                LCTrainSync.ExecSync(logger,JCConn,LocalDBConn,JCJWDID);
                logger.Info("同步机车数据完成");

                logger.Info("开始同步机车报警");
                LCTrainBJSync.ExecSync(logger,JCBJConn, LocalDBConn, DateTime.Now.Date);
                logger.Info("同步机车报警完成");

                logger.Info("开始同步机车统计");
                LCTrainTJSync.ExecSync(logger, JCTJConn, LocalDBConn, DateTime.Now.Date);
                logger.Info("同步机车统计完成");
            }
            catch(Exception e)
            {
                logger.Error("同步异常" + e.Message);
            }
        }
    }
}
