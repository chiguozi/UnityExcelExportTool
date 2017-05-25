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


    public static string GetClientClassOutputPath()
    {
        //string subPath = "";
        //switch(exportType)
        //{
        //    case ExcelDataExportType.Text:
        //        subPath = "/ConfigT/";
        //        break;
        //    case ExcelDataExportType.Json:
        //        subPath = "/ConfigJ/";
        //        break;
        //    case ExcelDataExportType.Bytes:
        //        subPath = "/ConfigB/";
        //        break;
        //    case ExcelDataExportType.ScriptObject:
        //        subPath = "/ConfigS/";
        //        break;
        //}
        return ClientScriptOutputPath + "/";
    }

    public static string GetClientDataOutputPath()
    {
        //string subPath = "";
        //switch (exportType)
        //{
        //    case ExcelDataExportType.Text:
        //        subPath = "/DataT/";
        //        break;
        //    case ExcelDataExportType.Json:
        //        subPath = "/DataJ/";
        //        break;
        //    case ExcelDataExportType.Bytes:
        //        subPath = "/DataB/";
        //        break;
        //    case ExcelDataExportType.ScriptObject:
        //        subPath = "/DataS/";
        //        break;
        //}
        return ClientDataOutputPath + "/";
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

    public static void AddCommonSpaceToSb(StringBuilder sb)
    {
        sb.AppendLine("using System;");
        sb.AppendLine("using System.Collections.Generic;");
        sb.AppendLine("using UnityEngine;");
        sb.AppendLine();
    }

    public static string GenerateCommonClassStr(string nameSpace, string className, string configBase, ExcelGameData data, bool needSerializable = true)
    {
        List<string> types = data.fieldTypeList;
        List<string> fields = data.fieldNameList;

        StringBuilder sb = new StringBuilder();
        AddCommonSpaceToSb(sb);
        AddContentToSb(sb, nameSpace, className, configBase, types, fields, needSerializable);

        return sb.ToString();
    }

    public static void AddContentToSb(StringBuilder sb, string nameSpace, string className, string configBase, List<string> types, List<string> fields, bool needSerializable)
    {
        sb.AppendLine("namespace " + nameSpace);
        sb.AppendLine("{");
        if (needSerializable)
            sb.AppendLine("\t[Serializable]");
        sb.AppendLine("\tpublic class " + className + ": " + configBase);
        sb.AppendLine("\t{");

        AddFieldsToSb(sb, types, fields);

        sb.AppendLine("\t}");
        sb.AppendLine("}");
    }


    public static void AddFieldsToSb(StringBuilder sb, List<string> types, List<string> fields)
    {
        for (int i = 1; i < types.Count; i++)
        {
            var type = SupportTypeUtil.GetIType(types[i]);
            if (type != null)
            {
                if(exportType == ExcelDataExportType.Json && type.isUnityType)
                {
                    sb.AppendLine("\t\t" + type.jsonAttributeStr);
                }
                sb.AppendLine(string.Format("\t\tpublic {0} {1};", type.realName, fields[i]));
            }
        }
    }

    public static Type GetDataType(string nameSpace, string className)
    {
        var type = Type.GetType(nameSpace + className + ", Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null");
        if (type == null)
        {
            Debug.LogError("找不到类型" + nameSpace + className);
            return null;
        }
        return type;
    }

    public static string RemoveWhiteSpaceOutTheWordFull(string content)
    {
        content = RemoveWhiteSpaceOutTheWord(content, ',');
        content = RemoveWhiteSpaceOutTheWord(content, '(');
        content = RemoveWhiteSpaceOutTheWord(content, ')');
        return content;
    }

    static string RemoveWhiteSpaceOutTheWord(string content, char sep)
    {
        string[] subs = content.Split(new char[] { sep });
        for (int i = 0; i < subs.Length; i++)
        {
            subs[i] = subs[i].TrimStart(' ', '\t');
            subs[i] = subs[i].TrimEnd(' ', '\t');
        }
        content = string.Join(new string(sep, 1), subs);
        return content;
    }

    //需要先移除空白符
    //@todo  正则
    public static string RemoveWordFirstQuotation(string content)
    {
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < content.Length; i++)
        {
            bool needJump = false;
            if (content[i] == '"')
            {
                //首字符需要去掉
                if (i == 0)
                    needJump = true;
                else if (content[i - 1] == '(' || content[i - 1] == ',')
                    needJump = true;
                if (i == content.Length - 1)
                    needJump = true;
                else if (content[i + 1] == ')' || content[i + 1] == ',')
                    needJump = true;
            }
            if (!needJump)
                sb.Append(content[i]);
        }
        return sb.ToString();
    }
}
