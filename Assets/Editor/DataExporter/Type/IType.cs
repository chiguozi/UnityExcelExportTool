using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

//每个类型对应一个类便于特殊类型检测
//使用统一Info的方式也可以，各类特殊检测会比较麻烦
public interface IType
{
    string lowerName { get; }
    string realName { get; }
    Type type { get; }
    string parseFuncName { get; }
    bool isUnityType { get; }
    string jsonAttributeStr { get; }

    object GetValue(string content);

    bool CheckValue(string content);
}
