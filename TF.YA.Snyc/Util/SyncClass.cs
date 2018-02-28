using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.Reflection;
namespace TF.YA.Sync
{
    [AttributeUsage(AttributeTargets.Property)]
    public class DataMemberAttribute : Attribute { };

    public class SyncClass
    {
        private string _Md5;

        private string CreateMd5()
        {
            string ret = "";
            using (MD5CryptoServiceProvider md5Coding = new MD5CryptoServiceProvider())
            {
                string objString = ToString();
                byte[] bs = Encoding.Unicode.GetBytes(objString);
                byte[] codingbs = md5Coding.ComputeHash(bs);
                foreach (byte b in codingbs)
                {
                    ret += b.ToString("x2");
                }

            }
            return ret;
        }
        //获取对象的MD5值
        public string Md5
        {
            get
            {
                if (string.IsNullOrEmpty(_Md5))
                {
                    _Md5 = CreateMd5();
                    return _Md5;
                }
                return _Md5;
            }
            set { _Md5 = value; }
        }

        public virtual string Key { get { return ""; } }

        public override string ToString()
        {
            StringBuilder ret = new StringBuilder();

            PropertyInfo[] propertys = GetType().GetProperties();

            foreach (var pro in propertys)
            {

                object[] attrs = pro.GetCustomAttributes(typeof(DataMemberAttribute), false);

                if (attrs.Length > 0)
                {
                    object obj = pro.GetValue(this, null);
                    if (obj != null)
                    {
                        ret.Append(obj.ToString());
                    }
                }
            }
            return ret.ToString();
        }
    }
}
