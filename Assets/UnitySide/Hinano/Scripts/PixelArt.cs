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
    }
}