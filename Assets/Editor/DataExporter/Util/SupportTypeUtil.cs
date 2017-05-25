using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SupportTypeUtil
{
    static Dictionary<string, IType> _supportTypeMap = new Dictionary<string, IType>();
    static SupportTypeUtil()
    {
        IType type = new IntType();
        _supportTypeMap.Add(type.lowerName, type);
        type = new FloatType();
        _supportTypeMap.Add(type.lowerName, type);
        type = new StringType();
        _supportTypeMap.Add(type.lowerName, type);
        type = new ListIntType();
        _supportTypeMap.Add(type.lowerName, type);
        type = new ListStringType();
        _supportTypeMap.Add(type.lowerName, type);
        type = new ListFloatType();
        _supportTypeMap.Add(type.lowerName, type);
        type = new Vector2Type();
        _supportTypeMap.Add(type.lowerName, type);
        type = new Vector3Type();
        _supportTypeMap.Add(type.lowerName, type);
        type = new DesType();
        _supportTypeMap.Add(type.lowerName, type);
        type = new DictionaryIntIntType();
        _supportTypeMap.Add(type.lowerName, type);
        type = new DictionaryIntStringType();
        _supportTypeMap.Add(type.lowerName, type);
        type = new ListListStringType();
        _supportTypeMap.Add(type.lowerName, type);
    }

    public static IType GetIType(string typeName)
    {
        string lowerType = typeName.ToLower().Replace(" ", "");
        if (_supportTypeMap.ContainsKey(lowerType))
            return _supportTypeMap[lowerType];
        Debug.LogError("找不到类型" + typeName);
        return null;
    }

    static public bool TryGetTypeName(string origin, out string formatType)
    {
        formatType = "string";
        origin = origin.ToLower();
        origin = origin.Replace(" ", "");

        if(_supportTypeMap.ContainsKey(origin))
        {
            formatType = _supportTypeMap[origin].lowerName;
            return true;
        }
        return false;
    }


    static public string GetTypeParseFuncName(string key)
    {
        key = key.ToLower();
        key = key.Replace(" ", "");
        if (_supportTypeMap.ContainsKey(key))
        {
            return _supportTypeMap[key].parseFuncName;
        }
        return "ParseString";
    }
}
