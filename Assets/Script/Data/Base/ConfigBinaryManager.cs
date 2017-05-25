using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using System.IO;

public class ConfigBinaryManager
{
    static ConfigBinaryManager _instance;
    public static ConfigBinaryManager Instance
    {
        get
        {
            if (_instance == null)
                _instance = new ConfigBinaryManager();
            return _instance;
        }
    }
    Dictionary<string, Dictionary<int, ConfigBinaryBase>> _map = new Dictionary<string, Dictionary<int, ConfigBinaryBase>>();

    public void Init()
    {
        _map.Clear();
        BinaryFormatter formatter = new BinaryFormatter();
        var files = Directory.GetFiles(Application.dataPath + "/Resources/DataB/", "*.bytes");
        for(int i = 0; i < files.Length; i++)
        {
            FileStream fs = new FileStream(files[i], FileMode.Open, FileAccess.Read, FileShare.Read);
            var container = formatter.Deserialize(fs) as ConfigBinaryContainer;
            _map.Add(container.typeName, container.dataMap);
            fs.Close();
        }
    }


    public T GetConfig<T>(string configName, int id) where T : ConfigBinaryBase
    {
        if (!_map.ContainsKey(configName))
            return null;
        if (!_map[configName].ContainsKey(id))
            return null;
        return (T)_map[configName][id];
    }

    public T GetConfig<T>(int id) where T : ConfigBinaryBase
    {
        string configName = typeof(T).Name;
        return GetConfig<T>(configName, id);
    }

    public Dictionary<int, ConfigBinaryBase> GetConfigs(string configName)
    {
        if (_map.ContainsKey(configName))
            return _map[configName];

        return null;
    }
}
