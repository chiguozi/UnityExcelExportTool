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
                TryParseInt(value, out intOutput);
                return intOutput;
            case "float":
                float floatOutput;
                TryParseFloat(value, out floatOutput);
                return floatOutput;
            case "List<int>":
                List<int> valueList;
                TryParseListInt(value, out valueList);
                return valueList;
            case "List<string>":
                List<string> stringValueList;
                TryParseListString(value, out stringValueList);
                return stringValueList;
            case "Dictionary<int, int>":
                Dictionary<int, int> intDic;
                TryParseDicIntInt(value, out intDic);
                return intDic;
            case "Dictionary<int, string>":
                Dictionary<int, string> stringIntDic;
                TryParseDicIntString(value, out stringIntDic);
                return stringIntDic;
            case "des":
                string des;
                TryParseDes(value, out des);
                return des;
            case "Vector3":
                Vector3 vec3;
                TryParseVector3(value, out vec3);
                return vec3;
            case "Vector2":
                Vector2 vec2;
                TryParseVector2(value, out vec2);
                return vec2;
            case "List<float>":
                List<float> floatList;
                TryParseListFloat(value, out floatList);
                return floatList;
        }
        return value;
    }

    public static bool TryParseInt(string str, out int value)
    {
        value = 0;
        if (string.IsNullOrEmpty(str))
            return true;
        if (int.TryParse(str, out value))
            return true;
        return false;
    }

    public static bool TryParseFloat(string str, out float value)
    {
        value = 0;
        if (string.IsNullOrEmpty(str))
            return true;
        if (float.TryParse(str, out value))
            return true;
        return false;
    }

    public static bool TryParseDes(string str, out string value)
    {
        value = str.Replace("|", "\n");
        return true;
    }

    //1，2，3，4
    public static bool TryParseListInt(string str, out List<int> valueList)
    {
        valueList = new List<int>();
        if (string.IsNullOrEmpty(str))
            return true;
        string[] values = str.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
        bool success = true;
        for (int i = 0; i < values.Length; i++)
        {
            int value;
            if (!TryParseInt(values[i], out value))
                success = false;
            valueList.Add(value);
        }
        return success;
    }

    public static bool TryParseListFloat(string str, out List<float> valueList)
    {
        valueList = new List<float>();
        if (string.IsNullOrEmpty(str))
            return true;
        string[] values = str.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
        bool success = true;
        for (int i = 0; i < values.Length; i++)
        {
            float value;
            if (!TryParseFloat(values[i], out value))
                success = false;
            valueList.Add(value);
        }
        return success;
    }

    public static bool TryParseListString(string str, out List<string> valueList)
    {
        valueList = new List<string>();
        if (string.IsNullOrEmpty(str))
            return true;
        string[] values = str.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
        bool success = true;
        for (int i = 0; i < values.Length; i++)
        {
            valueList.Add(values[i]);
        }
        return success;
    }
    //(1,2),(2,3)
    public static bool TryParseDicIntInt(string str, out Dictionary<int, int> valueDic)
    {
        valueDic = new Dictionary<int, int>();
        if (string.IsNullOrEmpty(str))
            return true;
        str = str.TrimStart('(').TrimEnd(')');

        string[] values = str.Split(new string[] { "),(" }, StringSplitOptions.RemoveEmptyEntries);
      
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
            if (!TryParseInt(nums[0], out key) )
            {
                success = false;
            }
            if(!TryParseInt(nums[1], out value))
            {
                success = false;
            }
            valueDic.Add(key, value);
        }
        return success;
    }

    public static bool TryParseDicIntString(string str, out Dictionary<int, string> valueDic)
    {
        valueDic = new Dictionary<int, string>();
        if (string.IsNullOrEmpty(str))
            return true;
        str = str.TrimStart('(').TrimEnd(')');

        string[] values = str.Split(new string[] { "),(" }, StringSplitOptions.RemoveEmptyEntries);
        
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
            if (!TryParseInt(nums[0], out key))
            {
                success = false;
            }
            valueDic.Add(key, nums[1]);
        }
        return success;
    }

    public static bool TryParseVector3(string str, out Vector3 vec)
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
        if (!TryParseFloat(values[0], out x))
            success = false;
        if (!TryParseFloat(values[1], out y))
            success = false;
        if (!TryParseFloat(values[2], out z))
            success = false;
        vec = new Vector3(x, y, z);
        return success;
    }

    public static  bool TryParseVector2(string str, out Vector2 vec)
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
        if (!TryParseFloat(values[0], out x))
            success = false;
        if (!TryParseFloat(values[1], out y))
            success = false;
        vec = new Vector3(x, y);
        return success;
    }

    //(1,2,3,4),(1,2,3,4)
    public static bool TryParseListListString(string str, out List<List<string>> value)
    {
        value = new List<List<string>>();
        if (string.IsNullOrEmpty(str))
            return true;
        str = str.TrimStart('(').TrimEnd(')');
        string[] listStr = str.Split(new string[] { "),(" }, StringSplitOptions.RemoveEmptyEntries);
        for (int i = 0; i < listStr.Length; i++)
        {
            string[] values = listStr[i].Split(',');
            value.Add(new List<string>(values));
        }
        return true;
    }

}
