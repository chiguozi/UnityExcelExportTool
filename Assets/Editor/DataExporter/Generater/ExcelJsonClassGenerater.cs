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
        sb.AppendLine("using System;");
        sb.AppendLine("using System.Collections.Generic;");
        sb.AppendLine("using UnityEngine;");
        sb.AppendLine("using Newtonsoft.Json;");
        sb.AppendLine();

        sb.AppendLine("namespace " + "Config.JsonConfig");
        sb.AppendLine("{");

        sb.AppendLine("\tpublic class " + className + ": ConfigJsonBase");
        sb.AppendLine("\t{");
        for (int i = 1; i < types.Count; i++)
        {
            var type = SupportTypeUtil.GetIType(types[i]);
            if (type != null)
            {
                if(type.isUnityType)
                    sb.AppendLine("\t\t" + type.jsonAttributeStr);
                sb.AppendLine(string.Format("\t\tpublic {0} {1};", type.realName, fields[i]));
            }
               
        }
        sb.AppendLine("\t}");
        sb.AppendLine("}");
        File.WriteAllText(savePath + fileName, sb.ToString());
    }
}
