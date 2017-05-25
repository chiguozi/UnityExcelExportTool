using System;
using System.Collections.Generic;

public class ListIntType : IType
{
    public string lowerName { get { return "list<int>"; } }
    public string realName { get { return "List<int>"; } }
    public Type type { get { return typeof(List<int>); } }

    public string parseFuncName { get { return "ParseListInt"; } }

    public bool isUnityType { get { return false; } }

    public string jsonAttributeStr { get { return ""; } }

    public object GetValue(string content)
    {
        List<int> res;
        StringUtil.TryParseListInt(content, out res);
        return res;
    }

    public bool CheckValue(string content)
    {
        List<int> res;
        return StringUtil.TryParseListInt(content, out res);
    }
}
