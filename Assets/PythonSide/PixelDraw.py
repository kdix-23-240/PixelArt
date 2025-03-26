# Description: ピクセルアートを描画する Python スクリプト

if __name__ == "__main__":
    # 色の定義
    RESET = "\033[0m"      # リセット
    BLACK = "\033[30m"     # 黒
    RED = "\033[31m"       # 赤
    GREEN = "\033[32m"     # 緑
    YELLOW = "\033[33m"    # 黄
    BLUE = "\033[34m"      # 青
    WHITE = "\033[37m"     # 白
    ORANGE = "\033[38;5;214m"  # オレンジ（ANSI 256色モード）
    PURPLE = "\033[35m"    # 紫
    LIGHT_BLUE = "\033[36m"  # 水色
    PINK = "\033[38;5;206m"  # ピンク（ANSI 256色モード）
    LIGHT_GREEN = "\033[38;5;190m"  # 黄緑（ANSI 256色モード）

    # 色付きの文字を表示
    a = RED + "■" + RESET
    b = GREEN + "■" + RESET
    c = YELLOW + "■" + RESET
    d = BLUE + "■" + RESET
    e = WHITE + "■" + RESET
    f = ORANGE + "■" + RESET
    g = PURPLE + "■" + RESET
    h = LIGHT_BLUE + "■" + RESET
    i = PINK + "■" + RESET
    j = LIGHT_GREEN + "■" + RESET

<<<<<<< HEAD
    # ---------------------ここから---------------------------
    
    # ピクセルアート格納用の二次元配列を定義
    list_width = 3
    list_height = 5
    pixel_art = [[e]*list_width]*list_height # 12x18 の白いピクセルアート

    #リスト化
    pixel_art[0] = [a,a,a]
    pixel_art[1] = [b,b,b]
    pixel_art[2] = [c,c,c]
    pixel_art[3] = [c,c,c]
    pixel_art[4] = [c,c,c]
=======
<<<<<<< HEAD
    #リスト化
    ab=[a,a,a,b,b,b,c,c,c]
    bc=[a,a,a,b,b,b,c,c,c]
    cd=[a,a,a,b,b,b,c,c,c]
    de=[a,a,a,b,b,b,c,c,c]
    ef=[a,a,a,e,e,e,c,c,c]
=======
    # ピクセルアート格納用の二次元配列を定義
    list_width = 8
    list_height = 7
    pixel_art = [[e]*list_width]*list_height # 12x18 の白いピクセルアート
>>>>>>> f8309d9 (動的変化可能に変更)

    #リスト化
    pixel_art[0] = [a,a,a,a,a,a,a,a]
    pixel_art[1] = [b,b,b,b,b,b,b,b]
    pixel_art[2] = [c,c,c,c,c,c,c,c]
    pixel_art[3] = [d,d,d,d,d,d,d,d]
    pixel_art[4] = [e,e,e,e,e,e,e,e]
    pixel_art[5] = [f,f,f,f,f,f,f,f]
    pixel_art[6] = [g,g,g,g,g,g,g,g]
>>>>>>> 244cd31 (動的変化可能に変更)

    # ピクセルアートを表示
    for i in range(len(pixel_art)):
        for j in range(len(pixel_art[i])):
            print(''.join(pixel_art[i][j]), end="")
        print()
        