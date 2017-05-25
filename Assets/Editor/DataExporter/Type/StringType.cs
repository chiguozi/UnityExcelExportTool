using System;

public class StringType : IType
{
    public string lowerName { get { return "string"; } }
    public string realName { get { return "string"; } }
    public Type type { get { return typeof(string); } }

    public string parseFuncName { get { return "ParseString"; } }

    public bool isUnityType { get { return false; } }

    public string jsonAttributeStr { get { return ""; } }

    public object GetValue(string content)
    {
        return content;
    }

    public bool CheckValue(string content)
    {
        return true; 
    }
}

