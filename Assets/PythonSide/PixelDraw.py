# Description: ピクセルアートを描画する Python スクリプト

if __name__ == "__main__":
    # 色の定義
    RESET = "\033[0m"      # リセット
    BLACK = "\033[30m"     # 黒
    RED = "\033[31m"       # 赤
    GREEN = "\033[32m"     # 緑
    YELLOW = "\033[33m"    # 黄
    BLUE = "\033[34m"      # 青
    MAGENTA = "\033[35m"   # マゼンタ
    CYAN = "\033[36m"      # シアン
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
    e = MAGENTA + "■" + RESET
    f = CYAN + "■" + RESET
    g = WHITE + "■" + RESET
    h = ORANGE + "■" + RESET
    i = PURPLE + "■" + RESET
    j = LIGHT_BLUE + "■" + RESET
    k = PINK + "■" + RESET
    l = LIGHT_GREEN + "■" + RESET

    #リスト化
    ab=[a,a,a,b,b,b,c,c,c]
    bc=[a,a,a,b,b,b,c,c,c]
    cd=[a,a,a,b,b,b,c,c,c]
    de=[a,a,a,b,b,b,c,c,c]
    ef=[a,a,a,b,b,b,c,c,c]

    #表示
    print(''.join(ab))
    print(''.join(bc))
    print(''.join(cd))
    print(''.join(de))
    print(''.join(ef))