using UnityEngine;
using UnityEditor;
using System.IO;

public class ExcelScriptableObjectDataGenerater : IExcelDataGenerater
{
    public void GenerateData(string savePath, string fileName, ExcelGameData data, string className)
    {
        if (!Directory.Exists(savePath))
            Directory.CreateDirectory(savePath); 
       
        var type = ExcelExporterUtil.GetDataType("Config.ScriptableConfig.", className);
        if (type == null)
            return;
        var objContainer = ScriptableObject.CreateInstance<Config.ScriptableConfig.CfgScriptableObjectContainer>();
        objContainer.typeName = type.Name;
        string relativePath = ExcelExporterUtil.GetRelativePath(savePath);
        AssetDatabase.CreateAsset(objContainer, Path.Combine(relativePath, fileName));
        for (int i = 0; i < data.cellList.Count; i++)
        {
            var dataInstance = data.GetSOObject(i, type) as ConfigSoBase;
            dataInstance.hideFlags = HideFlags.HideInInspector;
            objContainer.dataList.Add(dataInstance);
            AssetDatabase.AddObjectToAsset(dataInstance, objContainer);
        }
        AssetDatabase.SaveAssets();
    }
} 
