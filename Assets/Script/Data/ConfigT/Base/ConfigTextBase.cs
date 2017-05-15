using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ConfigTextBase  : IConfigBase
{
    public int ID;


    protected int PraseInt(string value)
    {
        int res;
        if(!StringUtil.TryPraseInt(value, out res))
        {
            Debug.LogError(string.Format("{0} 解析int 出错   value = {1}", this.GetType(), value));
        }
        return res;
    }
    protected float PraseFloat(string value)
    {
        float res;
        if (!StringUtil.TryPraseFloat(value, out res))
        {
            Debug.LogError(string.Format("{0} 解析float 出错   value = {1}", this.GetType(), value));
        }
        return res;
    }

    protected string PraseDes(string value)
    {
        string des;
        StringUtil.TryPraseDes(value, out des);
        return des;
    }

    protected List<int> PraseListInt(string value)
    {
        List<int> res;
        if(!StringUtil.TryPraseListInt(value, out res))
        {
            Debug.LogError(string.Format("{0} 解析List<int> 出错   value = {1}", this.GetType(), value));
        }
        return res;
    }

    protected List<string> PraseListString(string value)
    {
        List<string> res;
        if (!StringUtil.TryPraseListString(value, out res))
        {
            Debug.LogError(string.Format("{0} 解析List<string> 出错   value = {1}", this.GetType(), value));
        }
        return res;
    }

    protected Dictionary<int, int> PraseDicIntInt(string value)
    {
        Dictionary<int, int> res;
        if (!StringUtil.TryPraseDicIntInt(value, out res))
        {
            Debug.LogError(string.Format("{0} 解析DicIntInt 出错   value = {1}", this.GetType(), value));
        }
        return res;
    }

    protected Dictionary<int, string> PraseDicIntString(string value)
    {
        Dictionary<int, string> res;
        if (!StringUtil.TryPraseDicIntString(value, out res))
        {
            Debug.LogError(string.Format("{0} 解析DicIntString 出错   value = {1}", this.GetType(), value));
        }
        return res;
    }

    protected Vector3 PraseVector3(string value)
    {
        Vector3 res;
        if(!StringUtil.TryPraseVector3(value, out res))
        {
            Debug.LogError(string.Format("{0} 解析Vector3 出错   value = {1}", this.GetType(), value));
        }
        return res;
    }

    protected Vector2 PraseVector2(string value)
    {
        Vector2 res;
        if (!StringUtil.TryPraseVector2(value, out res))
        {
            Debug.LogError(string.Format("{0} 解析Vector2 出错   value = {1}", this.GetType(), value));
        }
        return res;
    }


    protected string PraseString(string value)
    {
        return value;
    }

    public virtual void Write(int i, string value)
    {
        Debug.LogError(GetType().Name + "未找到第 + " + i + "个字段");
    }
}
