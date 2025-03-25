using UnityEngine;
using System.Collections;
using System;

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
        [SerializeField] private GameObject _poolManager;// ObjectPoolを管理するオブジェクト
        [SerializeField] private GameObject _pixelPrefab;// 生成するキューブのプレハブ
        // 横方向の最大幅（ワールド座標上）
        [SerializeField] private float _maxLimitWidth = 10.0f;
        // 縦方向の最大高さ（ワールド座標上）
        [SerializeField] private float _maxLimitHeight = 10.0f;
        private int _width;// ピクセルアートの横幅
        private int _height;// ピクセルアートの縦幅
        [SerializeField] private float _pixeListNum;// どのピクセルリストを使用するかインスペクターで選択可能
        private int _pixelArtCompleteTime;// ピクセルアートを生成するのにかかる時間
        private float _delayTime;// キューブ生成の遅延時間
        private float _pixelSizeRate; // ピクセルのサイズ(倍率)
        private float _offsetX;     // 横方向のオフセット（中央寄せ用）
        private char[,] _pixelList;// ピクセルリスト
        private char[,] _samplePixelList1;// サンプルのピクセルリスト、Pythonと同期できるまでこれを使う
        private char[,] _samplePixelList2;
        private char[,] _samplePixelList3;
        private char[,] _samplePixelList4;
        [SerializeField] private float _debagSpeedRate;// デバッグ時のスピード倍率

        private void Awake()
        {
            Initialize();
        }

        void Start()
        {
            SampleInitialize();// サンプルのピクセルリストを初期化
            InitializePixelList(32, 32);// ピクセルリストを初期化
            SelectPixelList();// ピクセルリストを選択
            ResetCubeParam();// キューブのパラメータをリセット
            StartCoroutine(CreatePixelArtCoroutine());// キューブ生成のタイミングをずらしたバージョン
        }

        void Update()
        {
            // デバッグ用のキー入力
            // スペースキーでキューブを非アクティブにする
            if (Input.GetKeyDown(KeyCode.Space))
            {
                DeactiveAllPixels();
            }

            // エンターキーでピクセルリストを切り替え&ピクセルアートを生成
            if (Input.GetKeyDown(KeyCode.Return))
            {
                _pixeListNum = (++_pixeListNum) % 4;
                SelectPixelList();
                DeactiveAllPixels();
                StartCoroutine(CreatePixelArtCoroutine());
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
        /// ピクセルリストを初期化
        /// ピクセルリストのサイズを指定して、全ての要素を初期化する
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="defaultChar"></param>
        private void InitializePixelList(int width, int height, char defaultChar = 'w')
        {
            _pixelList = new char[height, width];
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    _pixelList[y, x] = defaultChar;
                }
            }
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

            // test用のサンプル（配列サイズ：[32,32]）
            _samplePixelList4 = new char[32, 32]
            {
                {'w','w','w','w','w','w','w','w','w','w','w','w','w','w','w','w','w','w','w','w','w','w','w','w','w','w','w','w','w','w','w','w'},
                {'w','w','w','w','w','w','w','w','w','w','w','w','w','w','w','w','w','w','w','w','w','w','w','w','w','w','w','w','w','w','w','w'},
                {'w','w','w','w','w','w','w','w','w','w','w','w','w','w','w','w','w','w','w','w','w','w','w','w','w','w','w','w','w','w','w','w'},
                {'w','w','w','w','w','w','w','w','w','w','w','w','w','w','w','w','w','w','w','w','w','w','w','w','w','w','w','w','w','w','w','w'},
                {'w','w','w','w','w','w','w','w','w','w','w','w','w','w','w','w','w','w','w','w','w','w','w','w','w','w','w','w','w','w','w','w'},
                {'w','w','w','w','w','w','w','w','w','w','w','w','w','w','w','w','w','w','w','w','w','w','w','w','w','w','w','w','w','w','w','w'},
                {'w','w','w','w','w','w','w','w','w','w','w','w','w','w','w','w','w','w','w','w','w','w','w','w','w','w','w','w','w','w','w','w'},
                {'w','w','w','w','w','w','w','w','w','w','w','w','w','w','w','w','w','w','w','w','w','w','w','w','w','w','w','w','w','w','w','w'},
                {'w','w','w','w','w','w','w','w','w','w','w','w','w','w','w','w','w','w','w','w','w','w','k','k','k','k','k','w','w','w','w','w'},
                {'w','w','w','w','w','w','w','w','w','w','w','w','w','w','w','w','w','w','w','w','k','k','o','o','o','o','o','k','w','w','w','w'},
                {'w','w','w','w','w','w','w','k','k','w','w','w','w','k','k','k','w','w','w','k','o','o','o','o','o','y','y','o','k','w','w','w'},
                {'w','w','w','w','w','w','k','o','k','w','w','w','k','o','o','o','k','k','k','k','k','k','o','o','o','o','y','y','k','w','w','w'},
                {'w','w','w','w','w','w','k','o','k','w','w','w','k','k','o','o','o','o','k','o','o','o','k','k','o','o','k','o','o','k','w','w'},
                {'w','w','w','w','w','k','o','k','w','w','w','k','o','k','k','o','o','o','k','o','p','p','o','o','k','k','w','k','o','k','w','w'},
                {'w','w','w','w','w','k','o','k','w','w','k','o','o','k','w','k','o','o','o','k','p','p','p','p','o','o','k','k','o','o','k','w'},
                {'w','w','w','w','k','o','k','k','k','k','o','o','k','w','w','w','k','o','k','k','k','p','p','p','p','p','o','o','k','o','k','w'},
                {'w','w','w','w','k','o','o','o','k','o','o','k','k','k','w','w','k','k','o','o','k','k','p','p','p','p','p','p','o','k','k','w'},
                {'w','w','w','k','o','o','o','o','o','o','k','o','o','o','k','k','o','o','o','o','k','p','k','p','p','p','p','p','p','o','k','w'},
                {'w','w','w','k','o','o','o','o','o','o','o','o','o','o','o','o','o','o','o','o','o','p','p','p','p','p','k','k','k','k','k','w'},
                {'w','w','w','k','o','o','o','o','k','o','o','k','k','o','o','o','o','o','o','o','o','o','k','o','k','k','o','o','o','o','k','w'},
                {'w','w','k','o','o','o','o','k','w','o','o','k','w','k','o','o','o','o','k','o','k','o','k','o','o','o','k','o','o','k','w','w'},
                {'w','k','o','o','o','o','k','k','w','o','k','w','w','w','k','o','o','k','w','k','o','o','k','o','o','o','k','o','o','k','w','w'},
                {'w','k','o','o','o','o','o','o','k','k','w','w','w','k','o','k','k','o','o','o','o','k','o','o','o','o','o','k','k','w','w','w'},
                {'w','w','k','o','o','o','k','k','w','w','w','w','w','k','o','o','k','w','o','o','k','o','o','o','o','o','o','k','w','w','w','w'},
                {'w','w','w','k','k','k','w','w','w','w','w','w','w','w','k','o','o','k','k','k','o','k','o','o','o','o','o','k','w','w','w','w'},
                {'w','w','w','w','w','w','w','w','w','w','w','w','w','k','o','k','k','y','y','y','k','o','o','o','o','o','k','w','w','w','w','w'},
                {'w','w','w','w','w','w','w','w','w','w','w','w','w','k','k','k','k','k','k','y','k','o','o','o','o','o','k','w','w','w','w','w'},
                {'w','w','w','w','w','w','w','w','w','w','w','w','w','w','w','w','w','w','w','k','k','k','o','o','o','k','w','w','w','w','w','w'},
                {'w','w','w','w','w','w','w','w','w','w','w','w','w','w','w','w','w','w','w','w','k','w','o','w','o','k','w','w','w','w','w','w'},
                {'w','w','w','w','w','w','w','w','w','w','w','w','w','w','w','w','w','w','w','w','w','k','k','k','k','w','w','w','w','w','w','w'},
                {'w','w','w','w','w','w','w','w','w','w','w','w','w','w','w','w','w','w','w','w','w','w','w','w','w','w','w','w','w','w','w','w'},
                {'w','w','w','w','w','w','w','w','w','w','w','w','w','w','w','w','w','w','w','w','w','w','w','w','w','w','w','w','w','w','w','w'},

            };
        }

        // ピクセルリストを選択
        private void SelectPixelList()
        {
            switch (_pixeListNum)
            {
                case 0:
                    _pixelList = _samplePixelList1;
                    break;
                case 1:
                    _pixelList = _samplePixelList2;
                    break;
                case 2:
                    _pixelList = _samplePixelList3;
                    break;
                case 3:
                    _pixelList = _samplePixelList4;
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
            float worldY = (y - (_height - 1)) * _pixelSizeRate + 18;// キューブ生成の位置を上に18に上げる
            Vector3 position = new Vector3(_offsetX + x * _pixelSizeRate, worldY, 0);
            GameObject pixel = Instantiate(_pixelPrefab, position, Quaternion.identity);
            pixel.GetComponent<Renderer>().material.color = color;
            pixel.transform.localScale = new Vector3(_pixelSizeRate, _pixelSizeRate, _pixelSizeRate);
        }

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
        /// 1ピクセルのキューブを非アクティブにする
        /// _objectPool.Get()のDeactivate()を呼び出す
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
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
                    char pixelChar = _pixelList[y, x];
                    Color color = Colors.WHITE;

                    switch (pixelChar)
                    {
                        case 'r': color = Colors.RED; break;
                        case 'g': color = Colors.GREEN; break;
                        case 'b': color = Colors.BLUE; break;
                        case 'w': color = Colors.WHITE; break;
                        case 'k': color = Colors.BLACK; break;
                        case 'o': color = Colors.ORANGE; break;
                        case 'p': color = Colors.PURPLE; break;
                        case 'l': color = Colors.LIGHT_GREEN; break;
                        case 'y': color = Colors.YELLOW; break;
                        case 'm': color = Colors.MAGENTA; break;
                        case 'c': color = Colors.CYAN; break;
                        case 'P': color = Colors.PINK; break;
                        case 'G': color = Colors.GREY; break;
                        case 'B': color = Colors.LIGHT_BLUE; break;
                    }

                    ActivatePixel(x, y, color);
                    yield return new WaitForSeconds(_delayTime);
                }
            }
        }

        private void DeactiveAllPixels()
        {
            DebugLog("DeactiveAllPixels");
            DeactivatePixel();
        }

        private void DebugLog(string message)
        {
            // Debug.Log(message);
        }
    }
}
