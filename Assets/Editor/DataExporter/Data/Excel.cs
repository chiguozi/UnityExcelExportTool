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

    public string fileName
    {
        get
        {
            return _fileName;
        }

        set
        {
            _fileName =  value ;
        }
    }

    public ExcelData excelData
    {
        get
        {
            return _excelData;
        }

        set
        {
            _excelData =  value ;
        }
    }

    public string FullPath
    {
        get
        {
            return _fullPath;
        }

        set
        {
            _fullPath =  value ;
        }
    }

    public Excel(string fullPath)
    {
        fileName = Path.GetFileNameWithoutExtension(fullPath);
        FullPath = fullPath;
        _extension = Path.GetExtension(fullPath);
    }

    public void Load()
    {
        var loader = GetLoader();
        excelData = loader.Load();
        InitGameData();
    }

    public void Write(string outputPath)
    {
        var writer = GetWriter();
        writer.Write(outputPath, excelData);
    }

    public void Write()
    {
        Write(FullPath);
    }

    public void GenerateClientScript()
    {
        var generater = ClassFactory.GetFactory(false).Create();
        generater.GenerateClass(ExcelExporterUtil.GetClientClassOutputPath(), ExcelExporterUtil.GetClientClassFileName(fileName), clientData);
    }

    public void GenerateClientData()
    {
        var generater = DataFactory.GetFactory(false).Create();
        generater.GenerateData(ExcelExporterUtil.GetClientDataOutputPath(), ExcelExporterUtil.GetDataFileFullName(fileName), clientData, ExcelExporterUtil.GetClientClassFileName(fileName));
    }


    void InitGameData()
    {
        clientData = new ExcelGameData();
        serverData = new ExcelGameData();
        int row = PassIgnoreRow();
        //名称行和类型行
        if (row + 1 >= excelData.count)
            return;
        row = ProcessFieldNames(row);
        row = ProcessFieldTypes(row);
        ProcessExcelContent(row);
    }

    public void ProcessExcelContent(int row)
    {
        int lineNum = 0;
        for(int rowNum = row; rowNum < excelData.count; rowNum++)
        {
            var rowData = excelData.GetRow(rowNum);
            //空行跳过
            if (rowData.IsEmpty)
                continue;
            int val;
            //id字段
            if(!int.TryParse(rowData.GetCell(0).stringValue, out val))
            {
                continue;
            }
            //结束颜色判断
            if (rowData.GetCell(0).rule == ExcelRule.Finish)
                break;
            for(int column = 0; column < rowData.count; column++)
            {
                var cell = rowData.GetCell(column);
                //cell允许empty
                if(clientFieldIndexList.Contains(column))
                {
                    if(!clientData.AddCell(lineNum, cell))
                    {
                        Debug.LogErrorFormat("{0} 第{1}行 第 {2} 列 解析客户端字段异常 : {3}  type : ", fileName, lineNum, column, cell.stringValue);
                    }
                }

                if(serverFieldIndexList.Contains(column))
                {
                    if (!serverData.AddCell(lineNum, cell))
                    {
                        Debug.LogErrorFormat("{0} 第{1}行 第 {2} 列 解析服务端字段异常 : {3}", fileName, lineNum, column, cell.stringValue);
                    }
                }
            }
            lineNum++;
        }
    }

    public int ProcessFieldTypes(int row)
    {
        var rowData = excelData.GetRow(row);
        for(int i = 0; i < rowData.count; i++)
        {
            var cell = rowData.GetCell(i);
            string type = "string";

            if(clientFieldIndexList.Contains(i) || serverFieldIndexList.Contains(i))
                if (!SupportTypeUtil.TryGetTypeName(cell.stringValue, out type))
                    Debug.LogError(string.Format("{0}  不支持类型 {1}  替换为string ", fileName, cell.stringValue));
            
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
        var rowData = excelData.GetRow(row);
        HashSet<string> fieldNameSet = new HashSet<string>();
        for(int i = 0; i < rowData.count; i++)
        {
            var cell = rowData.GetCell(i);
            if (cell.IsEmpty || string.IsNullOrEmpty(cell.stringValue))
                continue;
            if (fieldNameSet.Contains(cell.stringValue))
            {
                Debug.LogError(string.Format("表{0}  {1}字段名重复", fileName, cell.stringValue));
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
        for (int i = 0; i < excelData.count; i++)
        {
            var row = excelData.GetRow(i);
            if (row.IsEmpty || row.GetCell(0).stringValue.StartsWith("//") || row.GetCell(0).rule == ExcelRule.Ignore)
                continue;
            return i;
        }
        return excelData.count;
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
        return new NPOILoader(FullPath);
    }

    //NPOI不支持2007版写入
    //EPPlus不支持2003写入
    IExcelWriter GetWriter()
    {
        if (_extension == ".xls")
            return new NPOIWriter();
        else
            return new EppWriter();
    }
}
