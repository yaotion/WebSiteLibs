using System;
using System.IO;
using System.Collections;
using System.Web;
using System.Text;
using System.Web.UI;

namespace TF.CommonUtility
{
    /// <summary>
    /// 类名:FileUtil
    /// 描述:通用文件操作类
    /// </summary>
   public sealed class FileUtil
    {
        /// <summary>
        /// 获取文件名(包含扩展名)
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <returns></returns>
        public static String GetFileName(string filePath)
        {
            return Path.GetFileName(filePath);
        }

        /// <summary>
        /// 获取不含扩展名的文件名
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <returns></returns>
        public static string GetFileNameWithoutExtension(string filePath)
        {
            return Path.GetFileNameWithoutExtension(filePath);
        }

        /// <summary>
        /// 获取扩展名
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <returns></returns>
        public static String GetExtension(string filePath)
        {
            return Path.GetExtension(filePath);
        }

        /// <summary>
        /// 读取文本文件内容,每行存入arrayList 并返回arrayList对象
        /// </summary>
        /// <param name="sFileName"></param>
        /// <returns>arrayList</returns>
        public static ArrayList ReadFileRow(string sFileName)
        {
            string sLine = "";
            ArrayList alTxt = null;
            try
            {
                using (StreamReader sr = new StreamReader(sFileName))
                {
                    alTxt = new ArrayList();

                    while (!sr.EndOfStream)
                    {
                        sLine = sr.ReadLine();
                        if (sLine != "")
                        {
                            alTxt.Add(sLine.Trim());
                        }

                    }
                    sr.Close();
                }
            }
            catch
            {

            }
            return alTxt;
        }


        /// <summary>
        /// 备份文件
        /// </summary>
        /// <param name="sourceFileName">源文件名</param>
        /// <param name="destFileName">目标文件名</param>
        /// <param name="overwrite">当目标文件存在时是否覆盖</param>
        /// <returns>操作是否成功</returns>
        public static bool BackupFile(string sourceFileName, string destFileName, bool overwrite)
        {
            if (!System.IO.File.Exists(sourceFileName))
                throw new FileNotFoundException(sourceFileName + "文件不存在！");

            if (!overwrite && System.IO.File.Exists(destFileName))
                return false;

            try
            {
                System.IO.File.Copy(sourceFileName, destFileName, true);
                return true;
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        /// <summary>
        /// 备份文件,当目标文件存在时覆盖
        /// </summary>
        /// <param name="sourceFileName">源文件名</param>
        /// <param name="destFileName">目标文件名</param>
        /// <returns>操作是否成功</returns>
        public static bool BackupFile(string sourceFileName, string destFileName)
        {
            return BackupFile(sourceFileName, destFileName, true);
        }


        /// <summary>
        /// 恢复文件
        /// </summary>
        /// <param name="backupFileName">备份文件名</param>
        /// <param name="targetFileName">要恢复的文件名</param>
        /// <param name="backupTargetFileName">要恢复文件再次备份的名称,如果为null,则不再备份恢复文件</param>
        /// <returns>操作是否成功</returns>
        public static bool RestoreFile(string backupFileName, string targetFileName, string backupTargetFileName)
        {
            try
            {
                if (!System.IO.File.Exists(backupFileName))
                    throw new FileNotFoundException(backupFileName + "文件不存在！");

                if (backupTargetFileName != null)
                {
                    if (!System.IO.File.Exists(targetFileName))
                        throw new FileNotFoundException(targetFileName + "文件不存在！无法备份此文件！");
                    else
                        System.IO.File.Copy(targetFileName, backupTargetFileName, true);
                }
                System.IO.File.Delete(targetFileName);
                System.IO.File.Copy(backupFileName, targetFileName);
            }
            catch (Exception e)
            {
                throw e;
            }
            return true;
        }

        /// <summary>
        /// 恢复文件（不再备份恢复文件）
        /// </summary>
        /// <param name="backupFileName"></param>
        /// <param name="targetFileName"></param>
        /// <returns></returns>
        public static bool RestoreFile(string backupFileName, string targetFileName)
        {
            return RestoreFile(backupFileName, targetFileName, null);
        }
        //看文件或文件夹是否存在
        public static bool FileExists(string strPath)
        {
            if (!File.Exists(strPath))
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 换算单位
        /// </summary>
        /// <param name="filelength"></param>
        /// <returns></returns>
        public static string ChangeUnit(double filelength)
        {
            if (filelength < 1024)
            {
                return filelength.ToString("f2") + " b";
            }
            else if (filelength < (1024 * 1024))
            {
                return (filelength / 1024).ToString("f2") + " kb";
            }
            else
            {
                return (filelength / 1024 / 1024).ToString("f2") + " mb";
            }
        }


        #region 返回文件流,提供下载
        /// <summary>
        /// 返回文件流,提供下载
        /// </summary>
        /// <param name="filePath">文件全路径[绝对路径]</param>
        public static void ReturnHTTPStream(string filePath)
        {
            FileInfo info = new FileInfo(filePath);
            HttpResponse response = HttpContext.Current.Response;
            response.Charset = "UTF-8";
            response.HeaderEncoding = Encoding.UTF8;
            response.AppendHeader("Content-Disposition", "attachment;filename=" + HttpContext.Current.Server.UrlEncode(info.Name));
            response.AddHeader("Content-Length", info.Length.ToString());
            response.AppendHeader("Last-Modified", info.LastWriteTime.ToFileTime().ToString());
            response.ContentType = GetResponseContentType(info.Extension);
            response.WriteFile(info.FullName);
            response.Flush();
            response.End();
        }

        /// <summary>
        /// 打开对应文件类型 
        /// </summary>
        /// <param name="fileType">文件扩展名</param>
        /// <returns></returns>
        private static string GetResponseContentType(string fileType)
        {
            string contentType = "appliction/octet-stream";
            switch (fileType.ToLower())
            {
                case ".doc":
                    contentType = "application/msword"; break;
                case ".xls":
                case ".xlt":
                    contentType = "application/msexcel"; break;
                case ".txt":
                    contentType = "text/plain"; break;
                case ".pdf":
                    contentType = "application/pdf"; break;
                case ".ppt":
                    contentType = "appication/powerpoint"; break;
            }
            return contentType;
        }
        #endregion

        #region 上传文件
        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="fileUpload"></param>
        /// <returns></returns>
        public static bool UploadFile(string strFileName, System.Web.UI.WebControls.FileUpload fileUpload)
        {
            string filename = strFileName != "" ? strFileName : fileUpload.FileName;              //获取Excel文件名  DateTime日期函数
            string savePath = System.Web.HttpContext.Current.Server.MapPath(("/FileUpload/") + filename);//Server.MapPath 获得虚拟服务器相对路径
            fileUpload.SaveAs(savePath);                        //SaveAs 将上传的文件内容保存在服务器上
            return true;
        }
        #endregion

        #region 读取文件内容
        /// <summary>
        /// 读取文件内容
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string ReadAllContents(string path)
        {
            string str = "";
            using (StreamReader sr = new StreamReader(path, System.Text.Encoding.GetEncoding("GB2312")))
            {
                str = sr.ReadToEnd();
            }
            return str;
        }
        #endregion

        #region 追加内容至文件中
        /// <summary>
        /// 追加内容至文件中
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="contents"></param>
        public static void AppendStringToFile(string fileName, string contents)
        {
            StreamWriter sWriter = null;
            try
            {
                FileStream fileStream = new FileStream(fileName, FileMode.Append, FileAccess.Write);
                sWriter = new StreamWriter(fileStream);
                sWriter.Write(contents);
            }
            finally
            {
                if (sWriter != null)
                {
                    sWriter.Close();
                }
            }
        }
        #endregion

        #region 获取文件Md5值
        /// <summary>
        /// 获取文件Md5值
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static string GetMD5HashFromFile(string filePath)
        {
            try
            {
                FileStream file = new FileStream(filePath, FileMode.Open);
                System.Security.Cryptography.MD5 md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
                byte[] retVal = md5.ComputeHash(file);
                file.Close();
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < retVal.Length; i++)
                {
                    sb.Append(retVal[i].ToString("x2"));
                }
                return sb.ToString().ToUpper();
            }
            catch (Exception ex)
            {
                throw new Exception("GetMD5HashFromFile() fail,error:" + ex.Message);
            }
        }
        #endregion

        #region 新添加函数 by lzy 2014-11-13-15-40

        /// <summary>
        /// 删除单个文件
        /// </summary>
        /// <param name="_filepath">文件相对路径</param>
        public static bool DeleteFile(string _filepath)
        {
            if (string.IsNullOrEmpty(_filepath))
            {
                return false;
            }
            string fullpath = GetMapPath(_filepath);
            if (File.Exists(fullpath))
            {
                File.Delete(fullpath);
                return true;
            }
            return false;
        }

        /// <summary>
        /// 获得当前绝对路径
        /// </summary>
        /// <param name="strPath">指定的路径</param>
        /// <returns>绝对路径</returns>
        public static string GetMapPath(string strPath)
        {
            if (strPath.ToLower().StartsWith("http://"))
            {
                return strPath;
            }
            if (HttpContext.Current != null)
            {
                return HttpContext.Current.Server.MapPath(strPath);
            }
            else //非web程序引用
            {
                strPath = strPath.Replace("/", "\\");
                if (strPath.StartsWith("\\"))
                {
                    strPath = strPath.Substring(strPath.IndexOf('\\', 1)).TrimStart('\\');
                }
                return System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, strPath);
            }
        }

        #endregion

    }
}
