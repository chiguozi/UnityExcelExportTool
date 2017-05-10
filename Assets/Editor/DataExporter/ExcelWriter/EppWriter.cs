using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using OfficeOpenXml;

public class EppWriter : IExcelWriter
{
    string _path;
    public void Write(string fullPath, ExcelData data)
    {
        InitPath(fullPath);

        var fileInfo = new FileInfo(fullPath);
        ExcelPackage package = new ExcelPackage(fileInfo);
        var workBook = package.Workbook;
        var sheet = workBook.Worksheets[1];
        WriteDataToSheet(sheet, data);
        SaveToFile(package);
    }

    void WriteDataToSheet(ExcelWorksheet sheet, ExcelData data)
    {
        for (int i = 0; i < data.excelRows.Count; i++)
        {
            var excelRowData = data.excelRows[i];
            if (!excelRowData.isDirty)
                continue;
            excelRowData.isDirty = false;
            for(int j = 0; j < excelRowData.count; j++)
            {
                sheet.Cells[i + 1, j + 1].Value = excelRowData.GetCell(j).stringValue;
            }
        }
    }

    void SaveToFile(ExcelPackage package)
    {
        package.Save();
    }
    void InitPath(string fullPath)
    {
        _path = Path.GetDirectoryName(fullPath);
        if (!Directory.Exists(_path))
            Directory.CreateDirectory(_path);
    }
}
