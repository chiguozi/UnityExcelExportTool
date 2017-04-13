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
        { "dictionary<int, string>", "Dictionary<int, int>" },
        { "dictionary<string, int>", "Dictionary<string, int>" },
        { "dictionary<string, string>", "Dictionary<string, string>" }
    };

    static Dictionary<string,string> _supportUnityTypeSet = new Dictionary<string, string>()
    {
        { "vector3", "Vector3" },
        { "vector2", "Vector2" },
    };

    static public bool TryGetType(string origin, out string formatType)
    {
        origin = origin.ToLower();
        formatType = "string";
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
}
