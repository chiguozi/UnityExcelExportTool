using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using UnityEngine;

public class ExcelJsonDataGenerater : IExcelDataGenerater
{
    public void GenerateData(string savePath, string fileName, ExcelGameData data, string className)
    {
        if (!Directory.Exists(savePath))
            Directory.CreateDirectory(savePath);
        //需要添加程序集名称
        var type = Type.GetType("Config.JsonConfig." + className + ", Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null");
        if (type == null)
        {
            Debug.LogError("找不到类型" + "Config.JsonConfig." + className);
            return;
        }
        var objContainer = new ConfigJsonContainer();
        objContainer.typeName = type.Name;

        for(int i = 0; i < data.cellList.Count; i++)
        {
            var method = data.GetType().GetMethod("GetJsonObject");
            method = method.MakeGenericMethod(type);
            objContainer.dataList.Add((method.Invoke(data, new object[] { i, type } ) as Config.JsonConfig.CfgTest));
        }

        var content = JsonUtility.ToJson(objContainer, true);
        File.WriteAllText(Path.Combine(savePath, fileName), content);
    }
}
