using UnityEngine;

public class Pixel : MonoBehaviour
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
    }
    private Color _color;
    public Color Color
    {
        get => _color;
        set => _color = value;
    }

    public void ChangeQuizMode()
    {
        // グレーに変更
        GetComponent<SpriteRenderer>().color = Color.gray;
    }
}