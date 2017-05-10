using System;
using UnityEditor;
using UnityEngine;

public class EditorWindowUtil
{
    public static string SelectFileWithFilters(string title, string defaultPath, string[] filers = null)
    {
        string filePath = EditorUtility.OpenFilePanelWithFilters(title, defaultPath, filers);
        return filePath;
    }

    public static string SelectFile(string title, string defaultPath, string ext)
    {
        return EditorUtility.OpenFolderPanel(title, defaultPath, ext);
    }

    public static string SelectFolder(string title, string defaultFolder, string defaultName = "")
    {
        return EditorUtility.OpenFolderPanel(title, defaultFolder, defaultName);
    }

    public static bool DrawSelectPathView(string title, string value, string buttonName = "选择")
    {
        EditorGUILayout.LabelField(title);
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.TextField(value);
        bool select = GUILayout.Button("选择");
        GUILayout.EndHorizontal();
        return select;
    }

}
