namespace CellMachine.Logic;

public class Neighbours
{
    public byte UpperLeft { get; set; }
    public byte Upper { get; set; }
    public byte UpperRight { get; set; }
    public byte Left { get; set; }
    public byte Center { get; set; }
    public byte Right { get; set; }
    public byte LowerLeft { get; set; }
    public byte Lower { get; set; }
    public byte LowerRight { get; set; }

    public Neighbours()
    {
        UpperLeft = Upper = UpperRight = Left = Center = Right = LowerLeft = Lower = LowerRight = (byte)0;
    }
}
