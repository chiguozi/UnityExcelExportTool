using System.Collections.Generic;
using UnityEngine;
using System;
using Config.TextConfig;

public class ConfigTextManager
{
    protected static ConfigTextManager _instance;
    public static ConfigTextManager Instance
    {
        get
        {
            if (_instance == null)
                _instance = new ConfigTextManager();
            return _instance;
        }
    }
    protected Dictionary<string, Dictionary<int, ConfigTextBase>> _map = new Dictionary<string, Dictionary<int, ConfigTextBase>>();

    public T GetConfig<T>(string configName, int id) where T : ConfigTextBase
    {
        if (!_map.ContainsKey(configName))
            return null;
        if (!_map[configName].ContainsKey(id))
            return null;
        return (T)_map[configName][id];
    }

    public T GetConfig<T>(int id) where T : ConfigTextBase
    {
        string configName = typeof(T).Name;
        return GetConfig<T>(configName, id);
    }

    public Dictionary<int, ConfigTextBase> GetConfigs(string configName)
    {
        if (_map.ContainsKey(configName))
            return _map[configName];

        return null;
    }


    public void Init()
    {
        _map.Clear();
        var datas = Resources.LoadAll("DataT");
        for(int i = 0; i < datas.Length; i++) 
        {
            string configName = datas[i].name;
            string content = ( datas[i] as UnityEngine.TextAsset ).text;
            DecodeConfigFile(configName, content);
        }
    }

    void DecodeConfigFile(string configName, string content)
    {
        var config = ConfigFactory.Get(configName);
        if (config == null)
            return;
        string className = config.GetType().Name;
        Dictionary<int, ConfigTextBase> configMap = new Dictionary<int, ConfigTextBase>();
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