using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;

public class ExcelScriptableObjectClassGenerater
{
    public void GenerateClass(string savePath, string className, ExcelGameData data)
    {
        if (!Directory.Exists(savePath))
            Directory.CreateDirectory(savePath);

        string fileName = className + ExcelExporterUtil.ClientClassExt;
        List<string> types = data.fieldTypeList;
        List<string> fields = data.fieldNameList;

        StringBuilder sb = new StringBuilder();
    }
}
