using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IConfigManager
{
    T GetConfig<T>(string configName, int id) where T : IConfigBase;
    T GetConfig<T>(int id) where T : IConfigBase;
    Dictionary<int, IConfigBase> GetConfigs(string configName);
    void Init();
}
