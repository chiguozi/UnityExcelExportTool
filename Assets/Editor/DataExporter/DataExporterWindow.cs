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
        Excel excel = new Excel(XLSPATH);
        excel.Load();
        ExcelClassGenerater generater = new ExcelClassGenerater();
        generater.GenerateClientClass(Application.dataPath + "/Script/ConfigText/", "Cfg" + excel.fileName, excel.clientData);
        ExcelDataGenerater dataGenerater = new ExcelDataGenerater();
        dataGenerater.GenerateData(Application.dataPath + "/Resources/ConfigText/", excel.fileName + ".bytes", excel.clientData);
        generater.GenerateClientClassFactory(Application.dataPath + "/Resources/ConfigText/", Application.dataPath + "/Script/ConfigText/");
        AssetDatabase.Refresh();
    }

    [MenuItem("Tools/xlsx")]
    static void TestXLSX()
    {
        Excel excel = new Excel(XLSXPATH);
        excel.Load();
        ExcelClassGenerater generater = new ExcelClassGenerater();
        generater.GenerateClientClass(Application.dataPath + "/Script/ConfigText/", "Cfg" + excel.fileName, excel.clientData);
        ExcelDataGenerater dataGenerater = new ExcelDataGenerater();
        dataGenerater.GenerateData(Application.dataPath + "/Resources/ConfigText/", excel.fileName + ".bytes", excel.clientData);
        generater.GenerateClientClassFactory(Application.dataPath + "/Resources/ConfigText/", Application.dataPath + "/Script/ConfigText/");
        AssetDatabase.Refresh();
    }
}

