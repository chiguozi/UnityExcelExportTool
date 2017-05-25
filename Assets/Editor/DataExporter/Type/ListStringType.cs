using System;
using System.Collections.Generic;

public class ListStringType : IType
{
    public string lowerName { get { return "list<string>"; } }
    public string realName { get { return "List<string>"; } }
    public Type type { get { return typeof(List<string>); } }

    public string parseFuncName { get { return "ParseListString"; } }

    public bool isUnityType { get { return false; } }

    public string jsonAttributeStr { get { return ""; } }

    public object GetValue(string content)
    {
        List<string> res;
        StringUtil.TryParseListString(content, out res);
        return res;
    }

    public bool CheckValue(string content)
    {
        List<string> res;
        return StringUtil.TryParseListString(content, out res);
    }
}
