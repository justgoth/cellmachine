using SFML.System;

namespace CellMachine.Logic;

using SFML.Graphics;
using SFML.Window;

public class Cell
{
    private RenderWindow _window;
    private int _x, _y;
    private Color _color;
    private byte _value;
    private byte _oldvalue;
    private int _windowwidth, _windowheight;
    private RectangleShape _rectangle;
    public Neighbours _neighbours { get; set; }

    public Cell(RenderWindow window, int x, int y, byte value, int w, int h)
    {
        _window = window;
        _x = x;
        _y = y;
        _value = value;
        RefreshValue();
        _windowwidth = (int)window.Size.X;
        _windowheight = (int)window.Size.Y;
        _rectangle = new RectangleShape();
        _rectangle.Size = new Vector2f(_windowwidth / w, _windowheight / h);
        _rectangle.Position = new Vector2f(_x * (_windowwidth / w), _y * (_windowheight / h));
        _neighbours = new Neighbours();
        UpdateColor();
    }

    public void Draw()
    {
        _window.Draw(_rectangle);
    }

    private void UpdateColor()
    {
        byte _red, _green, _blue;
        _red = (int)_value < 128 ? (byte)(_value * 2) : (byte)255;
        _green = (int)_value < 128 ? (byte)0 : (byte)((_value - 128) * 2);
        _blue = 0;
        _color = new Color(_red, _green, _blue);
        _rectangle.FillColor = _color;
    }

    public void RefreshValue()
    {
        _oldvalue = _value;
    }

    public byte GetValue()
    {
        return _oldvalue;
    }

    public void SetValue(byte value)
    {
        _value = value;
        UpdateColor();
    }

    public void Step()
    {
        byte Value;
        
        Value = (byte)(((int)(_neighbours.UpperLeft * 0 + 
                              _neighbours.Upper * 0 + 
                              _neighbours.UpperRight * 0 + 
                              _neighbours.Left * 1 + 
                              _neighbours.Center * 1 + 
                              _neighbours.Right * 1+ 
                              _neighbours.LowerLeft * 2 + 
                              _neighbours.Lower * 2 + 
                              _neighbours.LowerRight * 2)) / 9); 
        SetValue(Value);
    }
}