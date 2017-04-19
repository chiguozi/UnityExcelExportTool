using System.Collections.Generic;
using System.IO;
using UnityEngine;
using NPOI.SS.UserModel;

public class NPOILoader : IExcelLoader
{
    string _fullPath;

    public NPOILoader(string fullPath)
    {
        _fullPath = fullPath;
    }

    public ExcelData Load()
    {
        IWorkbook workBook;
        ISheet sheet;
        using (FileStream fs = File.Open(_fullPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
        {
            if (fs == null)
            {
                Debug.LogError("找不到文件 " + _fullPath);
                return null;
            }
            workBook = WorkbookFactory.Create(fs);
            sheet = workBook.GetSheetAt(0);
        }
        ExcelData excel = new ExcelData();
        string fileName = Path.GetFileNameWithoutExtension(_fullPath);
        excel.fileName = fileName;
        excel.filePath = _fullPath;
        excel.excelRows = new List<ExcelRow>();
        for (int i = sheet.FirstRowNum; i <= sheet.LastRowNum; i++)
        {
            excel.excelRows.Add(GetExcelRow(sheet.GetRow(i), i));
        }
        workBook.Close();
        return excel;
    }

    //需要记录行数
    ExcelRow GetExcelRow(IRow row, int rowNum)
    {
        ExcelRow excelRow = new ExcelRow();
        excelRow.row = rowNum;
        if (row == null)
            return excelRow;

        excelRow.row = row.RowNum;
        excelRow.cellList = new List<ExcelCell>();
        for (int i = row.FirstCellNum; i < row.LastCellNum; i++)
        {
            excelRow.AddCell(GetExcelCell(row.GetCell(i), i));
        }
        return excelRow;
    }

    ExcelCell GetExcelCell(ICell cell, int index)
    {
        ExcelCell excelCell = new ExcelCell();
        excelCell.index = index;
        if (cell == null)
            return excelCell;
        CellType type;
        var obj = GetValueType(cell, out type);
        excelCell.value = obj;
        excelCell.stringValue = obj == null ? "" : obj.ToString();
        if (cell.CellStyle.FillForegroundColorColor != null)
            excelCell.rgb = cell.CellStyle.FillForegroundColorColor.RGB;
        else
            excelCell.rgb = new byte[] { 255, 255, 255 };
        excelCell.index = cell.ColumnIndex;
        return excelCell;
    }

    object GetValueType(ICell cell, out CellType type)
    {
        type = CellType.Error;
        if (cell == null)
            return null;
        type = cell.CellType;
        switch (type)
        {
            case CellType.Boolean:
                if (cell.BooleanCellValue == true)
                    return "true";
                else
                    return "false";
            case CellType.Numeric:
                return cell.NumericCellValue;
            case CellType.String:
                string str = cell.StringCellValue;
                if (string.IsNullOrEmpty(str))
                    return null;
                return str.ToString();
            case CellType.Error:
                return cell.ErrorCellValue;
            case CellType.Formula:
                {
                    type = cell.CachedFormulaResultType;
                    switch (type)
                    {
                        case CellType.Boolean:
                            if (cell.BooleanCellValue == true)
                                return "true";
                            else
                                return "false";
                        case CellType.Numeric:
                            return cell.NumericCellValue;
                        case CellType.String:
                            string str1 = cell.StringCellValue;
                            if (string.IsNullOrEmpty(str1))
                                return null;
                            return str1.ToString();
                        case CellType.Error:
                            return cell.ErrorCellValue;
                        case CellType.Unknown:
                        case CellType.Blank:
                        default:
                            return null;
                    }
                }
            case CellType.Unknown:
            case CellType.Blank:
            default:
                return null;
        }
    }

}
