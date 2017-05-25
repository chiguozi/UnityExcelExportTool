using System.IO;

public class ExcelScriptableObjectClassGenerater : IExcelClassGenerater
{
    public void GenerateClass(string savePath, string className, ExcelGameData data)
    {
        if (!Directory.Exists(savePath))
            Directory.CreateDirectory(savePath);

        string fileName = className + ExcelExporterUtil.ClientClassExt;
        var content = ExcelExporterUtil.GenerateCommonClassStr("Config.ScriptableConfig", className, "ConfigSoBase", data);

        File.WriteAllText(savePath + fileName, content);
    }
}
