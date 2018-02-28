using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;  

namespace TF.YA.Org.Web
{
    /// <summary>
    /// List<T>导出类
    /// </summary>
    /// <typeparam name="T">列表中的元素类型</typeparam>
    public class ExcelHelperForIList<T>
    {
        /// <summary>
        /// 泛型导出Excel
        /// </summary>
        /// <param name="lt">要导出的List集合</param>
        /// <param name="fileName">文件名</param>
        /// <param name="fieldNames">导出的字段名()</param>
        /// <param name="showNames">Excel标题行(需与FieldNames对应)</param>
        /// <returns></returns>
        public static void CreateAdvExcel(IList<T> lt, string fileName, string[] fieldNames, string[] showNames)
        {
            //创建Excel
            IWorkbook workbook = new HSSFWorkbook();//创建Workbook对象  
            ISheet sheet = workbook.CreateSheet("Sheet1");//创建工作表  

            //获取类型的公共字段名称
            System.Reflection.FieldInfo[] myPropertyInfo = lt.First().GetType().GetFields(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);

            //Excel中创建标题行
            IRow row = sheet.CreateRow(0);//在工作表中添加一行  
            int i = 0, j;
            //设置到处字段标题
            for (int m = 0; m < fieldNames.Length; m++)
            {
                //遍历属性集合生成excel的表头标题  
                for (i = 0, j = myPropertyInfo.Length; i < j; i++)
                {
                    System.Reflection.FieldInfo pi = myPropertyInfo[i];
                    string headname = pi.Name;//单元格头部
                    if (headname == fieldNames[m])
                    {
                        ICell cell = row.CreateCell(m);//在行中添加一列  
                        cell.SetCellValue(showNames[m]);//设置列的内容  
                    }
                }
            }
            int r = 1;
            //遍历集合生成excel的行集数据
            foreach (T item in lt)
            {
                if (lt == null)
                {
                    continue;
                }
                row = sheet.CreateRow(r);//在工作表中添加一行  
                for (int m = 0; m < fieldNames.Length; m++)
                {
                    for (i = 0, j = myPropertyInfo.Length; i < j; i++)
                    {
                        FieldInfo pi = myPropertyInfo[i];
                        if (pi.Name == fieldNames[m])
                        {
                            string str = string.Format("{0}", pi.GetValue(item)).Replace("\n", "");

                            ICell cell = row.CreateCell(m);//在行中添加一列  
                            cell.SetCellValue(str);//设置列的内容  
                        }
                    }
                }

                r = r + 1;
            }

            MemoryStream s = new MemoryStream();
            
            workbook.Write(s);
            s.Flush();
            
            
            HttpContext.Current.Response.Clear();
            // 指定返回的是一个不能被客户端读取的流，必须被下载
            HttpContext.Current.Response.ContentType = "application/ms-excel";
            HttpContext.Current.Response.Charset = "UTF-8";
            HttpContext.Current.Response.Buffer = true;
            HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312");
            HttpContext.Current.Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}.xls", fileName));
            s.WriteTo(HttpContext.Current.Response.OutputStream);
            HttpContext.Current.Response.End();
        }
    }
}