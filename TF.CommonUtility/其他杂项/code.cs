using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.IO;
using System.Net;
using System.Text;
using System.Net.Mail;
using TF.CommonUtility;

namespace TF.CommonUtility
{
    public class code
    {
      
        /// <summary>
        /// 生成随机的数字
        /// </summary>
        /// <param name="VcodeNum">生成字母的个数</param>
        /// <returns>string</returns>
        public static string RndNum(int VcodeNum)
        {
            string Vchar = "1,2,3,4,5,6,7,8,9,0";
            string[] VcArray = Vchar.Split(',');
            string VNum = ""; //由于字符串很短，就不用StringBuilder了
            int temp = -1; //记录上次随机数值，尽量避免生产几个一样的随机数

            //采用一个简单的算法以保证生成随机数的不同
            Random rand = new Random();
            for (int i = 1; i < VcodeNum + 1; i++)
            {
                if (temp != -1)
                {
                    rand = new Random(i * temp * unchecked((int)DateTime.Now.Ticks));
                }
                int t = rand.Next(VcArray.Length);
                if (temp != -1 && temp == t)
                {
                    return RndNum(VcodeNum);
                }
                temp = t;
                VNum += VcArray[t];
            }
            return VNum;
        }

        /// <summary>
        /// 下载附件信息
        /// </summary>
        /// <param name="fname"></param>
        /// <param name="fileUpload"></param>
        /// <returns></returns>
        public static bool UploadFile(string fname, FileUpload fileUpload)
        {
            ClientScriptManager clientScript = null;
            //string fileContentType = fileUpload.PostedFile.ContentType;   //上传的文件类型
            //FileInfo file = new FileInfo(Server.MapPath("../uploadFile" + labPic.Text));
            //if (file.Exists)
            //    file.Delete(); //删除原来的文件
            string name = fileUpload.PostedFile.FileName;       // 客户端文件路径
            FileInfo file = new FileInfo(name);
            string fileType = file.Extension.ToLower();

            int ss = fileUpload.PostedFile.ContentLength;

            string fileName = fname; //DateTime.Now.ToString("yyyyMMddHHmmss") + file.Extension;   // 文件名称
            //labPic.Text = fileName;
            string strfilename = "/UploadFile/" + fileName;   //文件路径
            string webFilePath = System.Web.HttpContext.Current.Server.MapPath(@strfilename);           // 服务器端文件路径
           
            HttpPostedFile hpf = fileUpload.PostedFile;
            hpf.SaveAs(webFilePath);//保存文件
            return true;
        }


        /// <summary>
        /// access数据库还原
        /// </summary>
        /// <param name="fname"></param>
        /// <param name="fileUpload"></param>
        /// <returns></returns>
        public static bool UploadFileForAccess(string fname, FileUpload fileUpload)
        {
            //ClientScriptManager clientScript = null;
            //string fileContentType = fileUpload.PostedFile.ContentType;   //上传的文件类型
            //FileInfo file = new FileInfo(Server.MapPath("../uploadFile" + labPic.Text));
            //if (file.Exists)
            //    file.Delete(); //删除原来的文件
            string name = fileUpload.PostedFile.FileName;       // 客户端文件路径
            FileInfo file = new FileInfo(name);
            string fileType = file.Extension.ToLower();

            int ss = fileUpload.PostedFile.ContentLength;

            string fileName = fname; //DateTime.Now.ToString("yyyyMMddHHmmss") + file.Extension;   // 文件名称
            //labPic.Text = fileName;
            string strfilename = "/App_Data/" + fileName;   //文件路径
            string webFilePath = System.Web.HttpContext.Current.Server.MapPath(@strfilename);           // 服务器端文件路径
            //if (File.Exists(webFilePath))
            //{
            //    clientScript.RegisterStartupScript(fileUpload.GetType(), "1", "<script>alert('该文件已存在，请重新上传！');</script>");
            //    return false;
            //}
            HttpPostedFile hpf = fileUpload.PostedFile;
            hpf.SaveAs(webFilePath);//保存文件
            return true;
        }



      


     


        /// <summary>Excel导出</summary>
        /// <param name="dt">要导入的数据(存储于DataTable中的)</param>
        /// <param name="arrayList">需要导出的列名集合</param>
        public static bool ExportToExcel(Page page, DataTable dt, System.Collections.ArrayList arrayList)
        {
            StringWriter sw = new StringWriter();
            string columnName = "";

            for (int i = 0; i < arrayList.Count; i++)
            {
                if (columnName.Length == 0)
                {
                    columnName = columnName + ((KeyValue)arrayList[i]).Value.ToString();
                }
                else
                {
                    columnName = columnName + '\t' + ((KeyValue)arrayList[i]).Value.ToString();
                }
            }

            sw.WriteLine(columnName);

            foreach (DataRow dr in dt.Rows)
            {
                string rowString = "";
                for (int i = 0; i < arrayList.Count; i++)
                {
                    if (rowString.Length == 0)
                    {
                        string str = dr[((KeyValue)arrayList[i]).Key.ToString()].ToString().Trim().Length == 0 ? "  " : dr[((KeyValue)arrayList[i]).Key.ToString()].ToString();
                        rowString = rowString + str;
                    }
                    else
                    {
                        string str = dr[((KeyValue)arrayList[i]).Key.ToString()].ToString().Trim().Length == 0 ? "  " : dr[((KeyValue)arrayList[i]).Key.ToString()].ToString();
                        rowString = rowString + '\t' + str;
                    }
                }



                sw.WriteLine(rowString);
            }
            sw.Close();
            page.Response.AddHeader("Content-Disposition", "attachment; filename=" + dt.TableName + ".xls");
            page.Response.ContentType = "application/ms-excel";
            page.Response.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312");
            page.Response.Write(sw);
            page.Response.End();
            return true;
        }














































    }
}
