using System;
using System.Collections.Generic;

public class ListFloatType : IType
{
    public string lowerName { get { return "list<float>"; } }
    public string realName { get { return "List<float>"; } }
    public Type type { get { return typeof(List<float>); } }

    public string parseFuncName { get { return "ParseListFloat"; } }

    public bool isUnityType { get { return false; } }

    public string jsonAttributeStr { get { return ""; } }

    public object GetValue(string content)
    {
        List<float> res;
        StringUtil.TryParseListFloat(content, out res);
        return res;
    }

    public bool CheckValue(string content)
    {
        List<float> res;
        return StringUtil.TryParseListFloat(content, out res);
    }
}
