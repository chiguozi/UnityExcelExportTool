using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IExcelWriter
{
    void Write(string fullPath, ExcelData data);
}
