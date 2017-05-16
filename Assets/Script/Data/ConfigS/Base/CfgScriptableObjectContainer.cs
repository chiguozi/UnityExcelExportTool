using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Config.ScriptableConfig
{
    public class CfgScriptableObjectContainer : ScriptableObject, ISerializationCallbackReceiver
    {
        public List<ConfigSoBase> dataList = new List<ConfigSoBase>();
        public Dictionary<int, ConfigSoBase> dataMap = new Dictionary<int, ConfigSoBase>();
        public string typeName;

        public void OnBeforeSerialize()
        {

        }

        public void OnAfterDeserialize()
        {
            CopyListToDic();
        }

        void CopyListToDic()
        {
            //编译完成后会调用该方法
            if (dataList.Count == 0 || dataList[0].ID == 0)  
                return;
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

        public T Get<T>(int id) where  T : ConfigSoBase
        {
            ConfigSoBase value = null;
            dataMap.TryGetValue(id, out value);
            return (T)value;
        }

    }
}

