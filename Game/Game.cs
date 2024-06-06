using Godot;
using System;
using System.Collections.Generic;

public partial class Game : Node2D
{
    public override void _Ready()
    {
        for (int i = 0; i < 9; i++)
        {
            buttons.Add(new TileButton());
            buttons[i].GlobalPosition = new Vector2(100+ 100 * positionX,100+ 100 * positionY);
            buttons[i].setPosition(positionX,positionY);
            buttons[i].Size = new Vector2(100, 100);
            buttons[i].TextureNormal = normal;
            buttons[i].TilePressed += setTile;
            AddChild(buttons[i]);

            positionX += 1;
            if (i % 3 == 2)
            {
                positionY += 1;
                positionX = 0;
            }

        }
    }
    private void setTile(int positionX, int positionY)
    {
        char tempText;
        if (turn)
        {
            buttons[positionX + positionY * 3].TextureNormal = Xtext;
            tempText = 'X';
        }
        else
        {
            buttons[positionX + positionY * 3].TextureNormal = Ytext;
            tempText = 'O';
        }
        buttons[positionX + positionY * 3].Disabled = true;
        if (gameOver = CheckVictory())
        {
            Label label = new Label();
            label.Text = "WYGRANA "+tempText;
            label.Size = new Vector2(1000, 400);
            label.HorizontalAlignment = HorizontalAlignment.Center;
            label.AddThemeFontSizeOverride("font_size", 100);
            AddChild(label);
            foreach(var button in buttons)
            {
                button.Disabled = true;
            }
        }
        turn = !turn;
        
    }
    private bool CheckVictory()
    {
        if (buttons[0].TextureNormal != normal)
        {
            if (buttons[0].TextureNormal == buttons[1].TextureNormal && buttons[0].TextureNormal == buttons[2].TextureNormal) { return true; }
            if (buttons[0].TextureNormal == buttons[3].TextureNormal && buttons[0].TextureNormal == buttons[6].TextureNormal) { return true; }
            if (buttons[0].TextureNormal == buttons[4].TextureNormal && buttons[0].TextureNormal == buttons[8].TextureNormal) { return true; }
        }
        if (buttons[4].TextureNormal != normal)
        {
            if (buttons[4].TextureNormal == buttons[2].TextureNormal && buttons[4].TextureNormal == buttons[6].TextureNormal) { return true; }
            if (buttons[4].TextureNormal == buttons[1].TextureNormal && buttons[4].TextureNormal == buttons[7].TextureNormal) { return true; }
            if (buttons[4].TextureNormal == buttons[3].TextureNormal && buttons[4].TextureNormal == buttons[5].TextureNormal) { return true; }
        }
        if (buttons[8].TextureNormal != normal)
        {
            if (buttons[8].TextureNormal == buttons[2].TextureNormal && buttons[8].TextureNormal == buttons[5].TextureNormal) { return true; }
            if (buttons[8].TextureNormal == buttons[6].TextureNormal && buttons[8].TextureNormal == buttons[7].TextureNormal) { return true; }
        }
        return false;
    }
    private bool gameOver = false, turn= true; //true - X, false - O 
    private int positionX=0, positionY=0;
    private int tileSize=100;
    Texture2D normal = GD.Load<Texture2D>("res://Game/Resources/normal.png") , Xtext = GD.Load<Texture2D>("res://Game/Resources/X.png"), Ytext = GD.Load<Texture2D>("res://Game/Resources/O.png");
    private List<TileButton> buttons = new(0);
}
