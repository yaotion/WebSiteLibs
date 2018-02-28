using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TF.YA.Statistics
{
    public class TrainCount
    {
        public string JWDCode = "";
        public string JWDName = "";
        public int PeiShuCount = 0;
        public int ZhiPeiCount = 0;
        public int YunYongCount = 0;
        public int FeiYongCount = 0;
    }

    public class TrainTJ
    {
        public DateTime TJDay;
        public string JWDCode;
        public string JWDName;
        public double TR;
        public double ZX;
        public double ZZ;
        public double SD;
        public double CL;
    }
}
