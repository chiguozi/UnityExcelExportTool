using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class ExcelBinaryDataGenerater : IExcelDataGenerater
{

    public void GenerateData(string savePath, string fileName, ExcelGameData data, string className)
    {
        if (!Directory.Exists(savePath))
            Directory.CreateDirectory(savePath);
        //需要添加程序集名称
        var type = Type.GetType("Config.BinaryConfig." + className + ", Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null");
        if (type == null)
        {
            Debug.LogError("找不到类型" + "Config.BinaryConfig." + className);
            return;
        }
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
