# Description: ピクセルアートを描画する Python スクリプト

if __name__ == "__main__":
    # 色の定義
    RESET = "\033[0m"  # リセット
    RED = "\033[31m"   # 赤
    GREEN = "\033[32m" # 緑
    YELLOW = "\033[33m" # 黄
    BLUE = "\033[34m"  # 青
    MAGENTA = "\033[35m" # マゼンタ
    CYAN = "\033[36m"  # シアン
    WHITE = "\033[37m" # 白

    # 色付きの文字を表示
    r=RED+"■"+RESET
    g=GREEN+"■"+RESET
    y=YELLOW+"■"+RESET
    b=BLUE+"■"+RESET
    m=MAGENTA+"■"+RESET
    c=CYAN+"■"+RESET
    w=WHITE+"■"+RESET

    ab=[g,g,g,w,w,w,r,r,r]
    bc=[g,g,g,w,w,w,r,r,r]
    cd=[g,g,g,w,w,w,r,r,r]
    de=[g,g,g,w,w,w,r,r,r]
    # ef=[g,g,w,w,r,r]
    # fg=[g,g,w,w,r,r]
    print(''.join(ab))
    print(''.join(bc))
    print(''.join(cd))
    print(''.join(de))
    # print(''.join(ef))
    # print(''.join(fg))