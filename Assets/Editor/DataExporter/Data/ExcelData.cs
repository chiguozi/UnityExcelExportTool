using System;
using System.Collections.Generic;
using UnityEngine;

//excel内的全部数据  不做过滤
public enum ExcelRule
{
    Error = 0,
    Common,  //客户端服务器通用
    Client,       //客户端使用
    Server,      //服务器使用
    Finish,      //结束标识(行)
    Ignore,     //忽略（行 列）
    Content  //内容
}

public class ExcelRuleUtil
{

    static Dictionary<int, ExcelRule> _colorRuleMap = new Dictionary<int, ExcelRule>()
    {
        { Rgb2Int(0,128,0), ExcelRule.Common},
        { Rgb2Int(255,204,0), ExcelRule.Server},
        { Rgb2Int(0,204,255), ExcelRule.Client},
        { Rgb2Int(128,128,128), ExcelRule.Ignore},
        { Rgb2Int(0,51,102), ExcelRule.Finish},
        { Rgb2Int(0,0,0), ExcelRule.Content},
    };

    static int Rgb2Int(byte r, byte g, byte b)
    {
        return r << 16 | g << 8 | b;
    }

    public static ExcelRule GetExcelRole(byte r, byte g, byte b)
    {
        int color = Rgb2Int(r, g, b);
        if (_colorRuleMap.ContainsKey(color))
            return _colorRuleMap[color];
        return ExcelRule.Error;
    }
}


//不想再对数据重新赋值，不使用继承的方式
public class ExcelContentCell
{
    public string fieldName;
    string _fieldTypeName;
    string _stringValue;
    IType _type;

    public string fieldTypeName
    {
        get { return _fieldTypeName; }
        set
        {
            var type = SupportTypeUtil.GetIType(value);
            if (type != null)
            {
                _fieldTypeName = type.realName;
                fieldType = type.type;
                _type = type;
            }
        }
    }
    public Type fieldType;
    public ExcelCell originCell;

    public object value
    {
        get
        {
            if (_type != null)
            {
                return _type.GetValue(_stringValue);
            }
            else
                return _stringValue;
        }
    }
    public string stringValue
    {
        get
        {
            return _stringValue;
        }
    }

    public ExcelContentCell(ExcelCell cell)
    {
        originCell = cell;
    }

    bool CheckCell()
    { return true; }

    public void FormatCell()
    {
        string res = originCell.stringValue;
        if(fieldType != typeof(string))
            res = ExcelExporterUtil.RemoveWhiteSpaceOutTheWordFull(res);
        res = ExcelExporterUtil.RemoveWordFirstQuotation(res);
        _stringValue = res;
        
    }

    public bool CheckValid()
    {
        return _type == null ? false : _type.CheckValue(_stringValue);
    }
}
public class ExcelCell
{
    public object value;
    public string stringValue;
    public int index;
    public ExcelRule rule;

    byte[] _rgb;
    public byte[] rgb
    {
        set
        {
            _rgb = value;
            rule = ExcelRuleUtil.GetExcelRole(_rgb[0], _rgb[1], _rgb[2]);
        }
        get { return _rgb; }
    }



    public bool IsEmpty
    {
        get
        {
            return value == null || string.IsNullOrEmpty(stringValue);
        }
    }
}

public class ExcelRow
{
    public int row;
    public List<ExcelCell> cellList = new List<ExcelCell>();
    public Dictionary<int, ExcelCell> cellMap = new Dictionary<int, ExcelCell>();
    public bool isDirty = false;
    public bool IsEmpty
    {
        get { return cellList == null || cellList.Count == 0; }
    }

    public int count { get { return cellList == null ? 0 : cellList.Count; } }

    public void AddCell(ExcelCell cell)
    {
        cellList.Add(cell);
        cellMap.Add(cell.index, cell);
    }

    public ExcelCell GetCell(int index)
    {
        if (cellMap.ContainsKey(index))
            return cellMap[index];
        return null;
    }

    //写数据时，不关心excelrule
    public void SetCellData(int column, object obj, Type type)
    {
        if (!cellMap.ContainsKey(column))
        {
            ExcelCell cell = new ExcelCell();
            cell.index = column;
            AddCell(cell);
        }
       
        cellMap[column].value = obj;
        cellMap[column].stringValue = obj.ToString();
        isDirty = true;
    }
}

public class ExcelData 
{
    public string fileName;
    public string filePath;
    public List<ExcelRow> excelRows;

    public int count { get { return excelRows == null? 0 : excelRows.Count; } }
    public ExcelRow GetRow(int index)
    {
        if (index >= excelRows.Count)
            return null;
        return excelRows[index];
    }

    public void SetCellValue(int row, int column, object value, Type type)
    {
        if (row > count)
        {
            Debug.LogError("不能跨行插入   行 = " + count);
            return;
        }
        if(row == count)
        {
            var excelRow = new ExcelRow();
            excelRow.row = row;
            excelRow.cellList = new List<ExcelCell>();
            excelRows.Add(excelRow);
        }
        var changeRow = excelRows[row];
        changeRow.SetCellData(column, value, type);
    }

    //用于拷贝数据时
    public void SetAllDirty()
    {
        for(int i = 0; i < excelRows.Count; i++)
        {
            excelRows[i].isDirty = true;
        }
    }
}
