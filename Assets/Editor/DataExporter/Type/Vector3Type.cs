using System;
using UnityEngine;

public class Vector3Type : IType
{
    public string lowerName { get { return "vector3"; } }
    public string realName { get { return "Vector3"; } }
    public Type type { get { return typeof(int); } }

    public string parseFuncName { get { return "ParseVector3"; } }

    public bool isUnityType { get { return true; } }

    public string jsonAttributeStr { get { return "[JsonConverter(typeof(Vector3Converter))]"; } }

    public object GetValue(string content)
    {
        Vector3 value;
        StringUtil.TryParseVector3(content, out value);
        return value;
    }

    public bool CheckValue(string content)
    {
        Vector3 value;
        return StringUtil.TryParseVector3(content, out value);
    }
}
