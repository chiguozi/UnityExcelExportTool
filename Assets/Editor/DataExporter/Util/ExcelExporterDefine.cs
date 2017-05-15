using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
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

    public static ExcelDataExportType exportType = ExcelDataExportType.ScriptObject;

    public static string ClientDataOutputPath = AssetPath + "/Resources/Data/";

    public static string ClientScriptOutputPath = AssetPath + "/Script/Data/";

    public static string ClientClassExt = ".cs";

    public static string ScriptableObjectExt = ".asset";

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
                subPath = "/ConfigT/";
                break;
            case ExcelDataExportType.Json:
                subPath = "/ConfigJ/";
                break;
            case ExcelDataExportType.Bytes:
                subPath = "/ConfigB/";
                break;
            case ExcelDataExportType.ScriptObject:
                subPath = "/ConfigS/";
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
                subPath = "/DataT/";
                break;
            case ExcelDataExportType.Json:
                subPath = "/DataJ/";
                break;
            case ExcelDataExportType.Bytes:
                subPath = "/DataB/";
                break;
            case ExcelDataExportType.ScriptObject:
                subPath = "/DataS/";
                break;
        }
        return ClientDataOutputPath + subPath;
    }

    public static string GetRelativePath(string fullPath)
    {
        return fullPath.Replace(Application.dataPath, "Assets");
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

    public static string GetDataFileFullName(string excelName)
    {
        switch (exportType)
        {
            case ExcelDataExportType.ScriptObject:
                return excelName + ".asset";
            case ExcelDataExportType.Text:
                return excelName + ".bytes";
        }
        return excelName + ".bytes";
    }

    public static string GenerateCommonClassStr(string nameSpace, string className, string configBase, ExcelGameData data, bool needSerializable = true)
    {
        List<string> types = data.fieldTypeList;
        List<string> fields = data.fieldNameList;

        StringBuilder sb = new StringBuilder();
        sb.AppendLine("using System;");
        sb.AppendLine("using System.Collections.Generic;");
        sb.AppendLine("using UnityEngine;");
        sb.AppendLine();

        sb.AppendLine("namespace " + nameSpace);
        sb.AppendLine("{");
        if(needSerializable)
            sb.AppendLine("\t[Serializable]");
        sb.AppendLine("\tpublic class " + className + ": " + configBase);
        sb.AppendLine("\t{");
        for (int i = 1; i < types.Count; i++)
        {
            if (Regex.IsMatch(types[i], @"^[a-zA-Z_0-9><,]*$") && Regex.IsMatch(fields[i], @"^[a-zA-Z_0-9]*$"))
                sb.AppendLine(string.Format("\t\tpublic {0} {1};", types[i], fields[i]));
        }
        sb.AppendLine("\t}");
        sb.AppendLine("}");
        return sb.ToString();
    }

}
