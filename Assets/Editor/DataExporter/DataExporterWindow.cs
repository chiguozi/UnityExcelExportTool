using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Text;
using UnityEditor.Callbacks;
using System.IO;

public class DataExporterWindow : EditorWindow
{ 
    [MenuItem("Excel/ExcelExportWindow")]
    static void Open()
    {
        _window = GetWindow<DataExporterWindow>();//WithRect<DataExporterWindow>(new Rect(0, 0, 400, 600));
        _window.ReadConfig();
    }

    #region 本地配置文件
    void WriteConfig()
    {
        WriteStringField("_assetPath");
        WriteStringField("_excelPath");
        WriteStringField("_clientDataOutputPath");
        WriteStringField("_clientScriptOutputPath");
        PlayerPrefs.SetInt("_exportType", (int)_exportType);
    }

    void ReadConfig()
    {
        ReadStringConfig("_assetPath");
        ReadStringConfig("_excelPath");
        ReadStringConfig("_clientDataOutputPath");
        ReadStringConfig("_clientScriptOutputPath");
        if (PlayerPrefs.HasKey("_exportType"))
        {
            _exportType = (ExcelDataExportType)PlayerPrefs.GetInt("_exportType");
            ExcelExporterUtil.exportType = _exportType;
        }
    }

    void ReadStringConfig(string fieldName)
    {
        if (PlayerPrefs.HasKey(fieldName))
            this.GetType()
                .GetField(fieldName, System.Reflection.BindingFlags.SetField | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic)
                .SetValue(this, PlayerPrefs.GetString(fieldName));
    }

    void WriteStringField(string fieldName)
    {
        var fieldInfo = this.GetType().GetField(fieldName, System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.GetField | System.Reflection.BindingFlags.Instance);
        string value = fieldInfo.GetValue(this).ToString();
        if (!string.IsNullOrEmpty(value))
            PlayerPrefs.SetString(fieldName, value);
    }

    //静态变量不能使用SerializeField序列化
    void RewritePathsToExcelUtil()
    {
        ExcelExporterUtil.ExcelPath = _excelPath;
        ExcelExporterUtil.AssetPath = _assetPath;
        ExcelExporterUtil.ClientDataOutputPath = _clientDataOutputPath;
        ExcelExporterUtil.ClientScriptOutputPath = _clientScriptOutputPath;
    }
    #endregion
    [DidReloadScripts]
    static void OnDllRebuild()
    {
    }

    private void OnEnable()
    {
    }

    private void OnDestroy()
    {
    }

    static DataExporterWindow _window;

    [SerializeField]
    private string _excelPath;
    [SerializeField]
    string _assetPath;
    [SerializeField]
    string _clientScriptOutputPath;
    [SerializeField]
    string _clientDataOutputPath;
    [SerializeField]
    ExcelDataExportType _exportType = ExcelDataExportType.Text;
    [SerializeField]
    List<string> _selectFiles = new List<string>();
    [SerializeField]
    string _selectFilesText = "";

    void CheckPaths()
    {
        if (_excelPath != "" && _excelPath != ExcelExporterUtil.ExcelPath)
            RewritePathsToExcelUtil();
    }

    private void OnGUI()
    {
        CheckPaths();

        #region GUI
        bool selectExcelPath = EditorWindowUtil.DrawSelectPathView("excel路径：", _excelPath);
        bool selectAssetPath = EditorWindowUtil.DrawSelectPathView("Asset路径：", _assetPath);
        bool selectClientScriptPath = EditorWindowUtil.DrawSelectPathView("客户端类输出路径：", _clientScriptOutputPath);
        bool selectClientDataPath = EditorWindowUtil.DrawSelectPathView("客户端数据输出路径：", _clientDataOutputPath);

        var type = (ExcelDataExportType)EditorGUILayout.EnumPopup("文件导出类型", _exportType);
        if(type != ExcelExporterUtil.exportType)
        {
            _exportType = type;
            ExcelExporterUtil.exportType = type;
            PlayerPrefs.SetInt("_exportType", (int)_exportType);
        }


        Rect rect = EditorGUILayout.GetControlRect(true, GUILayout.Height(100));
        GUI.Box(rect, "选择Excel文件");

        bool genData = GUILayout.Button("生成配置配置文件");
        bool genClass = GUILayout.Button("生成客户端类文件");
        bool cleanClient = GUILayout.Button("清理客户端类和数据");

        if (_selectFiles.Count > 0)
        {
            EditorGUILayout.LabelField("选中文件：");
            EditorGUILayout.TextArea(_selectFilesText);
        }
        #endregion

        DealDrag(rect);

        if (genClass)
        {
            GenerateSelectedScript();
        }
        if (genData)
        {
            if(EditorApplication.isCompiling) 
            {
                ShowNotification(new GUIContent("正在编译，请等待编译完成"));
                return;
            }
            GenerateSelectedData();
        }

        if(cleanClient)
        {
            CleanClient();
        }

        #region RefreshPaths
        if (selectExcelPath)
        {
            _excelPath = EditorWindowUtil.SelectFolder(_excelPath, "选择excel路径", "");
            ExcelExporterUtil.ExcelPath = _excelPath;
            WriteStringField("_excelPath");
        }

        if (selectAssetPath)
        {
            _assetPath = EditorWindowUtil.SelectFolder(_assetPath, "选择工程Assets路径", "");
            ExcelExporterUtil.AssetPath = _assetPath;
            WriteStringField("_assetPath");
        }

        if (selectClientDataPath)
        {
            _clientDataOutputPath = EditorWindowUtil.SelectFolder(_clientDataOutputPath, "选择客户端数据输出路径", "");
            ExcelExporterUtil.ClientDataOutputPath = _clientDataOutputPath;
            WriteStringField("_clientDataOutputPath");
        }

        if (selectClientScriptPath)
        {
            _clientScriptOutputPath = EditorWindowUtil.SelectFolder(_clientScriptOutputPath, "选择客户端类输出路径", "");
            ExcelExporterUtil.ClientScriptOutputPath = _clientScriptOutputPath;
            WriteStringField("_clientScriptOutputPath");
        }
        #endregion
    }

    void CleanClient()
    {
        var files = Directory.GetFiles(ExcelExporterUtil.GetClientClassOutputPath());
        for(int i = 0; i < files.Length; i++)
        {
            var fileName = Path.GetFileName(files[i]);
            if(fileName.StartsWith("Cfg") && fileName.EndsWith(".cs"))
                File.Delete(files[i]);
        }

        files = Directory.GetFiles(ExcelExporterUtil.GetClientDataOutputPath());
        for (int i = 0; i < files.Length; i++)
        {
            var fileName = Path.GetFileName(files[i]);
            if(fileName.EndsWith(".bytes"))
                File.Delete(files[i]);
        }
        ExcelTextClassGenerater.GenerateClientClassFactory(ExcelExporterUtil.GetClientDataOutputPath(), ExcelExporterUtil.GetClientClassOutputPath() + "Base/", true);

        AssetDatabase.Refresh();
    }

    void GenerateSelectedScript()
    {
        if(_selectFiles.Count == 0)
        {
            Debug.LogError("没有选中excel");
            return;
        }
        for(int i = 0; i < _selectFiles.Count; i++)
        {
            GenSingleClientScript(_selectFiles[i]);
        }
        AssetDatabase.Refresh();
    }

    void GenerateSelectedData()
    {
        if (_selectFiles.Count == 0)
        {
            Debug.LogError("没有选中excel");
            return;
        }
        for (int i = 0; i < _selectFiles.Count; i++)
        {
            GenSingleClientData(_selectFiles[i]);
        }

        if(ExcelExporterUtil.exportType == ExcelDataExportType.Text)
        {
            //必须有这个文件
            ExcelTextClassGenerater.GenerateClientClassFactory(ExcelExporterUtil.GetClientDataOutputPath() , ExcelExporterUtil.GetClientClassOutputPath() + "Base/", false);
        }
        AssetDatabase.Refresh();
    }

    void GenSingleClientScript(string path)
    {
        Excel excel = new Excel(path);
        excel.Load();
        excel.GenerateClientScript();
    }

    void GenSingleClientData(string path)
    {
        Excel excel = new Excel(path);
        excel.Load();
        excel.GenerateClientData();
    }

    private void DealDrag(Rect rect)
    {
        if (( Event.current.type == EventType.DragUpdated
            || Event.current.type == EventType.DragExited )
            && rect.Contains(Event.current.mousePosition))
        {
            DragAndDrop.visualMode = DragAndDropVisualMode.Generic;
            if (DragAndDrop.paths != null && DragAndDrop.paths.Length > 0)
            {
                _selectFiles = new List<string>(DragAndDrop.paths);
                RefreshSelectFileText();
            }
        }
    }

    void RefreshSelectFileText()
    {
        for (int i = _selectFiles.Count - 1; i >= 0; i--)
        {
            if (!_selectFiles[i].EndsWith(".xls") && !_selectFiles[i].EndsWith(".xlsx"))
                _selectFiles.RemoveAt(i);
        }

        StringBuilder sb = new StringBuilder();
        for(int i = 0; i < _selectFiles.Count; i++)
        {
            sb.AppendLine(_selectFiles[i]);
        }
        _selectFilesText = sb.ToString();
    }
}

