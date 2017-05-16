using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IClassFactory
{
    IExcelClassGenerater Create();
}

public interface IDataFactory
{
    IExcelDataGenerater Create();
}

public class ClassFactory
{
    public static IClassFactory  GetFactory(bool server)
    {
        if (server)
            return new ExcelServerClassGeneraterFactory();
        return new ExcelClientClassGeneraterFactory();
    }
}

public class DataFactory
{
    public static IDataFactory GetFactory(bool server)
    {
        if (server)
            return new ExcelServerDataGeneraterFactory();
        return new ExcelClientDataGeneraterFactory();
    }
}

public class ExcelServerClassGeneraterFactory : IClassFactory
{
    public IExcelClassGenerater Create()
    {
        switch (ExcelExporterUtil.exportType)
        {
            case ExcelDataExportType.Text:
                return new ExcelTextClassGenerater();
        }

        return null;
    }
}

public class ExcelClientClassGeneraterFactory : IClassFactory
{
    public IExcelClassGenerater Create()
    {
        switch (ExcelExporterUtil.exportType)
        {
            case ExcelDataExportType.Text:
                return new ExcelTextClassGenerater();
            case ExcelDataExportType.ScriptObject:
                return new ExcelScriptableObjectClassGenerater();
            case ExcelDataExportType.Json:
                return new ExcelJsonClassGenerater();
            case ExcelDataExportType.Bytes:
                return new ExcelBinaryClassGenerater();
        }

        return null;
    }
}

public class ExcelServerDataGeneraterFactory : IDataFactory
{
    public IExcelDataGenerater Create()
    {
        switch (ExcelExporterUtil.exportType)
        {
            case ExcelDataExportType.Text:
                return new ExcelTextDataGenerater();
        }
        return null;
    }
}

public class ExcelClientDataGeneraterFactory : IDataFactory
{
    public IExcelDataGenerater Create()
    {
        switch (ExcelExporterUtil.exportType)
        {
            case ExcelDataExportType.Text:
                return new ExcelTextDataGenerater();
            case ExcelDataExportType.ScriptObject:
                return new ExcelScriptableObjectDataGenerater();
            case ExcelDataExportType.Json:
                return new ExcelJsonDataGenerater();
            case ExcelDataExportType.Bytes:
                return new ExcelBinaryDataGenerater();
        }
        return null;
    }
}

