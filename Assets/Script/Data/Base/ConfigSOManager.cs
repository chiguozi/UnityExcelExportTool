using System.Collections;
using System.Collections.Generic;
using Config.ScriptableConfig;
using UnityEngine;

public class ConfigSOManager 
{
    static ConfigSOManager _instance;
    public static ConfigSOManager Instance
    {
        get
        {
            if (_instance == null)
                _instance = new ConfigSOManager();
            return _instance;
        }
    }
    Dictionary<string, Dictionary<int, ConfigSoBase>> _map = new Dictionary<string, Dictionary<int, ConfigSoBase>>();
    public void Init()
    {
        _map.Clear();
        var datas = Resources.LoadAll<CfgScriptableObjectContainer>("DataS");
        for(int i = 0; i < datas.Length; i++)
        {
            //datas[i].CopyListToDic();
            _map.Add(datas[i].typeName, datas[i].dataMap);
            Resources.UnloadAsset(datas[i]);
        }
    }


    public T GetConfig<T>(string configName, int id) where T : ConfigSoBase
    {
        if (!_map.ContainsKey(configName))
            return null;
        if (!_map[configName].ContainsKey(id))
            return null;
        return (T)_map[configName][id];
    }

    public T GetConfig<T>(int id) where T : ConfigSoBase
    {
        string configName = typeof(T).Name;
        return GetConfig<T>(configName, id);
    }

    public Dictionary<int, ConfigSoBase> GetConfigs(string configName)
    {
        if (_map.ContainsKey(configName))
            return _map[configName];

        return null;
    }

}
