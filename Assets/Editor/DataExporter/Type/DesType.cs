using System;

public class DesType : IType
{
    public string lowerName { get { return "des"; } }
    public string realName { get { return "string"; } }
    public Type type { get { return typeof(string); } }

    public string parseFuncName { get { return "ParseDes"; } }

    public bool isUnityType { get { return false; } }

    public string jsonAttributeStr { get { return ""; } }

    public object GetValue(string content)
    {
        string des;
        StringUtil.TryParseDes(content, out des);
        return des;
    }

    public bool CheckValue(string content)
    {
        return true;
    }
}

