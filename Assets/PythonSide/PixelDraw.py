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
    a = RED + "■" + RESET
    b = GREEN + "■" + RESET
    c = YELLOW + "■" + RESET
    d = BLUE + "■" + RESET
    g = WHITE + "■" + RESET
    h = ORANGE + "■" + RESET
    i = PURPLE + "■" + RESET
    j = LIGHT_BLUE + "■" + RESET
    k = PINK + "■" + RESET
    l = LIGHT_GREEN + "■" + RESET

    # ピクセルアートをつくる
    pixel_art = [   
                [a,a,a,a,a,a,a],
                [a,a,a,a,a,a,a],
                [a,a,a,a,a,a,a],
                [a,a,a,a,a,a,a],
                [a,a,a,a,a,a,a],
                [a,a,a,a,a,a,a],
                [a,a,a,a,a,a,a],
                ]

    # ピクセルアートを表示
    for i in range(len(pixel_art)):
        for j in range(len(pixel_art[i])):
            print(''.join(pixel_art[i][j]), end="")
        print()
        