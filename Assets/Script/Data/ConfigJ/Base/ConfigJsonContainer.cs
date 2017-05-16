using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Newtonsoft.Json;

[Serializable]
public class ConfigJsonContainer
{
    public List<ConfigJsonBase> dataList = new List<ConfigJsonBase>();
    public Dictionary<int, ConfigJsonBase> dataMap = new Dictionary<int, ConfigJsonBase>();
    public string typeName;
    [JsonConverter(typeof(Vector3Converter))]
    public Vector3 test = Vector3.zero;

    public void CopyListToDic()
    {
        for (int i = 0; i < dataList.Count; i++)
        {
            if (dataMap.ContainsKey(dataList[i].ID))
            {
                Debug.LogError("重复Id : " + dataList[i].ID + " 类型：" + typeName);
            }
            else
            {
                dataMap.Add(dataList[i].ID, dataList[i]);
            }
        }
    }
}
