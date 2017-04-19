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
    [MenuItem("Tools/write")]
    static void Write()
    {
        Excel excel = new Excel(Application.dataPath + "/Test1.xls");
        excel.Load();
        excel.excelData.SetAllDirty();
        excel.excelData.SetCellValue(2, 2, 10, typeof(string));
        NPOIWriter writer = new NPOIWriter();
        writer.Write(Application.dataPath + "/Test2.xls", excel.excelData);
        AssetDatabase.Refresh();
    }

    private static void TestExcel(string path)
    {
        Excel excel = new Excel(path);
        excel.Load();
        ExcelClassGenerater generater = new ExcelClassGenerater();
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

