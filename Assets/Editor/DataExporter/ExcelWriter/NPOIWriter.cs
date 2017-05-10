using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using NPOI.HSSF.UserModel;

public class NPOIWriter : IExcelWriter
{
    string _fullPath;
    string _path;
    string _ext;
    public NPOIWriter()
    {
    }

    void InitPath(string fullPath)
    {
        _fullPath = fullPath;
        _path = Path.GetDirectoryName(fullPath);
        _ext = Path.GetExtension(fullPath);
        if (!Directory.Exists(_path))
            Directory.CreateDirectory(_path);
    }

    public void Write(string fullPath, ExcelData data)
    {
        InitPath(fullPath);
     
        IWorkbook book = GetWorkBook();
        ISheet sheet = book.GetSheetAt(0);
        WriteDataToSheet(sheet, data);
        SaveToFile(book);
    }

    void SaveToFile(IWorkbook workBook)
    {
        using (FileStream fs = File.Open(_fullPath, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite))
        {
            workBook.Write(fs);
            workBook.Close();
            fs.Close();
        }
    }

    void WriteDataToSheet(ISheet sheet, ExcelData data)
    {
        for(int i = 0; i < data.excelRows.Count; i ++)
        {
            var excelRowData = data.excelRows[i];
            if (!excelRowData.isDirty)
                continue;
            excelRowData.isDirty = false;
            var row = sheet.GetRow(excelRowData.row);
            if (row == null)
                row = sheet.CreateRow(excelRowData.row);
            WriteDataToRow(row, excelRowData);
        }
    }

    void WriteDataToRow(IRow row, ExcelRow rowData)
    {
        for(int i = 0; i < rowData.count; i++)
        {
            var cellData = rowData.cellList[i];
            var cell = row.GetCell(cellData.index);
            if (cell == null)
                cell = row.CreateCell(cellData.index);
            WriteDataToCell(cell, cellData);
        }
    }

    //暂时都以string的形式写入
    //@todo  根据类型写入
    void WriteDataToCell(ICell cell, ExcelCell cellData)
    {
        cell.SetCellValue(cellData.stringValue);
    }

    IWorkbook GetWorkBook()
    {
        IWorkbook workBook;
        if (!File.Exists(_fullPath))
        {
            workBook = CreateWorkBook();
            workBook.CreateSheet();
            return workBook;
        }

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

    IWorkbook CreateWorkBook()
    {
        switch (_ext)
        {
            case ExcelExporterUtil.XLSEXT:
                return new HSSFWorkbook();
                //暂时不支持xlsx
            case ExcelExporterUtil.XLSXEXT:
                return new XSSFWorkbook();
        }
        return null;
    }


}
