using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Web;
using System.IO;

namespace TF.CommonUtility
{
    /// <summary>
    /// 简单文件上传 by lzy 2014-09-04-09-30
    /// </summary>
    public class UploadFile
    {
        #region 上传处理方法

        /// <summary>
        /// 简单上传
        /// </summary>
        /// <param name="postedFile">上传文件对象</param>
        /// <param name="storagePath">存储路径</param>
        /// <param name="FileNameSuffix">文件名称后缀</param>
        /// <returns></returns>
        public static string fileSaveAs(HttpPostedFile postedFile, string storagePath, string FileNameSuffix)
        {
            try
            {
                string fileExt = GetFileExt(postedFile.FileName); //文件扩展名，不含“.”
                string originalFileName = postedFile.FileName.Substring(postedFile.FileName.LastIndexOf(@"\") + 1); //取得文件原名
                string fileName = DateTime.Now.ToString("yyyyMMddHHmmss") + (FileNameSuffix != "" ? FileNameSuffix : "") + "." + fileExt; //按时间命名文件
                string dirPath = storagePath;

                //检查文件扩展名是否合法
                if (!CheckFileExt(fileExt))
                {
                    return "{\"Success\": 0, \"ResultText\": \"不允许上传" + fileExt + "类型的文件！\"}";
                }
                //获得要保存的文件路径
                string serverFileName = dirPath + fileName;
                string returnFileName = serverFileName;
                //物理完整路径                    
                string toFileFullPath = HttpContext.Current.Server.MapPath(dirPath);
                //检查有该路径是否就创建
                if (!Directory.Exists(toFileFullPath))
                {
                    Directory.CreateDirectory(toFileFullPath);
                }
                //保存文件
                postedFile.SaveAs(toFileFullPath + fileName);
                //return "{\"Success\": 1, \"ResultText\": \"" + returnFileName + "\"}";
                return returnFileName;
            }
            catch
            {
                //return "{\"Success\": 0, \"ResultText\": \"上传过程中发生意外错误！\"}";
                return "";
            }
        }

        /// <summary>
        /// 检查是否为合法的上传文件
        /// </summary>
        private static bool CheckFileExt(string _fileExt)
        {
            //检查危险文件
            string[] excExt = { "asp", "aspx", "php", "jsp", "htm", "html" };
            for (int i = 0; i < excExt.Length; i++)
            {
                if (excExt[i].ToLower() == _fileExt.ToLower())
                {
                    return false;
                }
            }
            //检查合法文件
            string[] allowExt = { "gif", "jpg", "png", "bmp", "rar", "zip", "doc", "xls", "txt" };
            for (int i = 0; i < allowExt.Length; i++)
            {
                if (allowExt[i].ToLower() == _fileExt.ToLower())
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 返回文件扩展名，不含“.”
        /// </summary>
        /// <param name="_filepath">文件全名称</param>
        /// <returns>string</returns>
        private static string GetFileExt(string _filepath)
        {
            if (string.IsNullOrEmpty(_filepath))
            {
                return "";
            }
            if (_filepath.LastIndexOf(".") > 0)
            {
                return _filepath.Substring(_filepath.LastIndexOf(".") + 1); //文件扩展名，不含“.”
            }
            return "";
        }

        #endregion
    }
}
