using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using System.IO;

public class ExcelTextDataGenerater : IExcelDataGenerater
{
    //fileName 带后缀名
    public void GenerateData(string savePath, string fileName, ExcelGameData data, string className)
    {
        if (!Directory.Exists(savePath))
            Directory.CreateDirectory(savePath);
        StringBuilder sb = new StringBuilder();
        for(int row = 0; row < data.cellList.Count; row++)
        {
            for(int column = 0; column < data.cellList[row].Count; column++)
            {
                sb.Append(data.cellList[row][column].stringValue);
                if (column != data.cellList[row].Count - 1)
                    sb.Append("\t");
            }
            sb.Append("\n");
        }

        File.WriteAllText(Path.Combine(savePath, fileName), sb.ToString());
    }
}
