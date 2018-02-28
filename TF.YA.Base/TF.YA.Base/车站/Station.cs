using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TF.YA.Base
{
    public class Station
    {
        //自增编号
        public int nid;
        /// <summary>
        /// 车站名称
        /// </summary>
        public string StationName;
        /// <summary>
        /// 车站名简拼
        /// </summary>
        public string NameJP;
        /// <summary>
        /// 运记信息(车站号-交路号-车站号,车站号-交路号-车站号)
        /// </summary>
        public string StationNumber;
    }
}
