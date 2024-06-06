using Godot;
using System;

public partial class Menu : Node2D
{
    public override void _Ready()
    {
        buttons[0]=GetChild<Button>(1);
        buttons[0].ButtonDown += pvpButtonClick;
        buttons[1]=GetChild<Button>(2);
        buttons[1].ButtonDown += pveButtonClick;
        buttons[2]=GetChild<Button>(3);
        buttons[2].ButtonDown += exitButtonClick;
    }
    private void pvpButtonClick()
    {
        _options.AiOpponent = false;
        GetTree().ChangeSceneToFile("res://Game/Game.tscn");
    }
    private void pveButtonClick()
    {
        _options.AiOpponent = true;
        GetTree().ChangeSceneToFile("res://Game/Game.tscn");
    }
    private void exitButtonClick()
    {
        GetTree().Quit();

    }
    private Options _options = GD.Load<Options>("res://Options/DefOpt.tres");
    private Button[] buttons = new Button[3]; //pvp, pve, exit
}
