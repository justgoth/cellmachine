using SFML.System;
using SFML.Window;

namespace CellMachine.Logic;

using SFML.Graphics;

public class Cells
{
    private readonly RenderWindow _window;
    private readonly Cell[,] _cells;
    private readonly int _w;
    private readonly int _h;
    private bool _stepping;

    public Cells(RenderWindow window, int w, int h)
    {
        _window = window;
        _cells = new Cell[w, h];
        _w = w;
        _h = h;
        _stepping = false;
        for (int x = 0; x < w; x++)
        {
            for (int y =0; y < h; y++)
                _cells[x,y] = new Cell(_window, x, y, 0, w, h);
        }
    }

    public void Draw()
    {
        foreach(Cell cell in _cells) cell.Draw();
    }

    private void UpdateNeighbours(int x, int y)
    {
        Neighbours neighbours = _cells[x, y]._neighbours;
        neighbours.UpperLeft = x == 0 ? (byte)0 : (y == 0 ? (byte)0 : _cells[x - 1, y - 1].GetValue());
        neighbours.Upper = y == 0 ? (byte)0 : _cells[x, y - 1].GetValue();
        neighbours.UpperRight = x == _w - 1 ? (byte)0 : (y == 0 ? (byte)0 : _cells[x + 1, y - 1].GetValue());
        neighbours.Left = x == 0 ? (byte)0 : _cells[x - 1, y].GetValue();
        neighbours.Center = _cells[x, y].GetValue();
        neighbours.Right = x == _w - 1 ? (byte)0 : _cells[x + 1, y].GetValue();
        neighbours.LowerLeft = x == 0 ? (byte)0 : (y == _h - 1 ? (byte)0 : _cells[x - 1, y + 1].GetValue());
        neighbours.Lower = y == _h - 1 ? (byte)0 : _cells[x, y + 1].GetValue();
        neighbours.LowerRight = x == _w - 1 ? (byte)0 : (y == _h - 1 ? (byte)0 : _cells[x + 1, y + 1].GetValue());
        _cells[x, y]._neighbours = neighbours;
    }

    public void Step()
    {
        Throw();
        if (!_stepping) return;
        for (int x = 0; x < _w; x++)
        {
            for (int y = 0; y < _h; y++)
            {
                _cells[x, y].RefreshValue();
            }
        }
        for (int x = 0; x < _w; x++)
        {
            for (int y = 0; y < _h; y++)
            {
                UpdateNeighbours(x, y);
            }
        }
        for (int x = 0; x < _w; x++)
        {
            for (int y = 0; y < _h; y++)
            {
                _cells[x, y].Step();
            }
        }
    }

    public void Throw()
    {
        /*        
        for (int x = -5; x < 6; x++)
        {
            for (int y = - 5; y < 6; y++) 
            {
                if (_cells[_w / 2 + x, _h - 10 + y].GetValue() < 160)
                {
                    _cells[_w / 2 + x, _h - 10 + y].SetValue(255);
                }
            }    
        }
        */
        if (Mouse.IsButtonPressed(Mouse.Button.Left))
        {
            Vector2i mousePos = Mouse.GetPosition(_window);
            if ((mousePos.X < 0) || (mousePos.Y < 0) || (mousePos.X > _window.Size.X) || (mousePos.Y > _window.Size.Y))
            {
                return;
            } else
            {
                int x = mousePos.X;
                int y = mousePos.Y;
                _cells[x / (_window.Size.X / _w), y / (_window.Size.Y / _h)].SetValue(255);
            }
        }

        if (Keyboard.IsKeyPressed(Keyboard.Key.Space)) _stepping = !_stepping;
    }

}