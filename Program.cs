using CellMachine.Logic;
using SFML.Graphics;
using SFML.Window;

namespace CellMachine;

internal static class Program
{
    private static Cells? _cells;

    private static void Main(string[] args)
    {
        RenderWindow window = new RenderWindow(new VideoMode(1920, 1200), "Cell Machine");
        _cells = new Cells(window, 160, 120);
        
        window.SetActive();
        window.Closed += new EventHandler(OnClose);

        while (window.IsOpen)
        {
            window.Clear();
            window.DispatchEvents();
            _cells.Step();
            _cells.Draw();
            window.Display();
        }
    }

    private static void OnClose(object? ssender, EventArgs e)
    {
        var window = (RenderWindow)ssender!;
        window.Close();
    }
}