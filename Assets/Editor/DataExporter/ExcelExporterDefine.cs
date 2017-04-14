using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ExcelDataExportType
{
    Text,
    Bytes,
    Json,
    ScriptObject,
}

public class ExcelExporterDefine
{
    public static ExcelDataExportType exportType = ExcelDataExportType.Text;

    public static string ClientClassExt = ".cs";

    public static string ClientClassPre = "Cfg";

    public static string ConfigFactoryName = "ConfigFactory";

}
