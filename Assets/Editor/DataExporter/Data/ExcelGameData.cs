using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Reflection;

public class ExcelGameData
{
    public List<string> fieldNameList = new List<string>();
    public List<string> fieldTypeList = new List<string>();
    //保留excelcell  导出二进制 或者 scirptobject会用到
    public List<List<ExcelCell>> cellList = new List<List<ExcelCell>>();

    public object GetObject(int index, Type type)
    {
        var valueList = cellList[index];
        var instance = Activator.CreateInstance(type);
        for(int i = 0; i < valueList.Count; i++)
        {
            var field = type.GetField(fieldNameList[i], BindingFlags.Public | BindingFlags.Instance | BindingFlags.GetField);
            field.SetValue(instance, Convert.ChangeType(valueList[i].value, SupportTypeUtil.TryGetType(fieldTypeList[i])));
        }
        return instance;
    }

    public bool AddFieldName(string field)
    {
        if (fieldNameList.Contains(field))
            return false;
        if (fieldNameList.Count == 0)
            field = "ID";
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
