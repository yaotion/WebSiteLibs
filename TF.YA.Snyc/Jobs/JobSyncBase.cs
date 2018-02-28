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
    public class JobSyncBase : IJob
    {
        ImportDeptDataCls dodept = new ImportDeptDataCls();
        ImportDutyUserBll dutyuserbll = new ImportDutyUserBll();
        public void Execute(IJobExecutionContext context)
        {
            var logger = LogManager.GetLogger(context.JobDetail.Key.Name);
            logger.Info("开始接收参数!");
            string PublicURL = context.JobDetail.JobDataMap.GetString("PublicURL");
            logger.Info("接口地址:" + PublicURL + "");
            string constr = context.JobDetail.JobDataMap.GetString("ConnURL");
            if (!string.IsNullOrEmpty(constr))
                PublicVar.Connstr = constr;
            logger.Info("获取数据库地址:" + PublicVar.Connstr + "");

            try
            {
                logger.Info("开始部门组织结构数据同步");
                logger.Info("开始区域同步");
                int bret = dodept.DoArea(PublicURL);
                Thread.Sleep(20);
                logger.Info("区域同步完成" + bret.ToString() + "条");

            }
            catch (Exception error)
            {
                logger.Info("区域同步异常");
                logger.Info(error.Message);
            }
            try
            {
                logger.Info("工作车间同步开始");
                int bret = dodept.DoWorkShop(PublicURL);
                Thread.Sleep(20);
                logger.Info("工作车间同步完成" + bret.ToString() + "条");

            }
            catch (Exception error)
            {
                logger.Info("工作车间同步异常");
                logger.Info(error.Message);
            }
            try
            {
                logger.Info("开始大队数据同步");
                int bret = dodept.DoGroup(PublicURL);
                Thread.Sleep(20);
                logger.Info("大队数据同步完成" + bret.ToString() + "条");

            }
            catch (Exception error)
            {
                logger.Info("大队数据同步异常");
                logger.Info(error.Message);
            }
            try
            {
                logger.Info("开始小组数据同步");
                int bret = dodept.DoSmallGroup(PublicURL);
                Thread.Sleep(20);
                logger.Info("小组数据同步完成" + bret.ToString() + "条");

            }
            catch (Exception error)
            {
                logger.Info("部门组织结构数据同步异常");
                logger.Info(error.Message);
            }
            try
            {
                logger.Info("开始地点数据同步");
                int bret = dodept.DoPlace(PublicURL);
                Thread.Sleep(20);
                logger.Info("地点数据同步完成" + bret.ToString() + "条");
            }
            catch (Exception error)
            {
                logger.Info("地点数据同步异常");
                logger.Info(error.Message);
            }
            try
            {
                logger.Info("区段数据同步");
                int bret = dodept.DoSection(PublicURL);
                Thread.Sleep(20);
                logger.Info("区段数据同步完成" + bret.ToString() + "条");

            }
            catch (Exception error)
            {
                logger.Info("区段数据同步异常");
                logger.Info(error.Message);
            }
            try
            {
                logger.Info("车站数据同步");
                int bret = dodept.DoStation(PublicURL);
                Thread.Sleep(20);
                logger.Info("车站数据同步完成" + bret.ToString() + "条");

            }
            catch (Exception error)
            {
                logger.Info("车站数据同步异常");
                logger.Info(error.Message);
            }
            try
            {
                logger.Info("值班员数据同步");
                int bret = dutyuserbll.DoDutyUser(PublicURL);
                Thread.Sleep(20);
                logger.Info("值班员数据同步完成" + bret.ToString() + "条");

            }
            catch (Exception error)
            {
                logger.Info("值班员数据同步异常");
                logger.Info(error.Message);
            }



            try
            {
                logger.Info("用户职位数据同步");
                int bpost = LCServerUser.DoPosts(PublicURL);
                logger.Info("用户职位数据同步完成" + bpost.ToString() + "条");
            }
            catch (Exception error)
            {
                logger.Info("用户职位数据同步异常");
                logger.Info(error.Message);
            }

        }
    }
}
