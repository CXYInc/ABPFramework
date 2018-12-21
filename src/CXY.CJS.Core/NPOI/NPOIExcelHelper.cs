using Microsoft.AspNetCore.Http;
using NPOI.HSSF.UserModel;
using NPOI.HSSF.Util;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;

namespace CXY.CJS.Core.NPOI
{
    public static class NPOIExcelHelper
    {
        /// <summary>
        /// 读取上传Excel文件
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public static DataSet ReadExcel(IFormFile file)
        {
            var filename = Path.GetFileName(file.FileName);

            using (var stream = file.OpenReadStream())
            {
                return ReadExcel(stream, filename);
            }
        }

        /// <summary>
        /// 根据文件路径读取excel
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static DataSet ReadExcel(string path)
        {
            DataSet ds = null;
            using (var fs = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                ds = new DataSet();
                var filename = Path.GetFileName(path);
                ds = ReadExcel(fs, filename);
            }
            return ds;
        }

        /// <summary>
        /// 读取excel流文件
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="fileName"></param>
        /// <param name="isHaveHeader"></param>
        /// <returns></returns>
        public static DataSet ReadExcel(Stream stream, string fileName, bool isHaveHeader = false)
        {
            IWorkbook workbook = null;
            var sheetNum = 0;
            var fileExt = Path.GetExtension(fileName).ToLower();
            if (fileExt == ".xls")
            {
                workbook = new HSSFWorkbook(stream);
                sheetNum = workbook.NumberOfSheets;
            }
            else
            {
                workbook = new XSSFWorkbook(stream);
                sheetNum = workbook.NumberOfSheets;
            }
            DataSet dataSet = new DataSet();
            for (var k = 0; k < sheetNum; k++)
            {
                var sheet = workbook.GetSheetAt(k);
                if (sheet == null) continue;

                DataTable table = new DataTable(sheet.SheetName);
                IRow headerRow = sheet.GetRow(0);
                int cellCount = 0;
                for (int i = headerRow.FirstCellNum; i < headerRow.LastCellNum; i++)
                {
                    if (headerRow.GetCell(i) == null)
                        break;
                    DataColumn column = new DataColumn(headerRow.GetCell(i).StringCellValue);
                    table.Columns.Add(column);
                    cellCount++;
                }
                int rowCount = sheet.LastRowNum;
                for (int i = (sheet.FirstRowNum) + 1; i <= rowCount; i++)
                {
                    IRow row = sheet.GetRow(i);
                    DataRow dataRow = table.NewRow();
                    for (int j = row.FirstCellNum; j < cellCount; j++)
                    {
                        var cell = row.GetCell(j);
                        if (cell != null)
                        {
                            dataRow[j] = GetCellValue(cell);
                        }
                        else
                        {
                            dataRow[j] = "";
                        }
                    }
                    table.Rows.Add(dataRow);
                }

                dataSet.Tables.Add(table);
            }
            return dataSet;
        }

        public static object GetCellValue(ICell cell)
        {
            object returnValue = "";
            switch (cell.CellType)
            {
                case CellType.Blank:
                    returnValue = "";
                    break;
                case CellType.Boolean:
                    returnValue = cell.BooleanCellValue;
                    break;
                case CellType.Numeric:
                    //对时间格式的处理
                    if (DateUtil.IsCellDateFormatted(cell))
                        returnValue = cell.DateCellValue.ToString("yyyy-MM-dd HH:mm:ss");
                    else
                        returnValue = cell.NumericCellValue;
                    break;
                case CellType.String:
                    returnValue = cell.StringCellValue;
                    break;
                case CellType.Error:
                    returnValue = cell.ErrorCellValue;
                    break;
                case CellType.Formula:
                    returnValue = "=" + cell.CellFormula;
                    break;
                default:
                    returnValue = cell.ToString();
                    break;
            }
            return returnValue;
        }

        /// <summary>
        /// 创建新的Sheet
        /// </summary>
        /// <param name="workBook"></param>
        /// <param name="sheetName"></param>
        /// <returns></returns>
        public static IWorkbook AddSheet(IWorkbook workBook, string sheetName)
        {
            if (workBook == null)
            {
                throw new ArgumentException("workBook must not be null");
            }

            if (sheetName == null)
            {
                throw new ArgumentException("sheetName must not be null");
            }

            if (workBook.GetSheet(sheetName) != null)
            {
                throw new ArgumentException("The workbook already contains a sheet of this name");
            }

            if (sheetName.Length > 0x1f)
            {
                sheetName = sheetName.Substring(0, 0x1f);
            }

            workBook.CreateSheet(sheetName);

            return workBook;
        }

        public static string ExportTest()
        {
            var fileName = Path.Combine($"用户订单统计_{DateTime.Now.ToString("yyyyMMddHHmmss")}.xls");
            var dirPath = Path.Combine("UploadFile", "UserFiles");

            if (!Directory.Exists(dirPath))
                Directory.CreateDirectory(dirPath);

            fileName = Path.Combine(dirPath, fileName);
            var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName);

            IWorkbook workbook = new HSSFWorkbook();

            workbook = AddSheet(workbook, "商户订单金额统计");
            var sheet = workbook.GetSheet("商户订单金额统计");
            //sheet.ProtectSheet("www.cx580.com");

            var row1ColumnNames = new List<string> { "合作商户名称", "商户负责人","支付渠道","有效批次量",
                "有效违章量（办理中+已完成的订单）", "", "", "", "", "", "", "", "", "", "", "", "",
                "已完成订单量", "", "", "", "", "", "" , "", "", "", "", "", "", ""};

            var row2ColumnNames = new List<string> { "", "","","",
                "违章数", "罚金", "实收手续费", "返点", "支付下游", "支付下游罚金", "支付下游手续费", "退款总额","退罚金","退手续费","佣金","优惠券","利润",
                "违章数", "罚金", "实收手续费", "返点", "支付下游", "支付下游罚金", "支付下游手续费", "退款总额","退罚金","退手续费","佣金","利润","折扣","税金"};

            var rowColumnNames = new List<string> { "Shortname","CompanyPersonName","PayChannel","Num",
                "TotalOrderCount", "TotalFines", "TotalServiceCharge", "TotalReturnPoint", "TotalPayProxy", "TotalProxyFines",
                "TotalProxyCharge", "TotalBackTotal", "TotalBackFines", "TotalBackCharge", "TotalCommission", "TotalCoupon", "TotalProfit",
                "OrderCount","Fines", "ServiceCharge", "ReturnPoint", "PayProxy", "ProxyFines", "ProxyCharge", "BackTotal","BackFines", "BackCharge", "Commission", "Profit","Discount", "Vat" };

            var rowCellCount = 0;

            //样式
            var cellStyle = workbook.CreateCellStyle();
            //字体
            var cellFont = workbook.CreateFont();
            cellFont.FontName = "宋体";
            cellFont.FontHeightInPoints = 10;
            //字体颜色

            cellFont.Color = HSSFColor.Black.Index;
            //字体加粗样式
            cellFont.Boldweight = (short)FontBoldWeight.Bold;

            //设置单元格背景色
            cellStyle.FillForegroundColor = HSSFColor.Gold.Index;
            cellStyle.FillPattern = FillPattern.SolidForeground;

            //样式里的字体设置具体的字体样式
            cellStyle.SetFont(cellFont);
            //文字水平对齐方式
            cellStyle.Alignment = HorizontalAlignment.Center;
            //文字垂直对齐方式
            cellStyle.VerticalAlignment = VerticalAlignment.Center;

            //边框
            cellStyle.BorderBottom = BorderStyle.Medium;
            cellStyle.BorderLeft = BorderStyle.Medium;
            cellStyle.BorderRight = BorderStyle.Medium;
            cellStyle.BorderTop = BorderStyle.Medium;

            //边框颜色
            cellStyle.BottomBorderColor = HSSFColor.Grey25Percent.Index;
            cellStyle.TopBorderColor = HSSFColor.Grey25Percent.Index;
            cellStyle.LeftBorderColor = HSSFColor.Grey25Percent.Index;
            cellStyle.RightBorderColor = HSSFColor.Grey25Percent.Index;

            var row = sheet.CreateRow(0);
            var row1 = sheet.CreateRow(1);
            row.HeightInPoints = 20;
            row1.HeightInPoints = 20;

            //合并单元格
            sheet.AddMergedRegion(new CellRangeAddress(0, 1, 0, 0));
            sheet.AddMergedRegion(new CellRangeAddress(0, 1, 1, 1));
            sheet.AddMergedRegion(new CellRangeAddress(0, 1, 2, 2));
            sheet.AddMergedRegion(new CellRangeAddress(0, 1, 3, 3));
            sheet.AddMergedRegion(new CellRangeAddress(0, 0, 4, 16));
            sheet.AddMergedRegion(new CellRangeAddress(0, 0, 17, 30));

            //设置单元格宽度
            sheet.SetColumnWidth(1, 12 * 256);
            sheet.SetColumnWidth(0, 12 * 256);
            sheet.SetColumnWidth(3, 12 * 256);
            sheet.SetColumnWidth(6, 12 * 256);
            sheet.SetColumnWidth(9, 12 * 256);
            sheet.SetColumnWidth(10, 14 * 256);
            sheet.SetColumnWidth(19, 12 * 256);
            sheet.SetColumnWidth(20, 12 * 256);
            sheet.SetColumnWidth(22, 14 * 256);
            sheet.SetColumnWidth(23, 14 * 256);
            sheet.SetColumnWidth(24, 12 * 256);

            foreach (var columnName in row1ColumnNames)
            {
                var cell = row.CreateCell(rowCellCount);
                cellStyle.IsLocked = true;
                cell.CellStyle = cellStyle;
                cell.SetCellValue(columnName);
                rowCellCount++;
            }

            rowCellCount = 0;
            foreach (var columnName in row2ColumnNames)
            {
                var cell = row1.CreateCell(rowCellCount);
                cellStyle.IsLocked = true;
                cell.CellStyle = cellStyle;
                cell.SetCellValue(columnName);
                rowCellCount++;
            }

            //冻结表头
            sheet.CreateFreezePane(0, 2, 0, 2);

            //自动生成单元格取消保护
            var defaultStyle = workbook.CreateCellStyle();
            defaultStyle.IsLocked = false;
            for (var i = 0; i < rowCellCount; i++)
                sheet.SetDefaultColumnStyle(i, defaultStyle);

            //将文件流写入到excel
            using (var fs = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.ReadWrite))
            {
                workbook.Write(fs);
            }

            return fileName;
        }
    }
}
