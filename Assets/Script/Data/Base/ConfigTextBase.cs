using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ConfigTextBase  : IConfigBase
{
    public int ID;


    protected int ParseInt(string value)
    {
        int res;
        if(!StringUtil.TryParseInt(value, out res))
        {
            Debug.LogError(string.Format("{0} 解析int 出错   value = {1}", this.GetType(), value));
        }
        return res;
    }
    protected float ParseFloat(string value)
    {
        float res;
        if (!StringUtil.TryParseFloat(value, out res))
        {
            Debug.LogError(string.Format("{0} 解析float 出错   value = {1}", this.GetType(), value));
        }
        return res;
    }

    protected string ParseDes(string value)
    {
        string des;
        StringUtil.TryParseDes(value, out des);
        return des;
    }

    protected List<int> ParseListInt(string value)
    {
        List<int> res;
        if(!StringUtil.TryParseListInt(value, out res))
        {
            Debug.LogError(string.Format("{0} 解析List<int> 出错   value = {1}", this.GetType(), value));
        }
        return res;
    }

    protected List<float> ParseListFloat(string value)
    {
        List<float> res;
        if (!StringUtil.TryParseListFloat(value, out res))
        {
            Debug.LogError(string.Format("{0} 解析List<float> 出错   value = {1}", this.GetType(), value));
        }
        return res;
    }

    protected List<string> ParseListString(string value)
    {
        List<string> res;
        if (!StringUtil.TryParseListString(value, out res))
        {
            Debug.LogError(string.Format("{0} 解析List<string> 出错   value = {1}", this.GetType(), value));
        }
        return res;
    }

    protected Dictionary<int, int> ParseDicIntInt(string value)
    {
        Dictionary<int, int> res;
        if (!StringUtil.TryParseDicIntInt(value, out res))
        {
            Debug.LogError(string.Format("{0} 解析DicIntInt 出错   value = {1}", this.GetType(), value));
        }
        return res;
    }

    protected Dictionary<int, string> ParseDicIntString(string value)
    {
        Dictionary<int, string> res;
        if (!StringUtil.TryParseDicIntString(value, out res))
        {
            Debug.LogError(string.Format("{0} 解析DicIntString 出错   value = {1}", this.GetType(), value));
        }
        return res;
    }

    protected Vector3 ParseVector3(string value)
    {
        Vector3 res;
        if(!StringUtil.TryParseVector3(value, out res))
        {
            Debug.LogError(string.Format("{0} 解析Vector3 出错   value = {1}", this.GetType(), value));
        }
        return res;
    }

    protected Vector2 ParseVector2(string value)
    {
        Vector2 res;
        if (!StringUtil.TryParseVector2(value, out res))
        {
            Debug.LogError(string.Format("{0} 解析Vector2 出错   value = {1}", this.GetType(), value));
        }
        return res;
    }


    protected string ParseString(string value)
    {
        return value;
    }

    protected List<List<string>> ParseListListString(string value)
    {
        List<List<string>> res;
        StringUtil.TryParseListListString(value, out res);
        return res;
    }

    public virtual void Write(int i, string value)
    {
        Debug.LogError(GetType().Name + "未找到第 + " + i + "个字段");
    }
}
