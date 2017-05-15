using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ExcelJsonClassGenerater : IExcelClassGenerater
{
    public void GenerateClass(string savePath, string className, ExcelGameData data)
    {
        if (!Directory.Exists(savePath))
            Directory.CreateDirectory(savePath);
        string fileName = className + ExcelExporterUtil.ClientClassExt;
        var content = ExcelExporterUtil.GenerateCommonClassStr("Config.JsonConfig", className, "ConfigJsonBase", data);
        File.WriteAllText(savePath + fileName, content);
    }
}
