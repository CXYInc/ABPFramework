using Microsoft.AspNetCore.Http;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System.Data;
using System.IO;

namespace CXY.CJS.Core.NPOI
{
    public static class ImportExeclHelper
    {
        public static DataSet ReadExcel(IFormFile file)
        {
            var filename = Path.GetFileName(file.FileName);

            return ReadExcel(file.OpenReadStream(), filename);
        }

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

        public static DataSet ReadExcel(Stream stream, string fileName)
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

    }
}
