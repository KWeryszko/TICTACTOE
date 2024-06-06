using Godot;
using System;
[GlobalClass]
public partial class Options : Resource
{
    [Export]
    public bool AiOpponent {  get => aiOpponent; set => aiOpponent = value; }
    private bool aiOpponent=false;
}
