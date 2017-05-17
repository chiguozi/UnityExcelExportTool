using System;
using System.IO;
using UnityEngine;
using Newtonsoft.Json;

public class ExcelJsonDataGenerater : IExcelDataGenerater
{
    public void GenerateData(string savePath, string fileName, ExcelGameData data, string className)
    {
        if (!Directory.Exists(savePath))
            Directory.CreateDirectory(savePath);
        ////需要添加程序集名称
        //var type = Type.GetType("Config.JsonConfig." + className + ", Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null");
        //if (type == null)
        //{
        //    Debug.LogError("找不到类型" + "Config.JsonConfig." + className);
        //    return;
        //}
        var type = ExcelExporterUtil.GetDataType("Config.JsonConfig.", className);
        if (type == null)
            return;
        var objContainer = new ConfigJsonContainer();;
        objContainer.typeName = type.Name;

        for(int i = 0; i < data.cellList.Count; i++)
        {
            var cfg = data.GetObject(i, type) as ConfigJsonBase;
            objContainer.dataMap.Add(cfg.ID, cfg);
        }

        JsonSerializerSettings settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Objects };
        var content = JsonConvert.SerializeObject(objContainer, settings);
        File.WriteAllText(Path.Combine(savePath, fileName), content);
    }
}
