using System.IO;

public class ExcelScriptableObjectClassGenerater : IExcelClassGenerater
{
    public void GenerateClass(string savePath, string className, ExcelGameData data)
    {
        if (!Directory.Exists(savePath))
            Directory.CreateDirectory(savePath);

        string fileName = className + ExcelExporterUtil.ClientClassExt;
        //List<string> types = data.fieldTypeList;
        //List<string> fields = data.fieldNameList;

        //StringBuilder sb = new StringBuilder();
        //sb.AppendLine("using System;");
        //sb.AppendLine("using System.Collections.Generic;");
        //sb.AppendLine("using UnityEngine;");
        //sb.AppendLine();

        //sb.AppendLine("namespace Config.ScriptableConfig");
        //sb.AppendLine("{");
        //sb.AppendLine("\t[Serializable]");
        //sb.AppendLine("\tpublic class " + className + ": ConfigSoBase");
        //sb.AppendLine("\t{");
        //for (int i = 1; i < types.Count; i++)
        //{
        //    if (Regex.IsMatch(types[i], @"^[a-zA-Z_0-9><,]*$") && Regex.IsMatch(fields[i], @"^[a-zA-Z_0-9]*$"))
        //        sb.AppendLine(string.Format("\t\tpublic {0} {1};", types[i], fields[i]));
        //}
        //sb.AppendLine("\t}");
        //sb.AppendLine("}");
        var content = ExcelExporterUtil.GenerateCommonClassStr("Config.ScriptableConfig", className, "ConfigSoBase", data);

        File.WriteAllText(savePath + fileName, content);
    }
}
