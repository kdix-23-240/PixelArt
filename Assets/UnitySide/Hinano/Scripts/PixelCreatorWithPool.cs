using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

namespace Hinano
{
    public static class Colors
    {
        public static readonly Color BLACK = Color.black;
        public static readonly Color GREY = Color.grey;
        public static readonly Color RED = Color.red;
        public static readonly Color GREEN = Color.green;
        public static readonly Color YELLOW = Color.yellow;
        public static readonly Color BLUE = Color.blue;
        public static readonly Color MAGENTA = Color.magenta;
        public static readonly Color CYAN = Color.cyan;
        public static readonly Color WHITE = Color.white;

        // Unity標準にない色はカスタム定義
        public static readonly Color ORANGE = new Color(1.0f, 0.5f, 0.0f);      // オレンジ
        public static readonly Color PURPLE = new Color(0.5f, 0.0f, 0.5f);        // 紫
        public static readonly Color LIGHT_BLUE = new Color(0.68f, 0.85f, 0.9f);    // 水色（薄い青）
        public static readonly Color PINK = new Color(1.0f, 0.75f, 0.8f);           // ピンク
        public static readonly Color LIGHT_GREEN = new Color(0.56f, 0.93f, 0.56f);  // 黄緑
    }

    public class PixelCreatorWithPool : PoolManager<CubeObject>
    {
        private GameObject _poolManager;// ObjectPoolを管理するオブジェクト
        [SerializeField] private GameObject _pixelPrefab;// 生成するキューブのプレハブ
        // 横方向の最大幅（ワールド座標上）
        private float _maxLimitWidth = 10.0f;
        // 縦方向の最大高さ（ワールド座標上）
        private float _maxLimitHeight = 10.0f;
        private int _width;// ピクセルアートの横幅
        private int _height;// ピクセルアートの縦幅
        private int _pixelArtCompleteTime;// ピクセルアートを生成するのにかかる時間
        private float _delayTime;// キューブ生成の遅延時間
        private float _pixelSizeRate; // ピクセルのサイズ(倍率)
        private float _offsetX;     // 横方向のオフセット（中央寄せ用）
        private string[,] _pixelList;// Pythonから取得したピクセルリスト
        private PythonRunner _pythonRunner;// Pythonを実行するクラス
        [SerializeField] private float _debagSpeedRate;// デバッグ時のスピード倍率

        private PixelArt _pixelArt;// ピクセルアートのデータ
        private List<PixelArt> _pixelArtList;// ピクセルアートのリスト

        private void Awake()
        {
            Initialize();
        }

        void Start()
        {
            _poolManager = this.gameObject;
            _pythonRunner = new PythonRunner();
            _pixelArtList = new List<PixelArt>();
            SelectPixelList();// ピクセルリストを選択
            ResetCubeParam();// キューブのパラメータをリセット
        }

        void Update()
        {
            // デバッグ用のキー入力
            // スペースキーでキューブを非アクティブにする
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                DeactiveAllPixels();
            }
            // エンターキーでピクセルリストを切り替え&ピクセルアートを生成
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                SelectPixelList();
                DeactiveAllPixels();
                StartCoroutine(CreatePixelArtCoroutine());
            }
            if(Input.GetKeyDown(KeyCode.Alpha3))
            {
                DeactiveAllPixels();
                ShowRandomPixelArt();
            }
        }

        /// <summary>
        /// キューブ生成のパラメータをリセット
        /// キューブの大きさ、生成遅延時間の設定と中央揃えになるようにオフセットを設定
        /// </summary>
        private void ResetCubeParam()
        {
            _width = _pixelList.GetLength(1);// ピクセルアートの横幅を取得
            _height = _pixelList.GetLength(0);// ピクセルアートの縦幅を取得

            // ピクセルアートを生成するのにかかる時間
            _pixelArtCompleteTime = 8 * (int)Math.Log(Math.Sqrt(_width * _height) * (1 / _debagSpeedRate), 2);
            _delayTime = (float)_pixelArtCompleteTime / (_width * _height);// キューブ生成の遅延時間を計算

            // ピクセルアートの横幅の最大値
            float hScale = _maxLimitWidth / _width;
            // ピクセルアートの縦幅の最大値
            float vScale = _maxLimitHeight / _height;
            // もしピクセルアートの横幅が設定値を超えるなら、縦幅の最大値を使う
            if (_height * hScale > _maxLimitHeight)
            {
                _pixelSizeRate = vScale;
            }
            else
            {
                _pixelSizeRate = hScale;
            }
            // 横幅は、生成するキューブ全体が自分のサイズ（_width * usedScale）分になるので、それに合わせて中央寄せにする
            _offsetX = -(_width * _pixelSizeRate) / 2 + _pixelSizeRate / 2;
        }

        /// <summary>
        /// ピクセルアートを生成する
        /// キューブの生成にはCreatePixelArtCoroutineメソッドを使用
        /// </summary>
        private IEnumerator CreatePixelArtCoroutine()
        {
            ResetCubeParam();// 初期化
            // 下の行（配列の最後）から上の行へ逆順に生成
            for (int y = _height - 1; y >= 0; y--)
            {
                for (int x = 0; x < _width; x++)
                {
                    Color color;
                    string pixelString = _pixelList[y, x];
                    color = Colors.WHITE;

                    switch (pixelString)
                    {
                        case "[31m???": color = Colors.RED; break;
                        case "[32m???": color = Colors.GREEN; break;
                        case "[34m???": color = Colors.BLUE; break;
                        case "[37m???": color = Colors.WHITE; break;
                        case "[30m???": color = Colors.BLACK; break;
                        case "[38;5;214m???": color = Colors.ORANGE; break;
                        case "[35m???": color = Colors.PURPLE; break;
                        case "[38;5;190m???": color = Colors.LIGHT_GREEN; break;
                        case "[33m???": color = Colors.YELLOW; break;
                        case "[38;5;206m???": color = Colors.PINK; break;
                        case "[36m???": color = Colors.LIGHT_BLUE; break;
                    }
                    ActivatePixel(x, y, color);
                    yield return new WaitForSeconds(_delayTime);
                }
            }
        }

        /// <summary>
        /// ピクセルアートをリストに登録
        /// </summary>
        /// <param name="pixelList"></param>
        private void RegisterPixelArtList(string[,] pixelList)
        {
            _pixelArt = new PixelArt(pixelList);
            _pixelArtList.Add(_pixelArt);
        }

        /// <summary>
        /// サンプルのピクセルリストを初期化
        /// Pythonで制作したピクセルアートのリストを使用
        /// </summary>
        private void SelectPixelList()
        {
            _pixelList = _pythonRunner.Run();
            RegisterPixelArtList(_pixelList);
        }

        /// <summary>
        /// 指定された座標にピクセルをアクティブにする
        /// </summary>
        /// <param name="x">生成x座標</param>
        /// <param name="y">生成y座標</param>
        /// <param name="color">設定する色</param>
        private void ActivatePixel(int x, int y, Color color)
        {
            DebugLog("ActivatePixel");
            var pixel = _objectPool.Get();
            if (pixel == null)
            {
                return;
            }
            pixel.Activate(x, y);
            float worldY = (y - (_height - 1)) * _pixelSizeRate + 20;// キューブ生成の位置を上に20に上げる
            Vector3 position = new Vector3(_offsetX + x * _pixelSizeRate, worldY, 0);
            pixel.transform.position = position;
            pixel.GetComponent<Renderer>().material.color = color;
            pixel.transform.localScale = new Vector3(_pixelSizeRate, _pixelSizeRate, _pixelSizeRate);
        }

        /// <summary>
        /// ピクセルを非アクティブにする
        /// </summary>
        private void DeactivatePixel()
        {
            DebugLog("DeactiveAllPixels");
            // PixelArtCreatorオブジェクトの子要素をすべて取得
            foreach (Transform child in transform)
            {
                // 子要素が CubeObject を持っている場合、Deactivate を呼び出す
                var pixel = child.GetComponent<CubeObject>();
                if (pixel != null)
                {
                    pixel.Deactivate();
                }
            }
        }

        /// <summary>
        /// 全てのピクセルを非アクティブにする
        /// </summary>
        private void DeactiveAllPixels()
        {
            DebugLog("DeactiveAllPixels");
            DeactivatePixel();
        }

    /// <summary>
    /// 指定されたインデックスのピクセルアートを表示
    /// </summary>
    /// <param name="index"></param>
        private void ShowDesignetedPixelArt(int index)
        {
            if (index < 0 || index >= _pixelArtList.Count)
            {
                Debug.LogWarning("指定されたインデックスが範囲外です");
                return;
            }
            _pixelList = _pixelArtList[index].Data;
            StartCoroutine(CreatePixelArtCoroutine());
        }

        /// <summary>
        /// ランダムなピクセルアートを表示
        /// </summary>
        private void ShowRandomPixelArt()
        {
            if(_pixelArtList.Count <= 1)
            {
                Debug.LogWarning("ピクセルアートが登録されていません");
                return;
            }
            int index = UnityEngine.Random.Range(1, _pixelArtList.Count);
            ShowDesignetedPixelArt(index);
        }

        private void DebugLog(string message)
        {
            Debug.Log(message);
        }
    }
}
