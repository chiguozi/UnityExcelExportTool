using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class DataExporterWindow : EditorWindow
{
    static string XLSPATH = Application.dataPath +"/Test1.xls";
    static string XLSXPATH = Application.dataPath + "/Test.xlsx";
    [MenuItem("Tools/xls")]
    static void TestXLS()
    {
        TestExcel(XLSPATH);
    }

    private static void TestExcel(string path)
    {
        Excel excel = new Excel(path);
        excel.Load();
        ExcelClassGenerater generater = new ExcelClassGenerater();
        Debug.LogError(ExcelExporterUtil.GetClientClassOutputPath());
        Debug.LogError(ExcelExporterUtil.GetClientDataOutputPath());
        generater.GenerateClientClass(ExcelExporterUtil.GetClientClassOutputPath(), ExcelExporterUtil.GetClientClassFileName(excel.fileName), excel.clientData);
        ExcelDataGenerater dataGenerater = new ExcelDataGenerater();
        dataGenerater.GenerateData(ExcelExporterUtil.GetClientDataOutputPath(), ExcelExporterUtil.GetDataFileFullName(excel.fileName), excel.clientData);
        generater.GenerateClientClassFactory(ExcelExporterUtil.GetClientDataOutputPath(), ExcelExporterUtil.GetClientClassOutputPath());
        AssetDatabase.Refresh();
    }

    [MenuItem("Tools/xlsx")]
    static void TestXLSX()
    {
        TestExcel(XLSXPATH);
    }
}

