using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Excel
{
    string _fileName;
    string _fullPath;
    string _extension;
    ExcelData _excelData;

    public HashSet<int> clientFieldIndexList = new HashSet<int>();
    public HashSet<int> serverFieldIndexList = new HashSet<int>();

    public ExcelGameData serverData;
    public ExcelGameData clientData;

    public Excel(string fullPath)
    {
        _fileName = Path.GetFileNameWithoutExtension(fullPath);
        _fullPath = fullPath;
        _extension = Path.GetExtension(fullPath);
    }

    public void Load()
    {
        var loader = GetLoader();
        _excelData = loader.Load();
        InitGameData();
    }

    void InitGameData()
    {
        clientData = new ExcelGameData();
        serverData = new ExcelGameData();
        int row = PassIgnoreRow();
        //名称行和类型行
        if (row + 1 >= _excelData.count)
            return;
        row = ProcessFieldNames(row);
        row = ProcessFieldTypes(row);
        ProcessExcelContent(row);
    }

    public void ProcessExcelContent(int row)
    {
        int lineNum = 0;
        for(int rowNum = row; rowNum < _excelData.count; rowNum++)
        {
            var rowData = _excelData.GetRow(rowNum);
            //空行跳过
            if (rowData.IsEmpty)
                continue;
            //结束颜色判断
            if (rowData.GetCell(0).rule == ExcelRule.Finish)
                break;
            for(int column = 0; column < rowData.count; column++)
            {
                var cell = rowData.GetCell(column);
                //cell允许empty
                if(clientFieldIndexList.Contains(column))
                {
                    clientData.AddCell(lineNum, cell);
                }

                if(serverFieldIndexList.Contains(column))
                {
                    serverData.AddCell(lineNum, cell);
                }
            }
            lineNum++;
        }
    }

    public int ProcessFieldTypes(int row)
    {
        var rowData = _excelData.GetRow(row);
        for(int i = 0; i < rowData.count; i++)
        {
            var cell = rowData.GetCell(i);
            string type = "string";

            if(clientFieldIndexList.Contains(i) || serverFieldIndexList.Contains(i))
                if (!SupportTypeUtil.TryGetType(cell.stringValue, out type))
                    Debug.LogError(string.Format("{0}  不支持类型 {1}  替换为string ", _fileName, cell.stringValue));
            
            //单次循环处理完 
            if (clientFieldIndexList.Contains(i))
            {
                clientData.AddFieldType(type);
            }

            if (serverFieldIndexList.Contains(i))
            {
                serverData.AddFieldType(type);
            }
        }
        return ++row;
    }

    public int ProcessFieldNames(int row)
    {
        var rowData = _excelData.GetRow(row);
        HashSet<string> fieldNameSet = new HashSet<string>();
        for(int i = 0; i < rowData.count; i++)
        {
            var cell = rowData.GetCell(i);
            if (cell.IsEmpty)
                continue;
            if (fieldNameSet.Contains(cell.stringValue))
            {
                Debug.LogError(string.Format("表{0}  {1}字段名重复", _fileName, cell.stringValue));
                continue;
            }
            fieldNameSet.Add(cell.stringValue);

            if(IsClientType(cell.rule))
            {
                clientData.AddFieldName(cell.stringValue);
                clientFieldIndexList.Add(i);
            }

            if (IsServerType(cell.rule))
            {
                serverData.AddFieldName(cell.stringValue);
                serverFieldIndexList.Add(i);
            }
        }

        return ++row;
    }

    //空行，第一个cell是ignore或者包含//  都表示注释行
    int PassIgnoreRow()
    {
        for (int i = 0; i < _excelData.count; i++)
        {
            var row = _excelData.GetRow(i);
            if (row.IsEmpty || row.GetCell(0).stringValue.StartsWith("//") || row.GetCell(0).rule == ExcelRule.Ignore)
                continue;
            return i;
        }
        return _excelData.count;
    }



    bool IsClientType(ExcelRule rule)
    {
        return rule == ExcelRule.Client || rule == ExcelRule.Common;
    }

    bool IsServerType(ExcelRule rule)
    {
        return rule == ExcelRule.Server || rule == ExcelRule.Common;
    }

    IExcelLoader GetLoader()
    {
        return new NPOILoader(_fullPath);
    }


}
