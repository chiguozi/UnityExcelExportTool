using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SupportTypeUtil
{
    static Dictionary<string, string> _supportCSTypeSet = new Dictionary<string, string>()
    {
        { "string", "string" },
        { "int", "int" },
        { "float", "float" },
        { "list<int>", "List<int>" },
        { "list<string>", "List<string>" },
        { "list<float>", "List<float>" },
        { "dictionary<int,int>", "Dictionary<int, int>" },
        { "dictionary<int, string>", "Dictionary<int, string>" },
        //{ "dictionary<string, int>", "Dictionary<string, int>" },
        //{ "dictionary<string, string>", "Dictionary<string, string>" },
        { "des", "des" },
    };

    static Dictionary<string,string> _supportUnityTypeSet = new Dictionary<string, string>()
    {
        { "vector3", "Vector3" },
        { "vector2", "Vector2" },
    };

    //Text 模式下使用
    static Dictionary<string, string> _supportTypeToFuncNameMap = new Dictionary<string, string>()
    {
        { "string", "PraseString"},
        { "int", "PraseInt" },
        { "float", "PraseFloat" },
        { "list<int>", "PraseListInt" },
        { "list<string>", "PraseListString" },
        { "list<float>", "PraseListFloat" },
        { "dictionary<int,int>", "PraseDicIntInt" },
        { "dictionary<int, string>", "PraseDicIntString" },
        //{ "dictionary<string, int>", "PraseDicStringInt" },
        //{ "dictionary<string, string>", "PraseDicStringString" },
        { "des", "PraseDes" },
    };


    static public bool TryGetType(string origin, out string formatType)
    {
        formatType = "string";
        if (origin == null)
            return false;
        origin = origin.ToLower();
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

    static public string GetTypePraseFuncName(string key)
    {
        key = key.ToLower();
        if(_supportTypeToFuncNameMap.ContainsKey(key))
        {
            return _supportTypeToFuncNameMap[key];
        }
        return "PraseString";
    }
}
