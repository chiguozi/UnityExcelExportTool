using System.Collections.Generic;
using UnityEngine;
using System;

public class ConfigManager
{
    static Dictionary<string, Dictionary<int, ConfigBase>> _map = new Dictionary<string, Dictionary<int, ConfigBase>>();

    public static T GetConfig<T>(string configName, int id) where T : ConfigBase
    {
        if (!_map.ContainsKey(configName))
            return null;
        if (!_map[configName].ContainsKey(id))
            return null;
        return (T)_map[configName][id];
    }

    public static T GetConfig<T>(int id) where T : ConfigBase
    {
        string configName = typeof(T).Name;
        return GetConfig<T>(configName, id);
    }

    public static Dictionary<int, ConfigBase> GetConfigs(string configName)
    {
        if (_map.ContainsKey(configName))
            return _map[configName];

        return null;
    }


    public static void Init()
    {
        var datas = Resources.LoadAll("ConfigText");
        for(int i = 0; i < datas.Length; i++) 
        {
            string configName = datas[i].name;
            string content = ( datas[i] as TextAsset ).text;
            DecodeConfigFile(configName, content);
        }
    }

    static void DecodeConfigFile(string configName, string content)
    {
        var config = ConfigFactory.Get(configName);
        if (config == null)
            return;
        string className = config.GetType().Name;
        Dictionary<int, ConfigBase> configMap = new Dictionary<int, ConfigBase>();
        string[] lines = content.Replace("\r", "").Split('\n');
        for (int i = 0; i < lines.Length; i++)
        {
            var strs = lines[i].Split('\t');
            if (strs.Length == 0 || strs[0] == string.Empty)
                continue;
            config = ConfigFactory.Get(configName);
            //字符串末尾多加一个\t导致数组长度多了1
            for (int j = 0; j < strs.Length; j++)
            {
                config.Write(j, strs[j]);
            }
            if (!configMap.ContainsKey(config.ID))
                configMap.Add(config.ID, config);
            else
            {
                Debug.LogError("ID 重复  " + className);
            }
        }
        _map.Add(className, configMap);
    }

}