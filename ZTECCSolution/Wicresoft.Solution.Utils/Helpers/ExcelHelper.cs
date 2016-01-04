using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Reflection;
using System.Web;


namespace Wicresoft.Solution.Utils
{
    public class ExcelHelper
    {
        #region DataTable数据导出到Excel
        private static MemoryStream Export(int dRow, int dColumn, DataTable SourceTable)
        {
            if (SourceTable == null)
            {
                return null;
            }

            MemoryStream stream = new MemoryStream();
            HSSFWorkbook workbook = new HSSFWorkbook();
            string sheetName = String.IsNullOrEmpty(SourceTable.TableName) ? "Sheet1" : SourceTable.TableName;
            ISheet sheet = workbook.CreateSheet(sheetName);

            for (int i = 0; i < SourceTable.Rows.Count; i++)
            {
                IRow row = sheet.CreateRow(i + dRow);
                for (int j = 0; j < SourceTable.Columns.Count; j++)
                {
                    row.CreateCell(j + dColumn).SetCellValue(SourceTable.Rows[i][j].ToString());
                }
            }
            workbook.Write(stream);
            sheet = null;
            workbook = null;
            stream.Flush();
            stream.Position = 0L;
            return stream;
        }
        public static void Export(int dRow, int dColumn, DataTable SourceTable, string fileName)
        {
            if (SourceTable == null)
            {
                return;
            }

            FileStream stream = new FileStream(fileName, FileMode.OpenOrCreate);
            HSSFWorkbook workbook = new HSSFWorkbook();
            string sheetName = String.IsNullOrEmpty(SourceTable.TableName) ? "Sheet1" : SourceTable.TableName;
            ISheet sheet = workbook.CreateSheet(sheetName);

            for (int i = 0; i < SourceTable.Rows.Count; i++)
            {
                IRow row = sheet.CreateRow(i + dRow);
                for (int j = 0; j < SourceTable.Columns.Count; j++)
                {
                    row.CreateCell(j + dColumn).SetCellValue(SourceTable.Rows[i][j].ToString());
                }
            }
            workbook.Write(stream);
            sheet = null;
            workbook = null;
            stream.Flush();
            stream.Position = 0L;
        }
        /// <summary>
        /// datatable导出到excel，指定表头信息
        /// </summary>
        /// <param name="HeaderText">excel表头信息，如:{"编号","名称"}</param>
        /// <param name="HeaderField">excel对应的DataTable中字段名称，如{"ID","Name"}</param>
        /// <param name="SourceTable">DataTable数据源</param>
        /// <returns></returns>
        private static MemoryStream Export(string[] HeaderText, string[] HeaderField, DataTable SourceTable)
        {
            if (SourceTable == null)
            {
                return null;
            }

            MemoryStream stream = new MemoryStream();
            HSSFWorkbook workbook = new HSSFWorkbook();
            string sheetName = String.IsNullOrEmpty(SourceTable.TableName) ? "Sheet1" : SourceTable.TableName;
            ISheet sheet = workbook.CreateSheet(sheetName);
            //固定表头
            sheet.CreateFreezePane(0, 1);
            //导出excel表头;
            IRow hRow = sheet.CreateRow(0);
            hRow.HeightInPoints = 25;
            ICellStyle headStyle = workbook.CreateCellStyle();
            headStyle.Alignment = HorizontalAlignment.Center;
            IFont font = workbook.CreateFont();
            font.FontHeightInPoints = 10;
            font.Boldweight = 700;
            headStyle.SetFont(font);
            //headStyle.FillBackgroundColor =See getFillForegroundColor();
            for (int m = 0; m < HeaderText.Length; m++)
            {
                hRow.CreateCell(m).SetCellValue(HeaderText[m]);
                hRow.GetCell(m).CellStyle = headStyle;
            }

            for (int i = 0; i < SourceTable.Rows.Count; i++)
            {
                IRow row = sheet.CreateRow(i + 1);
                for (int j = 0; j < HeaderField.Length; j++)
                {
                    row.CreateCell(j).SetCellValue(SourceTable.Rows[i][HeaderField[j]].ToString());
                }
            }
            workbook.Write(stream);
            sheet = null;
            workbook = null;
            stream.Flush();
            stream.Position = 0L;
            return stream;
        }


        /// <summary>
        /// datatable导出到excel，指定表头信息
        /// </summary>
        /// <param name="HeaderText">excel表头信息，如:{"编号","名称"}</param>
        /// <param name="HeaderField">excel对应的DataTable中字段名称，如{"ID","Name"}</param>
        /// <param name="SourceTable">DataTable数据源</param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static void ExportToLocal(string[] HeaderText, string[] HeaderField, DataTable SourceTable, string fileName)
        {
            if (SourceTable == null)
            {
                return;
            }

            using (FileStream stream = new FileStream(fileName, FileMode.OpenOrCreate))
            {
                HSSFWorkbook workbook = new HSSFWorkbook();
                string sheetName = String.IsNullOrEmpty(SourceTable.TableName) ? "Sheet1" : SourceTable.TableName;
                ISheet sheet = workbook.CreateSheet(sheetName);

                //导出excel表头;
                IRow hRow = sheet.CreateRow(0);
                hRow.HeightInPoints = 25;
                ICellStyle headStyle = workbook.CreateCellStyle();
                headStyle.Alignment = HorizontalAlignment.Center;
                IFont font = workbook.CreateFont();
                font.FontHeightInPoints = 12;
                font.Boldweight = 700;
                headStyle.SetFont(font);
                for (int m = 0; m < HeaderText.Length; m++)
                {
                    hRow.CreateCell(m).SetCellValue(HeaderText[m]);
                    hRow.GetCell(m).CellStyle = headStyle;
                }

                for (int i = 0; i < SourceTable.Rows.Count; i++)
                {
                    IRow row = sheet.CreateRow(i + 1);
                    for (int j = 0; j < HeaderField.Length; j++)
                    {
                        row.CreateCell(j).SetCellValue(SourceTable.Rows[i][HeaderField[j]].ToString());
                    }
                }
                workbook.Write(stream);
                sheet = null;
                workbook = null;
                stream.Flush();
                stream.Position = 0L;
                stream.Close();
            }

        }
        /// <summary>
        /// DataTable导出到excel:只导出数据，无表头
        /// </summary>
        /// <param name="SourceTable">DataTable数据源</param>
        /// <param name="strFilename">导出文件名</param>
        public static void ExportToExcel(DataTable SourceTable, string strFilename)
        {
            HttpContext curContext = HttpContext.Current;
            curContext.Response.ContentType = "application/vnd.ms-excel";
            curContext.Response.ContentEncoding = System.Text.Encoding.UTF8;
            curContext.Response.Charset = "";
            curContext.Response.AppendHeader("Content-Disposition",
                 "attachment;filename=" + strFilename);
            curContext.Response.BinaryWrite(ExcelHelper.Export(0, 0, SourceTable).GetBuffer());
            curContext.Response.End();
        }

        /// <summary>
        /// datatable导出到excel，指定表头信息
        /// </summary>
        /// <param name="HeaderText">excel表头信息，如:{"编号","名称"}</param>
        /// <param name="HeaderField">excel对应的DataTable中字段名称，如{"ID","Name"}</param>
        /// <param name="SourceTable">DataTable数据源</param>
        /// <param name="strFilename">导出文件名</param>
        public static void ExportToExcel(string[] HeaderText, string[] HeaderField, DataTable SourceTable, string strFilename)
        {
            HttpContext curContext = HttpContext.Current;
            curContext.Response.ContentType = "application/vnd.ms-excel";
            curContext.Response.ContentEncoding = System.Text.Encoding.UTF8;
            curContext.Response.Charset = "";
            curContext.Response.AppendHeader("Content-Disposition",
                 "attachment;filename=" + strFilename);
            curContext.Response.BinaryWrite(ExcelHelper.Export(HeaderText, HeaderField, SourceTable).GetBuffer());
            curContext.Response.End();
        }

        /// <summary>
        /// 根据模板生成excel：生成复杂表头的excel，模板中的数据列需要和DataTable中的列一致
        /// 适用于Web页面
        /// </summary>
        /// <param name="ExcelPath">模板文件路径</param>
        /// <param name="dRow">从第几行开始写入</param>
        /// <param name="dColumn">从第几列开始写入</param>
        /// <param name="SourceTable">DataTable数据源</param>
        /// <param name="strFilename">Excel文件名</param>
        public static void ExportToExcelByTemplate(string ExcelPath, int dRow, int dColumn, DataTable SourceTable, string strFilename)
        {
            if ((SourceTable == null) || string.IsNullOrEmpty(ExcelPath))
            {
                return;
            }
            MemoryStream stream = new MemoryStream();
            HSSFWorkbook workbook = new HSSFWorkbook(new FileStream(ExcelPath, FileMode.Open, FileAccess.Read));
            string sheetName = String.IsNullOrEmpty(SourceTable.TableName) ? "Sheet1" : SourceTable.TableName;
            ISheet sheet = workbook.GetSheet(sheetName);
            for (int i = 0; i < SourceTable.Rows.Count; i++)
            {
                IRow row = sheet.CreateRow(i + dRow);
                for (int j = 0; j < SourceTable.Columns.Count; j++)
                {
                    row.CreateCell(j + dColumn).SetCellValue(SourceTable.Rows[i][j].ToString());
                }
            }
            workbook.Write(stream);
            sheet = null;
            workbook = null;
            stream.Flush();
            stream.Position = 0L;
            //return stream;
            System.Web.
            HttpContext curContext = HttpContext.Current;
            curContext.Response.ContentType = "application/vnd.ms-excel";
            curContext.Response.ContentEncoding = System.Text.Encoding.UTF8;
            curContext.Response.Charset = "";
            curContext.Response.AppendHeader("Content-Disposition",
                 "attachment;filename=" + strFilename);
            curContext.Response.BinaryWrite(stream.GetBuffer());
            curContext.Response.End();
        }
        public static void ExportToExcelByTemplate(string ExcelPath, DataTable SourceTable, string Title, string[] columnsNames, string strFilename)
        {
            MemoryStream stream = new MemoryStream();
            HSSFWorkbook workbook = new HSSFWorkbook(new FileStream(ExcelPath, FileMode.Open, FileAccess.Read));
            string sheetName = "Sheet1";
            ISheet sheet = workbook.GetSheet(sheetName);
            if (SourceTable != null)
            {
                var rowsCount = SourceTable.Rows.Count + 2;
                for (int i = 0; i < rowsCount; i++)
                {
                    IRow row = sheet.GetRow(i);
                    if (i == 0)
                    {
                        row.GetCell(0).SetCellValue(Title);
                    }
                    else if (i == 1)
                    {
                        for (int j = 0; j < columnsNames.Length; j++)
                        {
                            if (columnsNames[j] != null)
                            {
                                row.GetCell(j).SetCellValue(columnsNames[j]);
                            }
                        }
                    }
                    else
                    {
                        for (int j = 0; j < SourceTable.Columns.Count; j++)
                        {
                            if (SourceTable.Rows[i - 2][j] != null && !string.IsNullOrEmpty(SourceTable.Rows[i - 2][j].ToString()))
                            {
                                row.GetCell(j).SetCellValue(SourceTable.Rows[i - 2][j].ToString().ToDouble());
                            }
                        }
                    }

                }
            }
            workbook.Write(stream);
            sheet = null;
            workbook = null;
            stream.Flush();
            stream.Position = 0L;
            //return stream;
            System.Web.
            HttpContext curContext = HttpContext.Current;
            curContext.Response.ContentType = "application/vnd.ms-excel";
            curContext.Response.ContentEncoding = System.Text.Encoding.UTF8;
            curContext.Response.Charset = "";
            curContext.Response.AppendHeader("Content-Disposition",
                 "attachment;filename=" + strFilename);
            curContext.Response.BinaryWrite(stream.GetBuffer());
            curContext.Response.End();
        }

        /// <summary>
        /// 将DataTable保存至excel(不需要模板)
        /// 适用于无需http请求
        /// </summary>
        /// <param name="dRow">从第几行开始写入</param>
        /// <param name="dColumn">从第几列开始写入</param>
        /// <param name="titleCol">table名需要合并单元格的数量</param>
        /// <param name="title">table名</param>
        /// <param name="arryTitle">列表头</param>
        /// <param name="SourceTable">DataTable</param>
        /// <param name="strFilename">保存的路径</param>
        public static void ExportToExcelByNOPI(int dRow, int dColumn, int titleCol, string title, string[] arryTitle, DataTable SourceTable, string strFilename)
        {
            if ((SourceTable == null) || string.IsNullOrEmpty(strFilename))
            {
                return;
            }

            if (File.Exists(strFilename))
            {
                File.Delete(strFilename);
            }
            File.Create(strFilename).Close();

            FileStream stream = new FileStream(strFilename, FileMode.Open, FileAccess.ReadWrite);
            HSSFWorkbook workbook = new HSSFWorkbook();
            string sheetName = String.IsNullOrEmpty(SourceTable.TableName) ? "Sheet1" : SourceTable.TableName;
            ISheet sheet = workbook.CreateSheet(sheetName);

            IRow row;
            sheet.CreateRow(0);
            row = sheet.CreateRow(0);
            ICell cell = row.CreateCell(0);
            cell.SetCellValue(title);
            ICellStyle style = workbook.CreateCellStyle();
            //style.Alignment = CellStyle.ALIGN_CENTER;
            style.Alignment = HorizontalAlignment.Center;//居中显示
            style.IsLocked = true;//单元格是否锁定
            IFont font = workbook.CreateFont();
            //font.FontHeightInPoints = 20;
            style.SetFont(font);
            cell.CellStyle = style;
            sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(0, 0, 0, titleCol));

            row = sheet.CreateRow(1);
            for (int i = 0; i < SourceTable.Columns.Count; i++)
            {
                row.CreateCell(i).SetCellValue(arryTitle[i]);
            }
            for (int i = 0; i < SourceTable.Rows.Count; i++)
            {
                row = sheet.CreateRow(i + dRow);
                for (int j = 0; j < SourceTable.Columns.Count; j++)
                {
                    row.CreateCell(j + dColumn).SetCellValue(SourceTable.Rows[i][j].ToString());
                }
            }
            workbook.Write(stream);

        }
        #endregion

        #region Excel 数据导入到 DataTable
        /// <summary>
        /// 从Excel文件导入数据到DataTable：支持.xlsx,xls文件；返回的DataTable的名字为LeaderExp
        /// </summary>
        /// <param name="filePath">Excel文件路径</param>
        /// <returns>DataTable</returns>
        public static DataTable GetDataByExcelFile(string filePath)
        {
            var dt = new DataTable();
            var extension = Path.GetExtension(filePath);
            if (extension != null)
            {
                string extName = extension.ToLower();
                using (var myConn = new OleDbConnection())
                {
                    string strCon = " Provider = Microsoft.Jet.OLEDB.4.0 ; Data Source = " + filePath + ";Extended Properties='Excel 8.0;HDR=Yes;IMEX=1'";
                    if (extName == ".xlsx")
                    {
                        strCon = " Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filePath +
                                 ";Extended Properties='Excel 12.0 Xml;HDR=YES;IMEX=1'";
                    }
                    myConn.ConnectionString = strCon;
                    myConn.Open();
                    var dsExcel = new DataSet();
                    DataTable table = myConn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                    if (table != null)
                    {
                        string sheetName = table.Rows[0][2].ToString();
                        string strCom = " SELECT * FROM [" + sheetName + "] ";
                        var myCommand = new OleDbDataAdapter(strCom, myConn);
                        myCommand.Fill(dsExcel, sheetName);
                    }
                    if (dsExcel.Tables[0].Rows.Count > 0)
                    {
                        dt = dsExcel.Tables[0];
                    }
                }
            }
            return dt;
        }
        #endregion

        #region 将DataTable换成集合类转
        public static List<T> DataTableToList<T>(DataTable dataTable)
        {
            List<T> list = new List<T>();
            Type targetType = typeof(T);
            PropertyInfo[] allPropertyArray = targetType.GetProperties();
            foreach (DataRow rowElement in dataTable.Rows)
            {
                T element = Activator.CreateInstance<T>();
                foreach (DataColumn columnElement in dataTable.Columns)
                {
                    foreach (PropertyInfo property in allPropertyArray)
                    {
                        if (property.Name.Equals(columnElement.ColumnName))
                        {
                            if (rowElement[columnElement.ColumnName] == DBNull.Value)
                            {
                                property.SetValue(element, null, null);
                            }
                            else
                            {
                                property.SetValue(element, rowElement[columnElement.ColumnName], null);
                            }
                        }
                    }
                }
                list.Add(element);
            }
            return list;
        }
        #endregion

        #region 将集合类转换成DataTable
        /// <summary>
        /// 将集合类转换成DataTable
        /// create by blake
        /// </summary>
        /// <param name="list">集合</param>
        /// <returns></returns>
        public static DataTable ToDataTable(IList list)
        {
            DataTable result = new DataTable();
            if (list.Count > 0)
            {
                PropertyInfo[] propertys = list[0].GetType().GetProperties();
                foreach (PropertyInfo pi in propertys)
                {
                    result.Columns.Add(pi.Name, pi.PropertyType);
                }
                for (int i = 0; i < list.Count; i++)
                {
                    ArrayList tempList = new ArrayList();
                    foreach (PropertyInfo pi in propertys)
                    {
                        object obj = pi.GetValue(list[i], null);
                        tempList.Add(obj);
                    }
                    object[] array = tempList.ToArray();
                    result.LoadDataRow(array, true);
                }
            }
            return result;
        }
        #endregion

        public static DataSet ConvertToDataSet<T>(IList<T> list)
        {
            if (list == null || list.Count <= 0)
            {
                return null;
            }

            DataSet ds = new DataSet();
            DataTable dt = new DataTable(typeof(T).Name);
            DataColumn column;
            DataRow row;

            System.Reflection.PropertyInfo[] myPropertyInfo = typeof(T).GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);

            foreach (T t in list)
            {
                if (t == null)
                {
                    continue;
                }

                row = dt.NewRow();

                for (int i = 0, j = myPropertyInfo.Length; i < j; i++)
                {
                    System.Reflection.PropertyInfo pi = myPropertyInfo[i];

                    string name = pi.Name;

                    if (dt.Columns[name] == null)
                    {
                        column = new DataColumn(name, pi.PropertyType);
                        dt.Columns.Add(column);
                    }

                    row[name] = pi.GetValue(t, null);
                }

                dt.Rows.Add(row);
            }

            ds.Tables.Add(dt);

            return ds;
        }

        /// <summary>
        /// 将数据保存到文件（格式可以是.xls,.txt等;）
        /// 缺点：数据类型只能为整数型如：007显示为7
        /// 优点：适合大量数据，速度快
        /// </summary>
        /// <param name="table">DataTable</param>
        /// <param name="titles">行标题(按从左到右排列)</param>
        /// <param name="file">要保存的文件路径</param>
        public static void DataTableToCsv(DataTable table, string[] titles, string file)
        {
            string title = "";

            FileStream fs = new FileStream(file, FileMode.OpenOrCreate);
            StreamWriter sw = new StreamWriter(new BufferedStream(fs), System.Text.Encoding.Default);

            for (int i = 0; i < table.Columns.Count; i++)
            {
                title += titles[i] + "\t";//自动跳到下一单元格
            }
            title = title.Substring(0, title.Length - 1) + "\n";
            sw.Write(title);

            foreach (DataRow row in table.Rows)
            {
                string line = "";
                for (int i = 0; i < table.Columns.Count; i++)
                {
                    //line += row[i].ToString().Trim() + "\t"; //内容：自动跳到下一单元格
                    line += row[i].ToString().Trim() + "\t";//自动跳到下一单元格
                }
                line = line.Substring(0, line.Length - 1) + "\n";
                sw.Write(line);
            }
            sw.Close();
            fs.Close();
        }

        /// <summary>
        /// 将datatable保存至excel
        /// Microsoft.Office.Interop.Excel
        /// </summary>
        /// <param name="srcDataTable">数据源</param>
        /// <param name="arryTitle">excel列标题</param>
        /// <param name="excelFilePath">要保存的路径</param>
        public static void DataTableToExcel(System.Data.DataTable srcDataTable, string[] arryTitle, string excelFilePath)
        {
            Microsoft.Office.Interop.Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();
            object missing = System.Reflection.Missing.Value;

            //导出到execl 
            try
            {
                if (xlApp == null)
                {
                    return;
                }

                Microsoft.Office.Interop.Excel.Workbooks xlBooks = xlApp.Workbooks;
                Microsoft.Office.Interop.Excel.Workbook xlBook = xlBooks.Add(Microsoft.Office.Interop.Excel.XlWBATemplate.xlWBATWorksheet);
                Microsoft.Office.Interop.Excel.Worksheet xlSheet = (Microsoft.Office.Interop.Excel.Worksheet)xlBook.Worksheets[1];
                //range = (Microsoft.Office.Interop.Excel.Range)xlSheet.get_Range("A1", "K1");
                //xlSheet.Name
                //让后台执行设置为不可见，为true的话会看到打开一个Excel，然后数据在往里写
                xlApp.Visible = false;

                object[,] objData = new object[srcDataTable.Rows.Count + 1, srcDataTable.Columns.Count];
                //首先将数据写入到一个二维数组中
                for (int i = 0; i < srcDataTable.Columns.Count; i++)
                {
                    //objData[0, i] = srcDataTable.Columns[i].ColumnName;
                    objData[0, i] = arryTitle[i];
                }
                if (srcDataTable.Rows.Count > 0)
                {
                    for (int i = 0; i < srcDataTable.Rows.Count; i++)
                    {
                        for (int j = 0; j < srcDataTable.Columns.Count; j++)
                        {
                            objData[i + 1, j] = srcDataTable.Rows[i][j];
                        }
                    }
                }
                string startCol = "A";
                int iCnt = (srcDataTable.Columns.Count / 26);
                string endColSignal = (iCnt == 0 ? "" : ((char)('A' + (iCnt - 1))).ToString());
                string endCol = endColSignal + ((char)('A' + srcDataTable.Columns.Count - iCnt * 26 - 1)).ToString();
                Microsoft.Office.Interop.Excel.Range range = xlSheet.get_Range(startCol + "1", endCol + (srcDataTable.Rows.Count - iCnt * 26 + 1).ToString());
                range.NumberFormatLocal = "@";     //设置单元格格式为文本
                range.Value = objData; //给Exccel中的Range整体赋值
                range.EntireColumn.AutoFit(); //设定Excel列宽度自适应
                xlSheet.get_Range(startCol + "1", endCol + "1").Font.Bold = 1;//Excel文件列名 字体设定为Bold

                //设置禁止弹出保存和覆盖的询问提示框
                xlApp.DisplayAlerts = false;
                xlApp.AlertBeforeOverwriting = false;
                if (xlSheet != null)
                {
                    xlSheet.SaveAs(excelFilePath, missing, missing, missing, missing, missing, missing, missing, missing, missing);

                }
            }
            catch (Exception ex)
            {
                Log.Error(ex);
            }
            finally
            {
                xlApp.Quit(); // 退出 Excel
                xlApp = null; // 将 Excel 实例设置为空
            }
        }


    }
}
