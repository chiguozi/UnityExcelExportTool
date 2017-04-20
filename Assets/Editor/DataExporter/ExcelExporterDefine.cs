using System;
using UnityEngine;

public enum ExcelDataExportType
{
    Text,
    Bytes,
    Json,
    ScriptObject,
}

[Serializable]
public class ExcelExporterUtil
{
    //可以指定不同工程
    [SerializeField]
    public static string AssetPath = Application.dataPath;
    [SerializeField]
    public static string ExcelPath = "";

    public static ExcelDataExportType exportType = ExcelDataExportType.Text;

    public static string ClientDataOutputPath = AssetPath + "/Resources/Data/";

    public static string ClientScriptOutputPath = AssetPath + "/Script/Data/";

    public static string ClientClassExt = ".cs";

    public static string ClientClassPre = "Cfg";

    public static string ConfigFactoryName = "ConfigFactory";

 

    public const string XLSEXT = ".xls";
    public const string XLSXEXT = ".xlsx";


    public static string SeverDataOutputPath = "";
    public static string SeverClassOutputPath = "";


    //测试使用，正式使用同一个路径
    public static string GetClientClassOutputPath()
    {
        string subPath = "";
        switch(exportType)
        {
            case ExcelDataExportType.Text:
                subPath = "ConfigT/";
                break;
            case ExcelDataExportType.Json:
                subPath = "ConfigJ/";
                break;
            case ExcelDataExportType.Bytes:
                subPath = "ConfigB/";
                break;
            case ExcelDataExportType.ScriptObject:
                subPath = "ConfigS/";
                break;
        }
        return ClientScriptOutputPath + subPath;
    }

    public static string GetClientDataOutputPath()
    {
        string subPath = "";
        switch (exportType)
        {
            case ExcelDataExportType.Text:
                subPath = "DataT/";
                break;
            case ExcelDataExportType.Json:
                subPath = "DataJ/";
                break;
            case ExcelDataExportType.Bytes:
                subPath = "DataB/";
                break;
            case ExcelDataExportType.ScriptObject:
                subPath = "DataS/";
                break;
        }
        return ClientDataOutputPath + subPath;
    }


    public static string GetConfigFactoryFullPath()
    {
        return ClientScriptOutputPath + ConfigFactoryName + ClientClassExt;
    }

    public static string GetClientClassFileName(string fileName)
    {
        fileName = fileName.Substring(0, 1).ToUpper() + fileName.Substring(1);
        return ClientClassPre + fileName;
    }

    //暂时所有类型都使用.bytes结尾
    public static string GetDataFileFullName(string excelName)
    {
        return excelName + ".bytes";
    }

}
