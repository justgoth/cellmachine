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
    private Cell _zerocell;

    public Cells(RenderWindow window, int w, int h)
    {
        _window = window;
        _cells = new Cell[w, h];
        _w = w;
        _h = h;
        _stepping = true;
        _zerocell = new Cell(_window, 0, 0, 0, 1, 1);
        for (int x = 0; x < w; x++)
        {
            for (int y =0; y < h; y++)
                _cells[x,y] = new Cell(_window, x, y, 0, w, h);
        }
        for (int x = 0; x < w; x++)
        {
            for (int y = 0; y < h; y++)
            {
                for (int a = -1; a < 2; a++)
                {
                    for (int b = -1; b < 2; b++)
                    {
                        if ((x + a < 0) || (y + b < 0) || (x + a >= w) || (y + b >= h))
                        {
                            _cells[x, y].StoreNeighbour(a + 1, b + 1, _zerocell);
                        }
                        else
                        {
                            _cells[x, y].StoreNeighbour(a + 1, b + 1, _cells[x + a, y + b]);
                        }
                    }
                }
            }
        }
    }

    public void Draw()
    {
        foreach(Cell cell in _cells) cell.Draw();
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