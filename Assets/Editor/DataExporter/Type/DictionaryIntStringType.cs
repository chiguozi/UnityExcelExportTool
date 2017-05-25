using System.Collections.Generic;
using System;

public class DictionaryIntStringType : IType
{
    public string lowerName { get { return "dictionary<int,string>"; } }
    public string realName { get { return "Dictionary<int, string>"; } }
    public Type type { get { return typeof(Dictionary<int, string>); } }

    public string parseFuncName { get { return "ParseDicIntString"; } }

    public bool isUnityType { get { return false; } }

    public string jsonAttributeStr { get { return ""; } }

    public object GetValue(string content)
    {
        Dictionary<int, string> res;
        StringUtil.TryParseDicIntString(content, out res);
        return res;
    }

    public bool CheckValue(string content)
    {
        Dictionary<int, string> res;
        return StringUtil.TryParseDicIntString(content, out res);
    }
}
