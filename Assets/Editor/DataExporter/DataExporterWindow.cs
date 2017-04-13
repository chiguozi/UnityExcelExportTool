using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class DataExporterWindow : EditorWindow
{
    static string XLSPATH = "E:/Test1.xls";
    static string XLSXPATH = "E:/Test.xlsx";
    [MenuItem("Tools/xls")]
    static void TestXLS()
    {
        Excel excel = new Excel(XLSPATH);
        excel.Load();
    }

    [MenuItem("Tools/xlsx")]
    static void TestXLSX()
    {
        Excel excel = new Excel(XLSXPATH);
        excel.Load();
    }
}

