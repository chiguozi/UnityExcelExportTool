using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

public class ConfigJsonManager
{
    static ConfigJsonManager _instance;
    public static ConfigJsonManager Instance
    {
        get
        {
            if (_instance == null)
                _instance = new ConfigJsonManager();
            return _instance;
        }
    }
    Dictionary<string, Dictionary<int, ConfigJsonBase>> _map = new Dictionary<string, Dictionary<int, ConfigJsonBase>>();

    public void Init()
    {
        _map.Clear();
        JsonSerializerSettings settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Objects };
        var datas = Resources.LoadAll<UnityEngine.TextAsset>("DataJ");
        for (int i = 0; i < datas.Length; i++)
        {
            var data = JsonConvert.DeserializeObject<ConfigJsonContainer>(datas[i].text, settings);
            _map.Add(data.typeName, data.dataMap);
        }
    }


    public T GetConfig<T>(string configName, int id) where T : ConfigJsonBase
    {
        if (!_map.ContainsKey(configName))
            return null;
        if (!_map[configName].ContainsKey(id))
            return null;
        return (T)_map[configName][id];
    }

    public T GetConfig<T>(int id) where T : ConfigJsonBase
    {
        string configName = typeof(T).Name;
        return GetConfig<T>(configName, id);
    }

    public Dictionary<int, ConfigJsonBase> GetConfigs(string configName)
    {
        if (_map.ContainsKey(configName))
            return _map[configName];

        return null;
    }
}
