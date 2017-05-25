using System;

public class FloatType : IType
{
    public string lowerName { get { return "float"; } }
    public string realName { get { return "float"; } }
    public Type type { get { return typeof(float); } }

    public string parseFuncName { get { return "ParseFloat"; } }

    public bool isUnityType { get { return false; } }

    public string jsonAttributeStr { get { return ""; } }

    public object GetValue(string content)
    {
        float value;
        StringUtil.TryParseFloat(content, out value);
        return value;
    }

    public bool CheckValue(string content)
    {
        float value;
        return StringUtil.TryParseFloat(content, out value);
    }
}

