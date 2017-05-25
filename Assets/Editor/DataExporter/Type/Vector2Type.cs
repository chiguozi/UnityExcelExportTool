using System;
using UnityEngine;

public class Vector2Type : IType
{
    public string lowerName { get { return "vector2"; } }
    public string realName { get { return "Vector2"; } }
    public Type type { get { return typeof(int); } }

    public string parseFuncName { get { return "ParseVector2"; } }

    public bool isUnityType { get { return true; } }

    public string jsonAttributeStr { get { return "[JsonConverter(typeof(Vector2Converter))]"; } }

    public object GetValue(string content)
    {
        Vector2 value;
        StringUtil.TryParseVector2(content, out value);
        return value;
    }

    public bool CheckValue(string content)
    {
        Vector2 value;
        return StringUtil.TryParseVector2(content, out value);
    }
}
