using System;

public class IntType : IType
{
    public string lowerName { get { return "int"; } }
    public string realName { get { return "int"; } }
    public Type type {get {return typeof(int);}}
    
    public string parseFuncName { get { return "ParseInt"; } }

    public bool isUnityType { get { return false; } }

    public string jsonAttributeStr { get { return ""; } }

    public object GetValue(string content)
    {
        int value;
        StringUtil.TryParseInt(content, out value);
        return value;
    }

    public bool CheckValue(string content)
    {
        int value;
        return StringUtil.TryParseInt(content, out value); 
    }
}
