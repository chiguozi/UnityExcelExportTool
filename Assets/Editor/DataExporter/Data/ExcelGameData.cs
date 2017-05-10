using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExcelGameData
{
    public List<string> fieldNameList = new List<string>();
    public List<string> fieldTypeList = new List<string>();
    //保留excelcell  导出二进制 或者 scirptobject会用到
    public List<List<ExcelCell>> cellList = new List<List<ExcelCell>>();

    public bool AddFieldName(string field)
    {
        if (fieldNameList.Contains(field))
            return false;
        fieldNameList.Add(field);
        return true;
    }

    public void AddFieldType(string type)
    {
        fieldTypeList.Add(type);
    }

    public void AddCell(int row, ExcelCell cell)
    {
        if (row >= cellList.Count)
            cellList.Add(new List<ExcelCell>());
        cellList[row].Add(cell);
    }
}
