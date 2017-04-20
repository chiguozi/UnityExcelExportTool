using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IExcelClassGenerater 
{
    void GenerateClass(string savePath, string className, ExcelGameData data);
}
