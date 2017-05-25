using System.Collections.Generic;
using System;

public class DictionaryIntIntType : IType
{
    public string lowerName { get { return "dictionary<int,int>"; } }
    public string realName { get { return "Dictionary<int, int>"; } }
    public Type type { get { return typeof(Dictionary<int, int>); } }

    public string parseFuncName { get { return "ParseDicIntInt"; } }

    public bool isUnityType { get { return false; } }

    public string jsonAttributeStr { get { return ""; } }

    public object GetValue(string content)
    {
        Dictionary<int, int> res;
        StringUtil.TryParseDicIntInt(content, out res);
        return res;
    }

    public bool CheckValue(string content)
    {
        Dictionary<int, int> res;
        return StringUtil.TryParseDicIntInt(content, out res);
    }
}
