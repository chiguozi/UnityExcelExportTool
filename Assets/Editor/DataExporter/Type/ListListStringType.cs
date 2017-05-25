using System;
using System.Collections.Generic;

public class ListListStringType : IType
{
    public string lowerName { get { return "list<list<string>>"; } }
    public string realName { get { return "List<List<string>>"; } }
    public Type type { get { return typeof(List<List<string>>); } }

    public string parseFuncName { get { return "ParseListListString"; } }

    public bool isUnityType { get { return false; } }

    public string jsonAttributeStr { get { return ""; } }

    public object GetValue(string content)
    {
        List<List<string>> res;
        StringUtil.TryParseListListString(content, out res);
        return res;
    }

    public bool CheckValue(string content)
    {
        List<List<string>> res;
        return StringUtil.TryParseListListString(content, out res);
    }
}
