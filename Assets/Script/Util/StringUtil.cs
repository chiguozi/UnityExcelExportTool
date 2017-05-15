using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class StringUtil
{

    //type 为SupportTypeUtil修正过的字符串
    public static object GetCellObjectValue(string type, string value)
    {
        switch (type)
        {
            case "string":
                return value;
            case "int":
                int intOutput;
                TryPraseInt(value, out intOutput);
                return intOutput;
            case "float":
                float floatOutput;
                TryPraseFloat(value, out floatOutput);
                return floatOutput;
            case "List<int>":
                List<int> valueList;
                TryPraseListInt(value, out valueList);
                return valueList;
            case "List<string>":
                List<string> stringValueList;
                TryPraseListString(value, out stringValueList);
                return stringValueList;
            case "Dictionary<int, int>":
                Dictionary<int, int> intDic;
                TryPraseDicIntInt(value, out intDic);
                return intDic;
            case "Dictionary<int, string>":
                Dictionary<int, string> stringIntDic;
                TryPraseDicIntString(value, out stringIntDic);
                return stringIntDic;
            case "des":
                string des;
                TryPraseDes(value, out des);
                return des;
            case "Vector3":
                Vector3 vec3;
                TryPraseVector3(value, out vec3);
                return vec3;
            case "Vector2":
                Vector2 vec2;
                TryPraseVector2(value, out vec2);
                return vec2;
                //case "List<float>":
                //    List<float> floatList;
                //    TryPraseFloatList
        }
        return value;
    }

    public static bool TryPraseInt(string str, out int value)
    {
        value = 0;
        if (string.IsNullOrEmpty(str))
            return true;
        if (int.TryParse(str, out value))
            return true;
        return false;
    }

    public static bool TryPraseFloat(string str, out float value)
    {
        value = 0;
        if (string.IsNullOrEmpty(str))
            return true;
        if (float.TryParse(str, out value))
            return true;
        return false;
    }

    public static bool TryPraseDes(string str, out string value)
    {
        value = str.Replace("|", "\n");
        return true;
    }

    //1，2，3，4
    public static bool TryPraseListInt(string str, out List<int> valueList)
    {
        valueList = null;
        if (string.IsNullOrEmpty(str))
            return true;
        string[] values = str.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
        valueList = new List<int>();
        bool success = true;
        for (int i = 0; i < values.Length; i++)
        {
            int value;
            if (!TryPraseInt(values[i], out value))
                success = false;
            valueList.Add(value);
        }
        return success;
    }

    public static bool TryPraseListString(string str, out List<string> valueList)
    {
        valueList = null;
        if (string.IsNullOrEmpty(str))
            return true;
        string[] values = str.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
        valueList = new List<string>();
        bool success = true;
        for (int i = 0; i < values.Length; i++)
        {
            valueList.Add(values[i]);
        }
        return success;
    }
    //(1,2),(2,3)
    public static bool TryPraseDicIntInt(string str, out Dictionary<int, int> valueDic)
    {
        valueDic = null;
        if (string.IsNullOrEmpty(str))
            return true;
        str = str.TrimStart('(').TrimEnd(')');

        string[] values = str.Split(new string[] { "),(" }, StringSplitOptions.RemoveEmptyEntries);
        valueDic = new Dictionary<int, int>();
        bool success = true;
        for(int i = 0; i < values.Length; i++)
        {
            string[] nums = values[i].Split(',');
            if (nums.Length != 2)
            {
                success = false;
                continue;
            }
            int key, value;
            if (!TryPraseInt(nums[0], out key) )
            {
                success = false;
            }
            if(!TryPraseInt(nums[1], out value))
            {
                success = false;
            }
            valueDic.Add(key, value);
        }
        return success;
    }

    public static bool TryPraseDicIntString(string str, out Dictionary<int, string> valueDic)
    {
        valueDic = null;
        if (string.IsNullOrEmpty(str))
            return true;
        str = str.TrimStart('(').TrimEnd(')');

        string[] values = str.Split(new string[] { "),(" }, StringSplitOptions.RemoveEmptyEntries);
        valueDic = new Dictionary<int, string>();
        bool success = true;
        for (int i = 0; i < values.Length; i++)
        {
            string[] nums = values[i].Split(',');
            if (nums.Length != 2)
            {
                success = false;
                continue;
            }
            int key;
            if (!TryPraseInt(nums[0], out key))
            {
                success = false;
            }
            valueDic.Add(key, nums[1]);
        }
        return success;
    }

    public static bool TryPraseVector3(string str, out Vector3 vec)
    {
        vec = Vector3.zero;
        if (string.IsNullOrEmpty(str))
            return true;
        str = str.TrimStart('(').TrimEnd(')');
        string[] values = str.Split(',');
        bool success = true;
        if (values.Length != 3)
            return false;

        float x, y, z;
        if (!TryPraseFloat(values[0], out x))
            success = false;
        if (!TryPraseFloat(values[1], out y))
            success = false;
        if (!TryPraseFloat(values[2], out z))
            success = false;
        vec = new Vector3(x, y, z);
        return success;
    }

    public static  bool TryPraseVector2(string str, out Vector2 vec)
    {
        vec = Vector2.zero;
        if (string.IsNullOrEmpty(str))
            return true;
        str = str.TrimStart('(').TrimEnd(')');
        string[] values = str.Split(',');
        bool success = true;
        if (values.Length != 2)
            return false;

        float x, y;
        if (!TryPraseFloat(values[0], out x))
            success = false;
        if (!TryPraseFloat(values[1], out y))
            success = false;
        vec = new Vector3(x, y);
        return success;
    }

}
