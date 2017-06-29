using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System.Text.RegularExpressions;
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
        IWorkbook workBook = LoadWorkBookInternal();
        ISheet sheet;
        sheet = workBook.GetSheetAt(0);
        string fileName = Path.GetFileNameWithoutExtension(_fullPath);
        var excel = LoadExcelDataFromSheet(sheet, fileName);
        workBook.Close();
        return excel;
    }

    public List<ExcelData> LoadBySheets()
    {
        List<ExcelData> excelList = new List<ExcelData>();
        IWorkbook workBook = LoadWorkBookInternal();
        int sheetCount = workBook.NumberOfSheets;
        if (sheetCount == 0)
            return excelList;

        ISheet sheet;
        for (int i = 0; i < sheetCount; i++)
        {
            sheet = workBook.GetSheetAt(i);
            string fileName;
            if(sheet != null && TryGetFileNameFromSheetName(sheet.SheetName, out fileName))
                excelList.Add(LoadExcelDataFromSheet(workBook.GetSheetAt(i), ""));
        }
        //兼容之前
        if(excelList.Count == 0)
        {
            string fileName = Path.GetFileNameWithoutExtension(_fullPath);
            LoadExcelDataFromSheet(workBook.GetSheetAt(0), fileName);
        }
        workBook.Close();
        return excelList;
    }

    //字母数字 下划线  开头只能为字母
    //fileName.OP.XXXX
    bool TryGetFileNameFromSheetName(string sheetName, out string fileName)
    {
        fileName = "";
        var subs = sheetName.Split('.');
        if (subs.Length <= 1)
            return false;
        if (!subs[1].Equals("OP"))
            return false;
        if (!Regex.IsMatch(subs[0], @"^[a-zA-Z]\w*$"))
            return false;
        fileName = subs[0];
        return true;
    }

    ExcelData LoadExcelDataFromSheet(ISheet sheet, string fileName)
    {
        ExcelData excel = new ExcelData();
        //string fileName = Path.GetFileNameWithoutExtension(_fullPath);
        excel.fileName = fileName;
        excel.excelRows = new List<ExcelRow>();
        for (int i = sheet.FirstRowNum; i <= sheet.LastRowNum; i++)
        {
            excel.excelRows.Add(GetExcelRow(sheet.GetRow(i), i));
        }
        return excel;
    }

    IWorkbook LoadWorkBookInternal()
    {
        IWorkbook workBook;
        using (FileStream fs = File.Open(_fullPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
        {
            if (fs == null)
            {
                Debug.LogError("找不到文件 " + _fullPath);
                return null;
            }
            workBook = WorkbookFactory.Create(fs);
        }
        return workBook;
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
