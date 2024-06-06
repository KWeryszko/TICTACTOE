using Godot;
using System;
[GlobalClass]
public partial class Options : Resource
{
    [Export]
    public bool AiOpponent {  get => aiOpponent; set => aiOpponent = value; }
    [Export]
    public bool AiStarts { get => aiStarts; set => aiStarts = value; }
    private bool aiOpponent=false;
    private bool aiStarts = true;
}
