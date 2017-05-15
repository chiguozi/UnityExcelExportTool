using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ExcelEditorWindow : EditorWindow
{
    [MenuItem("Excel/ExcelEditor")]
    public static void Open()
    {
        GetWindowWithRect<ExcelEditorWindow>(new Rect(0, 0, 600 , 400), false, "ExcelEditor");
    }

    public static void Open(Excel excel)
    {
        ExcelEditorWindow window = GetWindowWithRect<ExcelEditorWindow>(new Rect(0, 0, 600, 400), false, "ExcelEditor");
        window._excelPath = excel.FullPath;
        window._excel = excel;
    }

    public static void Open(string excelFullpath)
    {
        ExcelEditorWindow window = GetWindowWithRect<ExcelEditorWindow>(new Rect(0, 0, 600, 400), false, "ExcelEditor");
        window._excelPath = excelFullpath;
        window.LoadFile();
    }


    string _excelPath;
    Excel _excel;
    Vector2 _position;

    void OnGUI()
    {
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Excel路径 : ", GUILayout.Width(80));
        _excelPath = EditorGUILayout.TextField(_excelPath, GUILayout.Width(400));
        bool selectPath = GUILayout.Button("选择");
        if (selectPath)
            _excelPath = EditorWindowUtil.SelectFileWithFilters("选择Excel路径", _excelPath, new string[] { "xls,xlsx", "xls,xlsx" });
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        bool load = GUILayout.Button("Load");
        bool save = GUILayout.Button("Save");
        EditorGUILayout.EndHorizontal();

        if (load)
            LoadFile();
        if (save)
            SaveFile();

        if (_excel == null || _excel.excelData == null)
            return;

        _position = EditorGUILayout.BeginScrollView(_position);
        for(int i = 0; i < _excel.excelData.count; i++)
        {
            EditorGUILayout.BeginHorizontal();
            for (int j = 0; j < _excel.excelData.GetRow(i).count; j++)
            {
                DrawCell(_excel.excelData.GetRow(i).GetCell(j));
            }
            EditorGUILayout.EndHorizontal(); 
        }

        EditorGUILayout.EndScrollView();
    }

    void DrawCell(ExcelCell cell)
    {
        string s = EditorGUILayout.TextField(cell.stringValue);
        cell.stringValue = s;
    }

    void LoadFile()
    {
        if (string.IsNullOrEmpty(_excelPath))
        {
            _excelPath = EditorWindowUtil.SelectFileWithFilters("选择Excel路径", _excelPath, new string[] { "xls,xlsx", "xls,xlsx" });
        }
        if (string.IsNullOrEmpty(_excelPath) || (!_excelPath.EndsWith(".xls") && !_excelPath.EndsWith(".xlsx") ))
        {
            Debug.LogError("请选择正确的excel文件");
            return;
        }
        _excel = new Excel(_excelPath);
        _excel.Load();
    }

    void SaveFile()
    {
        if (_excel == null || string.IsNullOrEmpty(_excelPath))
            return;
        _excel.excelData.SetAllDirty();
        _excel.Write();
    }


    //string SelectFile()
    //{
    //    string path = EditorUtility.OpenFilePanelWithFilters("选择Excel路径", _excelPath, new string[] { "xls,xlsx", "xls,xlsx"});
    //    return path;
    //}

}
