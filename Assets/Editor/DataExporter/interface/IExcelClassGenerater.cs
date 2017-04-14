using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IExcelClassGenerater 
{
    void GenerateClientClass(string savePath, string className, ExcelGameData data);
    void GenerateServerClass(string savePath, string className, ExcelGameData data);
}
