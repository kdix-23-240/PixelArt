using UnityEngine;

public class PixelCreator : MonoBehaviour
{
    [SerializeField] private GameObject _pixelPrefab;
    private int _width;// ピクセルアートの横幅
    private int _height;// ピクセルアートの縦幅
    [SerializeField] private float _pixeListNum;// どのピクセルリストを使用するかインスペクターで選択可能
    private float _pixelSizeRate; // ピクセルのサイズ(倍率)
    private char[,] _pixelList;// ピクセルリスト
    private char[,] _samplePixelList1;// サンプルのピクセルリスト、Pythonと同期できるまでこれを使う
    private char[,] _samplePixelList2;
    private char[,] _samplePixelList3;

    void Start()
    {
        SampleInitialize();// サンプルのピクセルリストを初期化
        SelectPixelList();// ピクセルリストを選択
        _width = _pixelList.GetLength(1);// ピクセルアートの横幅を取得
        _height = _pixelList.GetLength(0);// ピクセルアートの縦幅を取得

        _pixelSizeRate = 1.0f;// ピクセルの倍率を仮に1.0fで設定 <--- ここを変更するとピクセルのサイズが変わる

        CreatePixel(0, 0, Color.red);// ピクセルを生成 <--- 見本、メソッドの使い方がわかったらこの行を消す
        CreatePixel(1, 0, Color.black);// ピクセルを生成 <--- 見本、メソッドの使い方がわかったらこの行を消す
        CreatePixel(2, 0, Color.blue);// ピクセルを生成 <--- 見本、メソッドの使い方がわかったらこの行を消す
        CreatePixel(3, 0, Color.red);// ピクセルを生成 <--- 見本、メソッドの使い方がわかったらこの行を消す
        CreatePixel(4, 0, Color.white);// ピクセルを生成 <--- 見本、メソッドの使い方がわかったらこの行を消す
        CreatePixel(1, 1, Color.green);// ピクセルを生成 <--- 見本、メソッドの使い方がわかったらこの行を消す

        // CreatePixelArt();// ピクセルアートを生成 <---  最終的にはここのコメントアウトを外す
    }

    private void SampleInitialize()
    {
        // イタリアの国旗をイメージ
        _samplePixelList1 = new char[8, 12]
        {
            {'r','r','r','r','w','w','w','w','g','g','g','g'},
            {'r','r','r','r','w','w','w','w','g','g','g','g'},
            {'r','r','r','r','w','w','w','w','g','g','g','g'},
            {'r','r','r','r','w','w','w','w','g','g','g','g'},
            {'r','r','r','r','w','w','w','w','g','g','g','g'},
            {'r','r','r','r','w','w','w','w','g','g','g','g'},
            {'r','r','r','r','w','w','w','w','g','g','g','g'},
            {'r','r','r','r','w','w','w','w','g','g','g','g'},
        };

        // 日本の国旗をイメージ
        _samplePixelList2 = new char[8, 12]
        {
            {'w','w','w','w','w','w','w','w','w','w','w','w'},
            {'w','w','w','w','w','r','r','w','w','w','w','w'},
            {'w','w','w','w','r','r','r','r','w','w','w','w'},
            {'w','w','w','r','r','r','r','r','r','w','w','w'},
            {'w','w','w','r','r','r','r','r','r','w','w','w'},
            {'w','w','w','w','r','r','r','r','w','w','w','w'},
            {'w','w','w','w','w','r','r','w','w','w','w','w'},
            {'w','w','w','w','w','w','w','w','w','w','w','w'},
        };

        // スイスの国旗をイメージ
        _samplePixelList3 = new char[8, 8]
        {
            {'w','w','w','r','r','w','w','w'},
            {'w','w','w','r','r','w','w','w'},
            {'w','w','w','r','r','w','w','w'},
            {'r','r','r','r','r','r','r','r'},
            {'r','r','r','r','r','r','r','r'},
            {'w','w','w','r','r','w','w','w'},
            {'w','w','w','r','r','w','w','w'},
            {'w','w','w','r','r','w','w','w'},
        };
    }

    // ピクセルリストを選択
    private void SelectPixelList()
    {
        switch (_pixeListNum)
        {
            case 1:
                _pixelList = _samplePixelList1;
                break;
            case 2:
                _pixelList = _samplePixelList2;
                break;
            case 3:
                _pixelList = _samplePixelList3;
                break;
            default:
                _pixelList = _samplePixelList1;
                break;
        }
    }

    /// <summary>
    /// 1ピクセルのキューブを生成する
    /// </summary>
    /// <param name="x">x座標</param>
    /// <param name="y">y座標</param>
    /// <param name="color">色</param>
    private void CreatePixel(int x, int y, Color color)
    {
        GameObject pixel = Instantiate(_pixelPrefab, new Vector3(x, y + 8, 0), Quaternion.identity);
        pixel.GetComponent<Renderer>().material.color = color;
        pixel.transform.localScale = new Vector3(_pixelSizeRate, _pixelSizeRate, _pixelSizeRate);
    }

    /// <summary>
    /// ピクセルアートを生成する
    /// キューブの生成にはCreatePixelメソッドを使用
    /// </summary>
    private void CreatePixelArt()
    {

    }
}