using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using NPOI.HSSF.UserModel;
using NPOI.HSSF.Util;
using NPOI.SS.Util;
using System.IO;
using System.Data;
using System.Web;

namespace Wicresoft.Solution.Utils
{
    public class NPOIExcelHelper : IDisposable
    {
        private string fileName = null; //文件名
        private IWorkbook workbook = null;
        private FileStream fs = null;
        private bool disposed;

        public NPOIExcelHelper(string fileName)
        {
            this.fileName = fileName;
            disposed = false;
        }

        #region DataTableToExcel
        /// <summary>
        /// 将DataTable数据导入到excel中
        /// </summary>
        /// <param name="data">要导入的数据</param>
        /// <param name="isColumnWritten">DataTable的列名是否要导入</param>
        /// <param name="sheetName">要导入的excel的sheet的名称</param>
        /// <returns>导入数据行数(包含列名那一行)</returns>
        public int DataTableToExcel(DataTable data, string sheetName, bool isColumnWritten)
        {
            int i = 0;
            int j = 0;
            int count = 0;
            ISheet sheet = null;

            fs = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            if (fileName.IndexOf(".xlsx") > 0) // 2007版本
                workbook = new XSSFWorkbook();
            else if (fileName.IndexOf(".xls") > 0) // 2003版本
                workbook = new HSSFWorkbook();

            try
            {
                if (workbook != null)
                {
                    sheet = workbook.CreateSheet(sheetName);
                }
                else
                {
                    return -1;
                }

                if (isColumnWritten == true) //写入DataTable的列名
                {
                    IRow row = sheet.CreateRow(0);
                    for (j = 0; j < data.Columns.Count; ++j)
                    {
                        row.CreateCell(j).SetCellValue(data.Columns[j].ColumnName);
                    }
                    count = 1;
                }
                else
                {
                    count = 0;
                }

                for (i = 0; i < data.Rows.Count; ++i)
                {
                    IRow row = sheet.CreateRow(count);
                    for (j = 0; j < data.Columns.Count; ++j)
                    {
                        row.CreateCell(j).SetCellValue(data.Rows[i][j].ToString());
                    }
                    ++count;
                }
                workbook.Write(fs); //写入到excel
                return count;
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                return -1;
            }
        }


        /// <summary>
        /// DataTable导出到excel
        /// </summary>
        /// <param name="SourceTable">DataTable数据源</param>
        /// <param name="strFilename">导出文件名</param>
        public static void ExportToExcel(DataTable SourceTable, string strFilename)
        {
        }

        #endregion

        #region ExcelToDataTable
        /// <summary>
        /// 将excel中的数据导入到DataTable中
        /// </summary>
        /// <param name="sheetName">excel工作薄sheet的名称</param>
        /// <param name="isFirstRowColumn">第一行是否是DataTable的列名</param>
        /// <returns>返回的DataTable</returns>
        public DataTable ExcelToDataTable(string sheetName, bool isFirstRowColumn)
        {
            ISheet sheet = null;
            DataTable data = new DataTable();
            int startRow = 0;
            try
            {
                fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                if (fileName.IndexOf(".xlsx") > 0) // 2007版本
                    workbook = new XSSFWorkbook(fs);
                else if (fileName.IndexOf(".xls") > 0) // 2003版本
                    workbook = new HSSFWorkbook(fs);

                if (sheetName != null)
                {
                    sheet = workbook.GetSheet(sheetName);
                    if (sheet == null) //如果没有找到指定的sheetName对应的sheet，则尝试获取第一个sheet
                    {
                        sheet = workbook.GetSheetAt(0);
                    }
                }
                else
                {
                    sheet = workbook.GetSheetAt(0);
                }
                if (sheet != null)
                {
                    IRow firstRow = sheet.GetRow(0);
                    int cellCount = firstRow.LastCellNum; //一行最后一个cell的编号 即总的列数

                    if (isFirstRowColumn)
                    {
                        for (int i = firstRow.FirstCellNum; i < cellCount; ++i)
                        {
                            ICell cell = firstRow.GetCell(i);
                            if (cell != null)
                            {
                                string cellValue = cell.StringCellValue;
                                DataColumn column = new DataColumn(cellValue);
                                data.Columns.Add(column);
                            }
                        }
                        startRow = sheet.FirstRowNum + 1;
                    }
                    else
                    {
                        startRow = sheet.FirstRowNum;
                    }

                    //最后一列的标号
                    int rowCount = sheet.LastRowNum;
                    for (int i = startRow; i <= rowCount; ++i)
                    {
                        IRow row = sheet.GetRow(i);
                        if (row == null) continue; //没有数据的行默认是null　　　　　　　

                        DataRow dataRow = data.NewRow();
                        for (int j = row.FirstCellNum; j < cellCount; ++j)
                        {
                            if (row.GetCell(j) != null) //同理，没有数据的单元格都默认是null
                                dataRow[j] = row.GetCell(j).ToString();
                        }
                        data.Rows.Add(dataRow);
                    }
                }
                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 将excel转化为datatable,将不为空的列名的数据去掉,第一行为列名,2015-06-17
        /// </summary>
        /// <param name="sheetName"></param>
        /// <param name="isFirstRowColumn"></param>
        /// <returns></returns>
        public DataTable ExcelToDataTableWithNoneColumnName(string sheetName, bool isFirstRowColumn)
        {
            ISheet sheet = null;
            DataTable data = new DataTable();
            int startRow = 0;
            try
            {
                fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                if (fileName.IndexOf(".xlsx") > 0) // 2007版本
                    workbook = new XSSFWorkbook(fs);
                else if (fileName.IndexOf(".xls") > 0) // 2003版本
                    workbook = new HSSFWorkbook(fs);

                if (sheetName != null)
                {
                    sheet = workbook.GetSheet(sheetName);
                    if (sheet == null) //如果没有找到指定的sheetName对应的sheet，则尝试获取第一个sheet
                    {
                        sheet = workbook.GetSheetAt(0);
                    }
                }
                else
                {
                    sheet = workbook.GetSheetAt(0);
                }
                if (sheet != null)
                {
                    IRow firstRow = sheet.GetRow(0);
                    int cellCount = firstRow.LastCellNum; //一行最后一个cell的编号 即总的列数

                    if (isFirstRowColumn)
                    {
                        DataColumn column = new DataColumn();
                        for (int i = firstRow.FirstCellNum; i < cellCount; ++i)
                        {
                            ICell cell = firstRow.GetCell(i);
                            if (cell != null)
                            {
                                string cellValue = cell.StringCellValue.Trim();
                                column = new DataColumn(cellValue);
                                data.Columns.Add(column);
                            }
                        }
                        startRow = sheet.FirstRowNum + 1;
                    }
                    else
                    {
                        startRow = sheet.FirstRowNum;
                    }

                    //最后一列的标号
                    int rowCount = sheet.LastRowNum;
                    for (int i = startRow; i <= rowCount; ++i)
                    {
                        IRow row = sheet.GetRow(i);
                        if (row == null) continue; //没有数据的行默认是null　　　　　　　

                        DataRow dataRow = data.NewRow();
                        for (int j = row.FirstCellNum; j < cellCount; ++j)
                        {
                            if (row.GetCell(j) != null) //同理，没有数据的单元格都默认是null
                                dataRow[j] = row.GetCell(j).ToString();
                        }
                        //NCCode为空的数据不做处理
                        //如果该列数据还有NCCode这一列  DataRow.Table.Columns.Contains("列名"); 
                        if (dataRow.Table.Columns.Contains("NCCode"))
                        {
                            var nccode = dataRow["NCCode"].ToString();
                            if (!string.IsNullOrEmpty(nccode))
                            {
                                data.Rows.Add(dataRow);
                            }
                        }
                    }
                    for (int i = data.Columns.Count - 1; i >= 0; i--)
                    {
                        if (firstRow.Cells[i].ToString().Trim() == string.Empty)
                        {
                            data.Columns.RemoveAt(i);
                        }
                    }
                }

                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable CostControlExcelToDataTable(string sheetName, bool isFirstRowColumn)
        {
            ISheet sheet = null;
            DataTable data = new DataTable();
            int startRow = 0;
            try
            {
                fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                if (fileName.IndexOf(".xlsx") > 0) // 2007版本
                    workbook = new XSSFWorkbook(fs);
                else if (fileName.IndexOf(".xls") > 0) // 2003版本
                    workbook = new HSSFWorkbook(fs);

                if (sheetName != null)
                {
                    sheet = workbook.GetSheet(sheetName);
                    if (sheet == null) //如果没有找到指定的sheetName对应的sheet，则尝试获取第一个sheet
                    {
                        sheet = workbook.GetSheetAt(0);
                    }
                }
                else
                {
                    sheet = workbook.GetSheetAt(0);
                }
                if (sheet != null)
                {
                    IRow firstRow = sheet.GetRow(0);
                    int cellCount = firstRow.LastCellNum; //一行最后一个cell的编号 即总的列数

                    if (isFirstRowColumn)
                    {
                        for (int i = firstRow.FirstCellNum; i < cellCount; ++i)
                        {
                            ICell cell = firstRow.GetCell(i);
                            if (cell != null)
                            {
                                string cellValue = cell.StringCellValue;
                                if (cellValue != null)
                                {
                                    DataColumn column = new DataColumn(cellValue);
                                    data.Columns.Add(column);
                                }
                            }
                        }
                        startRow = sheet.FirstRowNum + 1;
                    }
                    else
                    {
                        startRow = sheet.FirstRowNum;
                    }

                    //最后一列的标号
                    int rowCount = sheet.LastRowNum;
                    for (int i = startRow; i <= rowCount; ++i)
                    {
                        IRow row = sheet.GetRow(i);
                        if (row == null) continue; //没有数据的行默认是null　　　　　　　

                        DataRow dataRow = data.NewRow();
                        for (int j = row.FirstCellNum; j < cellCount; ++j)
                        {
                            if (row.GetCell(j) != null) //同理，没有数据的单元格都默认是null
                                dataRow[j] = row.GetCell(j).ToString();
                        }
                        data.Rows.Add(dataRow);
                    }
                }
                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region ExcelToDataTableValidateTwo
        /// <summary>
        /// 将excel中的数据导入到DataTable中,重复出现的列,返回重复列的列名和列个数
        /// </summary>
        /// <param name="sheetName"></param>
        /// <param name="isFirstRowColumn"></param>
        /// <returns></returns>
        public Dictionary<string, int> ExcelToDataTableValidateTwo(string sheetName, bool isFirstRowColumn)
        {
            var dicmanyColumns = new Dictionary<string, int>();
            ISheet sheet = null;
            DataTable data = new DataTable();
            try
            {
                fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                if (fileName.IndexOf(".xlsx") > 0) // 2007版本
                    workbook = new XSSFWorkbook(fs);
                else if (fileName.IndexOf(".xls") > 0) // 2003版本
                    workbook = new HSSFWorkbook(fs);

                if (sheetName != null)
                {
                    sheet = workbook.GetSheet(sheetName);
                    if (sheet == null) //如果没有找到指定的sheetName对应的sheet，则尝试获取第一个sheet
                    {
                        sheet = workbook.GetSheetAt(0);
                    }
                }
                else
                {
                    sheet = workbook.GetSheetAt(0);
                }
                if (sheet != null)
                {
                    IRow firstRow = sheet.GetRow(0);
                    int cellCount = firstRow.LastCellNum; //一行最后一个cell的编号 即总的列数

                    if (isFirstRowColumn)
                    {
                        for (int i = firstRow.FirstCellNum; i < cellCount; ++i)
                        {
                            ICell cell = firstRow.GetCell(i);
                            if (cell != null)
                            {
                                string cellValue = cell.StringCellValue.Trim();
                                if (!string.IsNullOrEmpty(cellValue))
                                {
                                    DataColumn column = new DataColumn(cellValue);
                                    if (data.Columns.Contains(cellValue))
                                    {
                                        var tem = dicmanyColumns[cellValue] + 1;
                                        dicmanyColumns.Remove(cellValue);
                                        dicmanyColumns.Add(cellValue, tem);
                                    }
                                    else
                                    {
                                        dicmanyColumns.Add(cellValue, 1);
                                        data.Columns.Add(column);
                                    }
                                }
                            }
                        }
                    }
                }
                return dicmanyColumns;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    if (fs != null)
                        fs.Close();
                }

                fs = null;
                disposed = true;
            }
        }

        /// <summary>
        /// 校验sheetname是否有要求的个数对应名称的sheet
        /// </summary>
        /// <param name="file"></param>
        /// <param name="sheetNames"></param>
        /// <returns></returns>
        public List<string> ValidateSheetName4ImportData(string file, string[] sheetNames)
        {
            var notexists = new List<string>();
            ISheet sheet = null;
            try
            {
                fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                if (fileName.IndexOf(".xlsx") > 0) // 2007版本
                    workbook = new XSSFWorkbook(fs);
                else if (fileName.IndexOf(".xls") > 0) // 2003版本
                    workbook = new HSSFWorkbook(fs);
                if (sheetNames.Any())
                {
                    foreach (var sheetName in sheetNames)
                    {
                        sheet = workbook.GetSheet(sheetName);
                        if (sheet == null) //如果没有找到指定的sheetName对应的sheet，则尝试获取第一个sheet
                        {
                            if (!notexists.Contains(sheetName))
                            {
                                notexists.Add(sheetName);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return notexists;
        }


        #region DataTableToExcel
        /// <summary>
        /// 将DataTable数据导入到excel中
        /// </summary>
        /// <param name="data">要导入的数据</param>
        /// <param name="isColumnWritten">DataTable的列名是否要导入</param>
        /// <param name="sheetName">要导入的excel的sheet的名称</param>
        /// <returns>导入数据行数(包含列名那一行)</returns>
        public int MonthlyStatementDataTableToExcel(string strFilename, Dictionary<int, SelfNPOICell[]> data1, Dictionary<int, SelfNPOICell[]> data2)
        {
            ISheet sheet1 = null;
            ISheet sheet2 = null;
            string sheetName1 = "Sheet1";
            string sheetName2 = "Sheet2";

            try
            {
                if (workbook == null)
                {
                    workbook = new HSSFWorkbook();
                    sheet1 = workbook.CreateSheet(sheetName1);
                    sheet2 = workbook.CreateSheet(sheetName2);
                }

                MSWriteToSheet1(data1, sheet1);
                MSWriteToSheet2(data2, sheet2);

                var context = HttpContext.Current;
                context.Response.ContentType = "application/vnd.ms-excel";
                context.Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", strFilename));
                //context.Response.Clear();

                MemoryStream file = new MemoryStream();
                workbook.Write(file); //写入到excel
                context.Response.BinaryWrite(file.GetBuffer());
                context.Response.End();

                return 0;
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                return -1;
            }
        }

        private int MSWriteToSheet1(Dictionary<int, SelfNPOICell[]> data, ISheet sheet)
        {
            if (data == null || data.Count == 0)
                return -1;

            List<int> setRow = new List<int>();
            setRow.Add(3);
            setRow.Add(4);
            setRow.Add(5);
            setRow.Add(6);
            setRow.Add(7);

            ICellStyle fontBold = this.setFontBold();
            ICellStyle cellBorder = this.setCellBorder();

            foreach (var r in data)
            {
                IRow row = sheet.CreateRow(r.Key);

                for (int col = 0; col < r.Value.Count(); col++)
                {
                    ICell cell = row.CreateCell(col);
                    cell.SetCellValue(r.Value[col].CellValue);
                    ////给加盟费等标签字体加粗
                    if (r.Value[col].IsFontBold)
                    {
                        cell.CellStyle = fontBold;
                    }

                    if (r.Value[col].HasCellBorder)
                    {
                        cell.CellStyle = cellBorder;
                    }
                }
                ////合并单元格并设置边框
                if (setRow.Contains(r.Key))
                {
                    CellRangeAddress region1 = new CellRangeAddress(r.Key, r.Key, 1, 2);
                    CellRangeAddress region2 = new CellRangeAddress(r.Key, r.Key, 5, 6);
                    sheet.AddMergedRegion(region1);
                    sheet.AddMergedRegion(region2);
                    ((HSSFSheet)sheet).SetEnclosedBorderOfRegion(region1, BorderStyle.Thin, NPOI.HSSF.Util.HSSFColor.Grey80Percent.Index);
                    ((HSSFSheet)sheet).SetEnclosedBorderOfRegion(region2, BorderStyle.Thin, NPOI.HSSF.Util.HSSFColor.Grey80Percent.Index);
                }
            }
            //设置合并单元格的长度，需全部显示文字
            sheet.SetColumnWidth(2, 30 * 256);
            sheet.SetColumnWidth(6, 30 * 256);

            return 0;
        }

        private int MSWriteToSheet2(Dictionary<int, SelfNPOICell[]> data, ISheet sheet)
        {
            if (data == null || data.Count == 0)
                return -1;

            ICellStyle backcolor = this.setCellBackColor();
            ICellStyle fontBold = this.setFontBold();
            ICellStyle wrapText = this.setWrapText();

            foreach (var r in data)
            {
                IRow row = sheet.CreateRow(r.Key);
                bool isSetBackColor = false;
                if (r.Key > 0 && r.Value[0].HasBackColor)
                {
                    isSetBackColor = true;
                }
                for (int col = 0; col < r.Value.Count(); col++)
                {
                    ICell cell = row.CreateCell(col);
                    cell.SetCellValue(r.Value[col].CellValue);
                    if (r.Value[col].IsFontBold)
                    {
                        cell.CellStyle = fontBold;
                   
                    }
                    if (isSetBackColor)
                    {
                        cell.CellStyle = backcolor;
                    }
                    else if (col == 4)
                    {
                        cell.CellStyle = setWrapText();
                    }
                }
            }
            //设置合并单元格的长度，需全部显示文字
            sheet.SetColumnWidth(0, 24 * 256);
            sheet.SetColumnWidth(1, 20 * 256);
            sheet.SetColumnWidth(4, 60 * 256);
            sheet.SetColumnWidth(8, 15 * 256);
            sheet.CreateFreezePane(0, 1, 0, 1);

            return 0;
        }

        /// <summary>
        /// 设置字体加粗
        /// </summary>
        /// <returns></returns>
        private ICellStyle setFontBold()
        {
            IFont headerFont = workbook.CreateFont();
            headerFont.FontHeightInPoints = 10;
            headerFont.FontName = "微软雅黑";
            headerFont.Boldweight = (short)FontBoldWeight.Bold;
            ICellStyle cellStyle = workbook.CreateCellStyle();
            cellStyle.SetFont(headerFont);
            return cellStyle;
        }
        /// <summary>
        /// 设置单元格背景色
        /// </summary>
        /// <returns></returns>
        private ICellStyle setCellBackColor()
        {
            ICellStyle backColorStyle = workbook.CreateCellStyle();

            IFont boldFont = workbook.CreateFont();
            boldFont.FontHeightInPoints = 10;
            boldFont.FontName = "微软雅黑";
            boldFont.Boldweight = (short)FontBoldWeight.Bold;

            HSSFPalette palette = ((HSSFWorkbook)workbook).GetCustomPalette();

            Color backColor = Color.FromArgb(238, 236, 225);

            short FIRST_COLOR_INDEX = (short)0x8;
            //index的取值范围 0x8 - 0x40
            palette.SetColorAtIndex((short)(FIRST_COLOR_INDEX), backColor.R, backColor.G, backColor.B);

            backColorStyle.FillPattern = FillPattern.SolidForeground;
            var v1 = palette.FindColor(backColor.R, backColor.G, backColor.B);
            if (v1 == null)
            {
                throw new Exception("Color is not in Palette");
            }
            backColorStyle.FillForegroundColor = v1.GetIndex();

            backColorStyle.SetFont(boldFont);
            return backColorStyle;
        }

        /// <summary>
        /// 设置边框
        /// </summary>
        /// <returns></returns>
        private ICellStyle setCellBorder()
        {
            ICellStyle cellStyle = workbook.CreateCellStyle();
            cellStyle.BorderBottom = BorderStyle.Thin;
            cellStyle.BorderLeft = BorderStyle.Thin;
            cellStyle.BorderRight = BorderStyle.Thin;
            cellStyle.BorderTop = BorderStyle.Thin;
            cellStyle.LeftBorderColor = HSSFColor.Grey80Percent.Index;
            cellStyle.RightBorderColor = HSSFColor.Grey80Percent.Index;
            cellStyle.TopBorderColor = HSSFColor.Grey80Percent.Index;
            cellStyle.BottomBorderColor = HSSFColor.Grey80Percent.Index;
            return cellStyle;
        }
        /// <summary>
        /// 设置自动换行
        /// </summary>
        /// <returns></returns>
        private ICellStyle setWrapText()
        {
            ICellStyle cellStyle = workbook.CreateCellStyle();
            cellStyle.WrapText = true;
            return cellStyle;
        }
        #endregion


        //第一个sheet中列明NCCode和财务专员字段列名必须存在,此处进行校验--财务上传定制,2015-06-24
        public List<string> ValidateFirstSheetContainsTwoColumnOfNcAndEmp(string sheetName, bool isFirstRowColumn)
        {
            ISheet sheet = null;
            DataTable data = new DataTable();
            int startRow = 0;
            try
            {
                fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                if (fileName.IndexOf(".xlsx") > 0) // 2007版本
                    workbook = new XSSFWorkbook(fs);
                else if (fileName.IndexOf(".xls") > 0) // 2003版本
                    workbook = new HSSFWorkbook(fs);

                if (sheetName != null)
                {
                    sheet = workbook.GetSheet(sheetName);
                    if (sheet == null) //如果没有找到指定的sheetName对应的sheet，则尝试获取第一个sheet
                    {
                        sheet = workbook.GetSheetAt(0);
                    }
                }
                else
                {
                    sheet = workbook.GetSheetAt(0);
                }
                if (sheet != null)
                {
                    IRow firstRow = sheet.GetRow(0);
                    int cellCount = firstRow.LastCellNum; //一行最后一个cell的编号 即总的列数
                    if (isFirstRowColumn)
                    {
                        for (int i = firstRow.FirstCellNum; i < cellCount; ++i)
                        {
                            ICell cell = firstRow.GetCell(i);
                            if (cell != null)
                            {
                                string cellValue = cell.StringCellValue;
                                DataColumn column = new DataColumn(cellValue);
                                data.Columns.Add(column);
                            }
                        }
                    }
                }
                var resList = new List<string>();
                if (!data.Columns.Contains("NCCode"))
                {
                    resList.Add("NCCode");
                }
                if (!data.Columns.Contains("财务专员"))
                {
                    resList.Add("财务专员");
                }
                return resList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
