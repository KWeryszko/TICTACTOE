using Godot;
using System;

public partial class TileButton : TextureButton
{
    [Signal]
    public delegate void TilePressedEventHandler(int positionX, int positionY);
    public override void _Pressed()
    {
        EmitSignal(SignalName.TilePressed, positionX, positionY);
        base._Pressed();
        
    }
    public void setPosition(int x, int y)
    {
        positionX = x;
        positionY = y;
    }
    public bool Occupied() { return occupied; }
    public void setOccupied() {  occupied = true; }
    private int positionX, positionY;
    private bool occupied=false;
}
