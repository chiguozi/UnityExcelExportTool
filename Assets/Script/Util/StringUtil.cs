using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Text;

public class StringUtil
{
    const char VectorStart = '(';
    const char VectorEnd = ')';
    const char CollectionsStart = '{';
    const char CollectionsEnd = '}';

    static string RemoveCollectionsChars(string str)
    {
        //暂时不关心开头和结尾大括号的个数
        str = str.TrimStart(CollectionsStart);
        str = str.TrimEnd(CollectionsEnd);
        return str;
    }        

    static string RemoveVectorChars(string str)
    {
        str = str.TrimStart(VectorStart);
        str = str.TrimEnd(VectorEnd);
        return str;
    }

    static List<string> SplitString(string str)
    {
        List<string> list = new List<string>();
        Stack<char> stack = new Stack<char>();
        StringBuilder sb = new StringBuilder();
        for(int i = 0; i < str.Length; i++)
        {
            if(str[i] == VectorStart)
            {
                stack.Push(str[i]);
                sb.Append(str[i]);
            }
            else if (str[i] == VectorEnd)
            {
                stack.Pop();
                sb.Append(str[i]);
            }
            else if(stack.Count == 0 && str[i] == ',' )
            {
                list.Add(sb.ToString());
                ClearStringBuilder(sb);
            }
            else
            {
                sb.Append(str[i]);
            }
        }
        if(sb.Length > 0)
        {
            list.Add(sb.ToString());
        }
        return list;
    }

    static void ClearStringBuilder(StringBuilder sb)
    {
        sb.Length = 0;
        sb.Capacity = 0;
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

    //{1，2，3，4}
    public static bool TryParseListInt(string str, out List<int> valueList)
    {
        valueList = new List<int>();
        if (string.IsNullOrEmpty(str))
            return true;
        str = RemoveCollectionsChars(str);
        List<string> values = SplitString(str);
        //string[] values = str.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
        bool success = true;
        for (int i = 0; i < values.Count; i++)
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
        str = RemoveCollectionsChars(str);
        //string[] values = str.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
        List<string> values = SplitString(str);
        bool success = true;
        for (int i = 0; i < values.Count; i++)
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
        str = RemoveCollectionsChars(str);
        //string[] values = str.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
        List<string> values = SplitString(str);
        bool success = true;
        for (int i = 0; i < values.Count; i++)
        {
            valueList.Add(values[i]);
        }
        return success;
    }
    //{1,2}, {2,3}
    public static bool TryParseDicIntInt(string str, out Dictionary<int, int> valueDic)
    {
        valueDic = new Dictionary<int, int>();
        if (string.IsNullOrEmpty(str))
            return true;
        str = RemoveCollectionsChars(str);

        string[] values = str.Split(new string[] { "},{" }, StringSplitOptions.RemoveEmptyEntries);
      
        bool success = true;
        for(int i = 0; i < values.Length; i++)
        {
            List<string> nums = SplitString(values[i]);
            if (nums.Count != 2)
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

    //(key, value),(key, value)
    public static bool TryParseDicIntString(string str, out Dictionary<int, string> valueDic)
    {
        valueDic = new Dictionary<int, string>();
        if (string.IsNullOrEmpty(str))
            return true;
        str = RemoveCollectionsChars(str);

        string[] values = str.Split(new string[] { "},{" }, StringSplitOptions.RemoveEmptyEntries);
        
        bool success = true;
        for (int i = 0; i < values.Length; i++)
        {
            List<string> nums = SplitString(values[i]);
            if (nums.Count != 2)
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
        str = RemoveVectorChars(str);
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
        str = RemoveVectorChars(str);
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
        str = RemoveCollectionsChars(str);
        string[] listStr = str.Split(new string[] { "},{" }, StringSplitOptions.RemoveEmptyEntries);
        for (int i = 0; i < listStr.Length; i++)
        {
            List<string> values = SplitString(listStr[i]);
            value.Add(values);
        }
        return true;
    }

}
