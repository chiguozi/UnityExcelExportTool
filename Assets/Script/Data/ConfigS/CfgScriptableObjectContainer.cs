using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Config.ScriptableConfig
{
    public class CfgScriptableObjectContainer : ScriptableObject
    {
        public List<ConfigBase> dataList;
        public Dictionary<int, ConfigBase> dataMap = new Dictionary<int, ConfigBase>();
        public string typeName;


        public void CopyListToDic()
        {
            for(int i = 0; i < dataList.Count; i++)
            {
                if(dataMap.ContainsKey(dataList[i].ID))
                {
                    Debug.LogError("重复Id : " + dataList[i].ID + " 类型：" + typeName);
                }
                else
                {
                    dataMap.Add(dataList[i].ID, dataList[i]);
                }
            }
        }

        public T Get<T>(int id) where  T : ConfigBase
        {
            ConfigBase value = null;
            dataMap.TryGetValue(id, out value);
            return (T)value;
        }

    }
}

