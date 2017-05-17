using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class ExcelBinaryDataGenerater : IExcelDataGenerater
{

    public void GenerateData(string savePath, string fileName, ExcelGameData data, string className)
    {
        if (!Directory.Exists(savePath))
            Directory.CreateDirectory(savePath);
      
        var type = ExcelExporterUtil.GetDataType("Config.BinaryConfig.", className);
        if (type == null)
            return;
        var objContainer = new ConfigBinaryContainer();
        objContainer.typeName = type.Name;
        for (int i = 0; i < data.cellList.Count; i++)
        {
            var cfg = data.GetObject(i, type) as ConfigBinaryBase;
            objContainer.dataMap.Add(cfg.ID, cfg);
        }
        BinaryFormatter formater = new BinaryFormatter();
        var fileStream = new FileStream(Path.Combine(savePath, fileName), FileMode.OpenOrCreate);
        formater.Serialize(fileStream, objContainer);
        fileStream.Close();
        //File.WriteAllText(Path.Combine(savePath, fileName), content);
    }
}
