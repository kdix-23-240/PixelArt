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

    # ---------------------ここから---------------------------
    
    # ピクセルアート格納用の二次元配列を定義
    list_width = 13
    list_height = 20
    pixel_art = [[e]*list_width]*list_height # 12x18 の白いピクセルアート

    #リスト化
    pixel_art[0] = [a,a,c,c,c,c,a,a,g,g,g,g,g]
    pixel_art[1] = [b,c,b,b,b,b,c,b,g,g,g,g,g]
    pixel_art[2] = [c,c,c,c,c,c,c,c,g,g,g,g,g]
    pixel_art[3] = [d,d,d,d,d,d,d,d,g,g,g,g,g]
    pixel_art[4] = [e,e,f,e,e,f,e,e,g,g,g,g,g]
    pixel_art[5] = [f,f,f,i,i,f,f,f,g,g,g,g,g]
    pixel_art[6] = [g,g,g,j,j,g,g,g,g,g,g,g,g]
    pixel_art[7] = [g,g,g,j,j,g,g,g,g,g,g,g,g]
    pixel_art[8] = [g,g,g,j,j,g,g,g,g,g,g,g,g]
    pixel_art[9] = [g,g,g,j,j,g,g,g,g,g,g,g,g]
    pixel_art[10] = [g,g,g,j,j,g,g,g,g,g,g,g,g]
    pixel_art[11] = [g,g,g,j,j,g,g,g,g,g,g,g,g]
    pixel_art[12] = [g,g,g,j,j,g,g,g,g,g,g,g,g]
    pixel_art[13] = [g,g,g,j,j,g,g,g,g,g,g,g,g]
    pixel_art[14] = [g,g,g,j,j,g,g,g,g,g,g,g,g]
    pixel_art[15] = [g,g,g,j,j,g,g,g,g,g,g,g,g]
    pixel_art[16] = [g,g,g,j,j,g,g,g,g,g,g,g,g]
    pixel_art[17] = [g,g,g,j,j,g,g,g,g,g,g,g,g]
    pixel_art[18] = [g,g,g,j,j,g,g,g,g,g,g,g,g]
    pixel_art[19] = [a,a,a,j,j,a,a,a,a,a,a,a,a]

    # ピクセルアートを表示
    for i in range(len(pixel_art)):
        for j in range(len(pixel_art[i])):
            print(''.join(pixel_art[i][j]), end="")
        print()
        