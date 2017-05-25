using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ConfigBinaryContainer
{
    public Dictionary<int, ConfigBinaryBase> dataMap = new Dictionary<int, ConfigBinaryBase>();
    public string typeName;
}
