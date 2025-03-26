<<<<<<< HEAD
public class PixelArt
{
    private string[,] _pixelArt;
    public string[,] Data => _pixelArt;

    public PixelArt(string[,] pixelArt)
    {
        _pixelArt = pixelArt;
=======
using UnityEngine;

public class PixelArt : MonoBehaviour
{
    private int _id;
    public int Id
    {
        get => _id;
        set => _id = value;
    }
    private string _name;
    public string Name
    {
        get => _name;
        set => _name = value;
>>>>>>> f2254d1 (クイズ実装用のリファクタ準備)
    }
}