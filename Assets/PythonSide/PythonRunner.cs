using System.Diagnostics;
using System.IO;
using UnityEngine;

// このスクリプトを実行すると、Pythonのスクリプトが実行される
public class PythonRunner
{
    private string[,] _colorList;

    public string[,] Run()
    {
        ProcessStartInfo psi = new ProcessStartInfo()
        {
            FileName = "python3",
            Arguments = Application.dataPath + "/PythonSide/PixelDraw.py",
            UseShellExecute = false,       // シェル機能を使用しない
            RedirectStandardOutput = true, // 標準出力をリダイレクト
            RedirectStandardError = true,  // 標準エラー出力をリダイレクト
            CreateNoWindow = true          // コンソール・ウィンドウを開かない
        };

        using(Process process = Process.Start(psi))
        {
            using(StreamReader reader = process.StandardOutput)
            {
                string result = reader.ReadToEnd();
                string[] lines = result.Split('\n');
                lines = lines[0..^1]; // 最後の要素は空のため削除
                string[] numCheck = lines[0].Split("[0m");
                _colorList = new string[lines.Length, numCheck.Length - 1];

                for(int i = 0; i < lines.Length; i++)
                {
                    string[] colors = lines[i].Split("[0m");
                    colors = colors[0..^1]; // 最後の要素は空のため削除
                    //UnityEngine.Debug.Log(colors[0]);
                    for(int j = 0; j < colors.Length; j++)
                    {
                        _colorList[i, j] = colors[j];
                        //UnityEngine.Debug.Log(colors[j]);
                    }
                }
            }
            process.WaitForExit();  // プロセスの終了を待つ
        }
        return _colorList;
    }
}