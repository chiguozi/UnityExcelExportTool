using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;

//不写成静态类
//@todo  添加模板方式
public class ExcelClassGenerater : IExcelClassGenerater
{
    public void GenerateClientClass(string savePath, string className, ExcelGameData data)
    {
        if (!Directory.Exists(savePath))
            Directory.CreateDirectory(savePath);
        string fileName = className + ExcelExporterDefine.ClientClassExt;

        List<string> types = data.fieldTypeList;
        List<string> fields = data.fieldNameList;
        StringBuilder sb = new StringBuilder();
        sb.AppendLine("using System;");
        sb.AppendLine("using System.Collections.Generic;");
        sb.AppendLine("using UnityEngine;");
        sb.AppendLine();
        sb.AppendLine("public class " + className + " : ConfigBase");
        sb.AppendLine("{");

        //跳过ID 字段
        for (int i = 1; i < types.Count; i++)
        {
            if (Regex.IsMatch(types[i], @"^[a-zA-Z_0-9><,]*$") && Regex.IsMatch(fields[i], @"^[a-zA-Z_0-9]*$"))
                sb.AppendLine(string.Format("\tpublic {0} {1};", types[i], fields[i]));
        }

        sb.AppendLine();
        sb.AppendLine();

        sb.AppendLine("\tpublic override void Write(int i, string value)");
        sb.AppendLine("\t{");
        sb.AppendLine("\t\tswitch (i)");
        sb.AppendLine("\t\t{");

        for (int i = 0; i < types.Count; i++)
        {
            sb.AppendLine("\t\t\tcase " + i + ":");
            //默认第一个字段名称为ID  先临时处理
            sb.AppendLine("\t\t\t\t" + (i == 0 ? "ID" : fields[i]) + " = " + SupportTypeUtil.GetTypePraseFuncName(types[i]) + "(value);");
            sb.AppendLine("\t\t\t\tbreak;");
        }

        sb.AppendLine("\t\t\tdefault:");
        sb.AppendLine("\t\t\t\tUnityEngine.Debug.LogError(GetType().Name + \"src i:\" + i);");
        sb.AppendLine("\t\t\t\tbreak;");
        sb.AppendLine("\t\t}");
        sb.AppendLine("\t}");
        sb.AppendLine("}");

        File.WriteAllText(savePath + fileName, sb.ToString());
    }

    public void GenerateServerClass(string savePath, string className, ExcelGameData data)
    { }

    public void GenerateClientClassFactory(string dataPath, string savePath)
    {
        var files = Directory.GetFiles(dataPath, "*.bytes");
        List<string> classNameList = new List<string>();
        for(int i = 0; i < files.Length; i++)
        {
            //@todo +宏
            string fileName = Path.GetFileNameWithoutExtension(files[i]);
            classNameList.Add(fileName);
        }
        StringBuilder sb = new StringBuilder();
        sb.AppendLine("public class " + "ConfigFactory");
        sb.AppendLine("{");
        sb.AppendLine("\tpublic static ConfigBase Get(string configName)");
        sb.AppendLine("\t{");
        sb.AppendLine("\t\tswitch(configName)");
        sb.AppendLine("\t\t{");

        for (int i = 0; i < classNameList.Count; i++)
        {
            sb.AppendLine("\t\t\tcase \"" + classNameList[i] + "\":");
            sb.AppendLine("\t\t\t\treturn new " + "Cfg" + classNameList[i] + "();");
        }
        sb.AppendLine("\t\t\tdefault:");
        sb.AppendLine("\t\t\t\tUnityEngine.Debug.LogError(configName + \"not found\");");
        sb.AppendLine("\t\t\t\treturn null;");

        sb.AppendLine("\t\t}");
        sb.AppendLine("\t}");
        sb.AppendLine("}");
        File.WriteAllText(Path.Combine(savePath, "ConfigFactory.cs"), sb.ToString());
    }

}
