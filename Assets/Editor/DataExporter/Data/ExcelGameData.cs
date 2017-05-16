using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Reflection;
using Object = UnityEngine.Object;

public class ExcelGameData
{
    public List<string> fieldNameList = new List<string>();
    public List<string> fieldTypeList = new List<string>();
    //保留excelcell  导出二进制 或者 scirptobject会用到
    public List<List<ExcelContentCell>> cellList = new List<List<ExcelContentCell>>();

    public Object GetSOObject(int index, Type type)
    {
        var valueList = cellList[index];
        var instance = ScriptableObject.CreateInstance(type);//Activator.CreateInstance(type);
        //var instance = Activator.CreateInstance(type);
        for (int i = 0; i < valueList.Count; i++)
        {
            var field = type.GetField(fieldNameList[i], BindingFlags.Public | BindingFlags.Instance | BindingFlags.GetField);

            field.SetValue(instance, valueList[i].value == null ? null : Convert.ChangeType(valueList[i].value, valueList[i].fieldType));
        }
        return instance;
    }

    public object GetObject(int index, Type type)
    {
        var valueList = cellList[index];
        var instance = Activator.CreateInstance(type);
        for (int i = 0; i < valueList.Count; i++)
        {
            var field = type.GetField(fieldNameList[i], BindingFlags.Public | BindingFlags.Instance | BindingFlags.GetField);

            field.SetValue(instance, valueList[i].value == null ? null : Convert.ChangeType(valueList[i].value, valueList[i].fieldType));
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
            cellList.Add(new List<ExcelContentCell>());
        var contentCell = new ExcelContentCell(cell);
        contentCell.fieldName = fieldNameList[cellList[row].Count];
        contentCell.fieldTypeName = fieldTypeList[cellList[row].Count];
        cellList[row].Add(contentCell);
        //if (row >= cellList.Count)
        //    cellList.Add(new List<ExcelCell>());
        //cellList[row].Add(cell);
    }
}
