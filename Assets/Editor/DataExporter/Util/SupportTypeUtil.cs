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


    static Dictionary<string, string> _supportCSTypeSet = new Dictionary<string, string>()
    {
        { "string", "string" },
        { "int", "int" },
        { "float", "float" },
        { "list<int>", "List<int>" },
        { "list<string>", "List<string>" },
        { "list<float>", "List<float>" },
        { "dictionary<int,int>", "Dictionary<int, int>" },
        { "dictionary<int,string>", "Dictionary<int, string>" },
        //{ "dictionary<string, int>", "Dictionary<string, int>" },
        //{ "dictionary<string, string>", "Dictionary<string, string>" },
        { "des", "des" },
    };

    static Dictionary<string,string> _supportUnityTypeSet = new Dictionary<string, string>()
    {
        { "vector3", "Vector3" },
        { "vector2", "Vector2" },
    };

    static Dictionary<string, string> _supportTypeToJsonAttributeMap = new Dictionary<string, string>()
    {
        { "vector3", "[JsonConverter(typeof(Vector3Converter))]" },
        { "vector2", "[JsonConverter(typeof(Vector2Converter))]" },
    };

    //Text 模式下使用
    static Dictionary<string, string> _supportTypeToFuncNameMap = new Dictionary<string, string>()
    {
        { "string", "ParseString"},
        { "int", "ParseInt" },
        { "float", "ParseFloat" },
        { "list<int>", "ParseListInt" },
        { "list<string>", "ParseListString" },
        { "list<float>", "ParseListFloat" },
        { "dictionary<int,int>", "ParseDicIntInt" },
        { "dictionary<int,string>", "ParseDicIntString" },
        { "vector3", "ParseVector3"},
        { "vector2", "ParseVector2"},
        //{ "dictionary<string, int>", "ParseDicStringInt" },
        //{ "dictionary<string, string>", "ParseDicStringString" },
        { "des", "ParseDes" },
    };

    static Dictionary<string, Type> _supprtTypeToTypeNameMap = new Dictionary<string, Type>()
    {
        { "string", typeof(string)},
        { "int", typeof(int)},
        { "float", typeof(float) },
        { "list<int>", typeof(List<int>) },
        { "list<string>",typeof(List<string>)  },
        { "list<float>", typeof(List<float>)  },
        { "dictionary<int,int>",  typeof(Dictionary<int, int>)  },
        { "dictionary<int,string>",typeof(Dictionary<int, string>)},
        { "des", typeof(string) },
        { "vector3", typeof(Vector3)},
        { "vector2", typeof(Vector2)},
    };

    public static bool IsUnityType(string type)
    {
        type = type.ToLower().Replace(" ", "");
        return _supportUnityTypeSet.ContainsKey(type);
    }

    public static string GetUnityTypeJsonAttribute(string type)
    {
        type = type.ToLower().Replace(" ", "");
        return _supportTypeToJsonAttributeMap[type];
    }

    static public bool TryGetTypeName(string origin, out string formatType)
    {
        formatType = "string";
        if (origin == null)
            return false;
        origin = origin.ToLower();
        origin = origin.Replace(" ", "");
        if(_supportCSTypeSet.ContainsKey(origin))
        {
            formatType = _supportCSTypeSet[origin];
            return true;
        }
        if(_supportUnityTypeSet.ContainsKey(origin))
        {
            formatType = _supportUnityTypeSet[origin];
            return true;
        }
        return false;
    }


    static public Type TryGetType(string origin)
    {
        Type type = null;
        origin = origin.ToLower();
        origin = origin.Replace(" ", "");
        if (_supprtTypeToTypeNameMap.ContainsKey(origin))
        {
            type = _supprtTypeToTypeNameMap[origin];
        }
        return type;
    }

    static public string GetTypeParseFuncName(string key)
    {
        key = key.ToLower();
        key = key.Replace(" ", "");
        if (_supportTypeToFuncNameMap.ContainsKey(key))
        {
            return _supportTypeToFuncNameMap[key];
        }
        return "ParseString";
    }
}
