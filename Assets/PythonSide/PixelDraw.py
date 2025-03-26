# Description: ピクセルアートを描画する Python スクリプト

if __name__ == "__main__":
    # 色の定義
    RESET = "\033[0m"      # リセット
    WHITE = "\033[37m"     # 白
    BLACK = "\033[30m"     # 黒
    RED = "\033[31m"       # 赤
    BLUE = "\033[34m"      # 青
    GREEN = "\033[32m"     # 緑
    YELLOW = "\033[33m"    # 黄
    PINK = "\033[38;5;206m"  # ピンク（ANSI 256色モード）
    LIGHT_BLUE = "\033[36m"  # 水色
    LIGHT_GREEN = "\033[38;5;190m"  # 黄緑（ANSI 256色モード）
    ORANGE = "\033[38;5;214m"  # オレンジ（ANSI 256色モード）
    PURPLE = "\033[35m"    # 紫















    # 色付きの文字を表示
    a = WHITE + "■" + RESET
    b = BLACK + "■" + RESET
    c = RED + "■" + RESET
    d = BLUE + "■" + RESET
    e = GREEN + "■" + RESET
    f = YELLOW + "■" + RESET
    g = PINK + "■" + RESET
    h = LIGHT_BLUE + "■" + RESET
    i = LIGHT_GREEN + "■" + RESET
    j = ORANGE + "■" + RESET
    k = PURPLE + "■" + RESET
    
    # ピクセルアート格納用の二次元配列を定義
    list_width = 5 
    list_height = 5
    pixel_art = [[e]*list_width]*list_height

    #リスト化
    pixel_art = [   [f,f,f,f,f],
                    [f,b,f,b,f],
                    [f,b,f,b,f],
                    [g,f,f,f,g],
                    [f,b,b,b,f]
                ]

    # ピクセルアートを表示
    for i in range(len(pixel_art)):
        for j in range(len(pixel_art[i])):
            print(''.join(pixel_art[i][j]), end="")
        print()
        