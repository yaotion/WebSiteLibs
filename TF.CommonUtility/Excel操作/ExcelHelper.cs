using NPOI.HPSF;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace TF.CommonUtility
{
    /// <summary>
    /// Excel操作帮助类(本版本仅支持Office2003格式)
    /// </summary>
    public class ExcelHelper
    {
        /// <summary>
        /// 导出列名
        /// </summary>
        public static System.Collections.SortedList ListColumnsName;

        /// <summary>
        /// DataTable导出到Excel文件
        /// 调用:ExcelHelper.DataTableToExcel(dt, "test", "../File/test.xls");
        /// </summary>
        /// <param name="dt">要导出的DataTable</param>
        /// <param name="sheetName">工作表名称</param>
        /// <param name="saveFilePath">保存文件的路径</param>
        /// <returns>返回保存的路径</returns>
        public static string DataTableToExcel(DataTable dt, string sheetName, string saveFilePath)
        {
            if (ListColumnsName == null || ListColumnsName.Count == 0)
                throw (new Exception("请对ListColumnsName设置要导出的列名！"));

            var serverPath = HttpContext.Current.Server.MapPath(saveFilePath);
            var saveDir = serverPath.Substring(0, serverPath.LastIndexOf("\\"));
            if (!Directory.Exists(saveDir))
            {
                Directory.CreateDirectory(saveDir);
            }

            using (MemoryStream ms = DataTableToMemoryStream(dt, sheetName))
            {
                using (FileStream fs = new FileStream(serverPath, FileMode.Create, FileAccess.Write))
                {
                    byte[] data = ms.ToArray();
                    fs.Write(data, 0, data.Length);
                    fs.Flush();
                    return saveFilePath;
                }
            }
        }


        /// <summary>
        /// 从Web导出Excel
        /// 调用:ExcelHelper.ExportExcelFromWeb(dt,"test","test.xls");
        /// </summary>
        /// <param name="dt">要导出的DataTable</param>
        /// <param name="sheetName">工作表名称</param>
        /// <param name="fileName">文件名</param>
        public static void ExportExcelFromWeb(DataTable dt, string sheetName, string fileName)
        {
            if (ListColumnsName == null || ListColumnsName.Count == 0)
                throw (new Exception("请对ListColumnsName设置要导出的列名！"));

            HttpContext curContext = HttpContext.Current;
            //设置编码和附件格式
            curContext.Response.ContentType = "application/ms-excel";
            curContext.Response.ContentEncoding = Encoding.UTF8;
            curContext.Response.AppendHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(fileName, Encoding.UTF8));
            curContext.Response.BinaryWrite(DataTableToMemoryStream(dt, sheetName).GetBuffer());
            curContext.Response.End();
        }


        /// <summary>
        /// DataTable导出到Excel的MemoryStream
        /// </summary>
        /// <param name="dt">要导出的DataTable</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>MemoryStream</returns>
        public static MemoryStream DataTableToMemoryStream(DataTable dt, string sheetName)
        {
            int rowIndex = 0;
            //创建工作簿
            HSSFWorkbook workbook = new HSSFWorkbook();
            HSSFSheet sheet = workbook.CreateSheet(sheetName) as HSSFSheet;

            //填充内容
            foreach (DataRow row in dt.Rows)
            {
                if (rowIndex == 65535 || rowIndex == 0)//Office2003单个Sheet最多存65535条数据
                {
                    if (rowIndex != 0)//超过65535条数据新建Sheet
                    {
                        sheet = workbook.CreateSheet("Sheet" + (int)(rowIndex / 65535 + 0.5)) as HSSFSheet;
                    }
                    //创建表头,填充列头及样式
                    CreateHeader(workbook, sheet, dt);
                    rowIndex = 1;
                }

                //填充行
                CreateRow(workbook, sheet, rowIndex, row);
                rowIndex++;
            }
            using (MemoryStream ms = new MemoryStream())
            {
                workbook.Write(ms);
                ms.Flush();
                ms.Position = 0;
                return ms;
            }
        }

        /// <summary>
        /// 创建Sheet第一行(Excel表头)
        /// </summary>
        /// <param name="workbook">要创建表头的工作簿</param>
        /// <param name="sheet">要创建表头的Sheet</param>
        /// <param name="DataTable">要导出的DataTable</param>
        private static void CreateHeader(HSSFWorkbook workbook, HSSFSheet sheet, DataTable dt)
        {
            int cellIndex = 0;
            HSSFRow headerRow = sheet.CreateRow(0) as HSSFRow;

            HSSFCellStyle headStyle = workbook.CreateCellStyle() as HSSFCellStyle;
            headStyle.Alignment = HorizontalAlignment.Center;
            HSSFFont font = workbook.CreateFont() as HSSFFont;
            font.FontHeightInPoints = 10;
            font.Boldweight = 700;
            headStyle.IsLocked = true;
            headStyle.SetFont(font);

            //取得列宽
            int[] arrayColumnWidth = new int[ListColumnsName.Count];
            int index = 0;
            foreach (System.Collections.DictionaryEntry de in ListColumnsName)
            {
                arrayColumnWidth[index] = Encoding.GetEncoding(936).GetBytes(de.Value.ToString()).Length;
                index++;
            }
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                for (int j = 0; j < ListColumnsName.Count; j++)
                {
                    //列名称
                    string columnName = ListColumnsName.GetKey(j).ToString();
                    int intTemp = Encoding.GetEncoding(936).GetBytes(dt.Rows[i][columnName].ToString()).Length;
                    if (intTemp > arrayColumnWidth[j])
                    {
                        arrayColumnWidth[j] = intTemp;
                    }
                }
            }

            //循环导出列
            foreach (System.Collections.DictionaryEntry de in ListColumnsName)
            {
                headerRow.CreateCell(cellIndex).SetCellValue(de.Value.ToString());
                headerRow.GetCell(cellIndex).CellStyle = headStyle;


                //int maxColumnWidth = 255*256;// 1/256为一个字符宽度单位
                int maxColumnWidth = 80*256;//设置80个字符列宽为最大

                int setColumn = (arrayColumnWidth[cellIndex] + 1)*256;//要设置的列宽

                if (setColumn >= maxColumnWidth)
                    setColumn = maxColumnWidth;

                //设置列宽
                sheet.SetColumnWidth(cellIndex, setColumn);

                cellIndex++;
            }
            sheet.CreateFreezePane(0, 1, 0, ListColumnsName.Count - 1);
        }


        /// <summary>
        /// 按ListColumnsName列名序列，在Excel的指定Sheet中写入行
        /// </summary>
        /// <param name="workbook">工作簿</param>
        /// <param name="sheet">要写入数据的Sheet</param>
        /// <param name="rowIndex">要写入的行索引</param>
        /// <param name="tableRow">要写入的DataRow</param>
        private static void CreateRow(HSSFWorkbook workbook, HSSFSheet sheet, int rowIndex, DataRow tableRow)
        {
            HSSFRow dataRow = sheet.CreateRow(rowIndex) as HSSFRow;
            for (int cellIndex = 0; cellIndex < ListColumnsName.Count; cellIndex++)
            {
                //列名称
                string columnsName = ListColumnsName.GetKey(cellIndex).ToString();
                HSSFCell newCell = dataRow.CreateCell(cellIndex) as HSSFCell;
                System.Type rowType = tableRow[columnsName].GetType();
                string drValue = tableRow[columnsName].ToString().Trim();
                switch (rowType.ToString())
                {
                    case "System.String"://字符串类型
                        newCell.SetCellValue(drValue);
                        break;
                    case "System.DateTime"://日期类型
                        DateTime dateV;
                        DateTime.TryParse(drValue, out dateV);
                        newCell.SetCellValue(dateV);

                        //格式化显示
                        HSSFCellStyle dateStyle = workbook.CreateCellStyle() as HSSFCellStyle;
                        HSSFDataFormat format = workbook.CreateDataFormat() as HSSFDataFormat;
                        dateStyle.DataFormat = format.GetFormat("yyyy-MM-dd HH:mm:ss");
                        newCell.CellStyle = dateStyle;
                        break;
                    case "System.Boolean"://布尔型
                        bool boolV = false;
                        bool.TryParse(drValue, out boolV);
                        newCell.SetCellValue(boolV);
                        break;
                    case "System.Int16"://整型
                    case "System.Int32":
                    case "System.Int64":
                    case "System.Byte":
                        int intV = 0;
                        int.TryParse(drValue, out intV);
                        newCell.SetCellValue(intV);
                        break;
                    case "System.Decimal"://浮点型
                    case "System.Double":
                        double doubV = 0;
                        double.TryParse(drValue, out doubV);
                        newCell.SetCellValue(doubV);
                        break;
                    case "System.DBNull"://空值处理
                        newCell.SetCellValue("");
                        break;
                    default:
                        newCell.SetCellValue("");
                        break;
                }
            }
        }


        /// <summary>
        /// 读取Excel到DataTable(默认第一行为列头)
        /// 调用:DataTable dt = ExcelHelper.ExcelToDataTable("/File/test.xls");
        /// </summary>
        /// <param name="filePath">Excel文档路径(相对路径)</param>
        /// <returns>DataTable</returns>
        public static DataTable ExcelToDataTable(string filePath)
        {
            var serverPath = HttpContext.Current.Server.MapPath(filePath);
            DataTable dt = new DataTable();
            HSSFWorkbook hssfworkbook;
            using (FileStream fs = new FileStream(serverPath, FileMode.Open, FileAccess.Read))
            {
                hssfworkbook = new HSSFWorkbook(fs);
            }
            HSSFSheet sheet = hssfworkbook.GetSheetAt(0) as HSSFSheet;
            HSSFRow headerRow = sheet.GetRow(0) as HSSFRow;
            int cellCount = headerRow.LastCellNum;

            for (int j = 0; j < cellCount; j++)
            {
                HSSFCell cell = headerRow.GetCell(j) as HSSFCell;
                dt.Columns.Add(cell.ToString());
            }

            for (int i = (sheet.FirstRowNum + 1); i <= sheet.LastRowNum; i++)
            {
                HSSFRow row = sheet.GetRow(i) as HSSFRow;
                DataRow dataRow = dt.NewRow();
                for (int j = row.FirstCellNum; j < cellCount; j++)
                {
                    if (row.GetCell(j) != null)
                        dataRow[j] = row.GetCell(j).ToString();
                }
                dt.Rows.Add(dataRow);
            }
            return dt;
        }

        /// <summary>
        /// 排序实现接口 不进行排序 根据添加顺序导出
        /// </summary>
        public class NoSort : System.Collections.IComparer
        {
            public int Compare(object x, object y)
            {
                return -1;
            }
        }
    }
}