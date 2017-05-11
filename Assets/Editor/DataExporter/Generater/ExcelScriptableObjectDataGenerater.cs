using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using System.Reflection;
using System.IO;

public class ExcelScriptableObjectDataGenerater : IExcelDataGenerater
{
    public void GenerateData(string savePath, string fileName, ExcelGameData data, string className)
    {
        //需要添加程序集名称
        var type = Type.GetType("Config.ScriptableConfig." + className + ", Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null");
        if(type == null)
        {
            Debug.LogError("找不到类型" + "Config.ScriptableConfig." + className);
            return;
        }
        var scriptObjType = typeof(Config.ScriptableConfig.CfgScriptableObjectContainer);
        var objContainer = ScriptableObject.CreateInstance(scriptObjType);
        for(int i = 0; i < data.cellList.Count; i++)
        {
            var field = objContainer.GetType().GetField("dataList", BindingFlags.Public | BindingFlags.Instance);
            IList fieldValue = (IList)field.GetValue(objContainer);
            fieldValue.Add(data.GetObject(i, type));
            //var addMethod = field.FieldType.GetMethod("Add", BindingFlags.Public | BindingFlags.InvokeMethod | BindingFlags.Instance);
            //addMethod.Invoke(field.GetValue(objContainer), new object[] { data.GetObject(i, type)});
        }

        string relativePath = ExcelExporterUtil.GetRelativePath(savePath);
        AssetDatabase.CreateAsset(objContainer, Path.Combine(relativePath, fileName));
    }
} 
