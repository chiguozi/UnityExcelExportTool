using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine;

public class ExcelJsonClassGenerater : IExcelClassGenerater
{
    public void GenerateClass(string savePath, string className, ExcelGameData data)
    {
        if (!Directory.Exists(savePath))
            Directory.CreateDirectory(savePath);
        string fileName = className + ExcelExporterUtil.ClientClassExt;
        //var content = ExcelExporterUtil.GenerateCommonClassStr("Config.JsonConfig", className, "ConfigJsonBase", data);
        List<string> types = data.fieldTypeList;
        List<string> fields = data.fieldNameList;

        StringBuilder sb = new StringBuilder();

        ExcelExporterUtil.AddCommonSpaceToSb(sb);

        sb.AppendLine("using Newtonsoft.Json;");
        sb.AppendLine();

        //sb.AppendLine("namespace " + "Config.JsonConfig");
        //sb.AppendLine("{");

        //sb.AppendLine("\tpublic class " + className + ": ConfigJsonBase");
        //sb.AppendLine("\t{");

        //ExcelExporterUtil.AddFieldsToSb(sb, types, fields);

        //sb.AppendLine("\t}");
        //sb.AppendLine("}");
        ExcelExporterUtil.AddContentToSb(sb, "Config.JsonConfig", className, "ConfigJsonBase", types, fields, false);


        File.WriteAllText(savePath + fileName, sb.ToString());
    }
}
