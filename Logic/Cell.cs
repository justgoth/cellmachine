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
    private Cell[,] _neighbour;

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
        _neighbour = new Cell[3, 3];
        UpdateColor();
    }

    public void Draw()
    {
        _window.Draw(_rectangle);
    }

    private void UpdateColor()
    {
        byte red, green, blue;
        red = (int)_value < 128 ? (byte)(_value * 2) : (byte)255;
        green = (int)_value < 128 ? (byte)0 : (byte)((_value - 128) * 2);
        blue = 0;
        _color = new Color(red, green, blue);
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
        int Value;

        Value = (int)GetValue()/2;

            Value += (_neighbour[0, 0].GetValue() * 0 +
                      _neighbour[1, 0].GetValue() * 0 +
                      _neighbour[2, 0].GetValue() * 0 +
                      _neighbour[0, 1].GetValue() * 1 +
                      _neighbour[1, 1].GetValue() * 1 +
                      _neighbour[2, 1].GetValue() * 1 +
                      _neighbour[0, 2].GetValue() * 2 +
                      _neighbour[1, 2].GetValue() * 2 +
                      _neighbour[2, 2].GetValue() * 2) / 17;
        if (Value < 0) Value = 0;
        if (Value > 255) Value = 255;
        SetValue((byte)Value);
    }

    public void StoreNeighbour(int x, int y, Cell neighbour)
    {
        _neighbour[x, y] = neighbour;
    }
}