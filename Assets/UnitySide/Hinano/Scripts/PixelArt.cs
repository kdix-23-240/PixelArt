public class PixelArt
{
    private string[,] _pixelArt;
    public string[,] Data => _pixelArt;

    public PixelArt(string[,] pixelArt)
    {
        _pixelArt = pixelArt;
    }
}