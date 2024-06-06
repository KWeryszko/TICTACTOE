using Godot;
using System;
using System.Collections.Generic;

public partial class Game : Node2D
{
    public override void _Ready()
    {
        //grid\\
        grid.Texture = GD.Load<Texture2D>("res://Game/Resources/grid.png");
        grid.Position = gridPosition;
        AddChild(grid);

        //buttons\\
        for (int i = 0; i < 9; i++)
        {
            buttons.Add(new TileButton());
            buttons[i].GlobalPosition = new Vector2( (gridPosition[0] +25) + (tileSize+10) * positionX, (gridPosition[1] + 25) + (tileSize + 10) * positionY);
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
            GD.Print(_options.AiOpponent);
        }
        //control buttons\\
        controlButtons[0] = GetChild(0).GetChild<Button>(1);//retry
        controlButtons[0].ButtonDown += RetryButtonClick;
        controlButtons[0].Visible= false;
        controlButtons[1] = GetChild(0).GetChild<Button>(2);//menu
        controlButtons[1].ButtonDown += MenuButtonClick; ;
        controlButtons[1].Visible = false;
        //turnLabel\\
        turnLabel = GetChild(0).GetChild<Label>(3);
        turnLabel.Text = "Turn of player 1, player is playing X";
        //creating ai if Aiopponent is chosen\\
        if (_options.AiOpponent) ai = new Ai();
    }
    private void setTile(int positionX, int positionY)
    {
        char tempText;
        turnCounter++;
        if (turn)
        {
            buttons[positionX + positionY * 3].TextureNormal = Xtext;
            tempText = 'X';
            turnLabel.Text = "Turn of player 2, player is playing O";
        }
        else
        {
            buttons[positionX + positionY * 3].TextureNormal = Ytext;
            tempText = 'O';
            turnLabel.Text = "Turn of player 1, player is playing X";
        }
        buttons[positionX + positionY * 3].setOccupied();
        buttons[positionX + positionY * 3].Disabled = true;
        

        if (gameOver = CheckVictory())
        {
            GenerateLabel(tempText + " Won!");
            turnLabel.Visible = false;
            foreach (var button in buttons)
            {
                button.Disabled = true;
            }
            controlButtons[0].Visible = true;
            controlButtons[1].Visible = true;
        }
        else if (turnCounter == 9)
        {
            gameOver = true;
            GenerateLabel("Draw!");
            turnLabel.Visible = false;
            foreach (var button in buttons)
            {
                button.Disabled = true;
            }
            controlButtons[0].Visible = true;
            controlButtons[1].Visible = true;
        }

        turn = !turn;
        if (_options.AiOpponent && !gameOver)
        {
            AiTurn();
            turn = !turn;
        }

        
        
        
    }
    private void AiTurn()
    {
        int positionChosenByAi = ai.ChooseTile(buttons.ToArray());
        GD.Print(positionChosenByAi.ToString());
        char tempText;
        turnCounter++;
        if (turn)
        {
            buttons[positionChosenByAi].TextureNormal = Xtext;
            tempText = 'X';
            turnLabel.Text = "Turn of player 2, player is playing O";
        }
        else
        {
            buttons[positionChosenByAi].TextureNormal = Ytext;
            tempText = 'O';
            turnLabel.Text = "Turn of player 1, player is playing X";
        }
        buttons[positionChosenByAi].setOccupied();
        buttons[positionChosenByAi].Disabled = true;
        

        if (gameOver = CheckVictory())
        {
            GenerateLabel(tempText + " Won!");
            turnLabel.Visible = false;
            foreach (var button in buttons)
            {
                button.Disabled = true;
            }
            controlButtons[0].Visible = true;
            controlButtons[1].Visible = true;
        }
        else if (turnCounter == 9)
        {
            GenerateLabel("Draw!");
            turnLabel.Visible = false;
            foreach (var button in buttons)
            {
                button.Disabled = true;
            }
            controlButtons[0].Visible = true;
            controlButtons[1].Visible = true;
        }

    }
    private void GenerateLabel(string labelText)
    {

        Label label = new Label();
        label.Text = labelText;
        label.Size = new Vector2(1152, 648);
        label.HorizontalAlignment = HorizontalAlignment.Center;
        label.AddThemeFontSizeOverride("font_size", 100);
        label.AddThemeColorOverride("font_color", new Color(0, 0, 0));
        AddChild(label);
    }
    private bool CheckVictory()
    {
        if (buttons[0].Occupied())
        {
            if (buttons[0].TextureNormal == buttons[1].TextureNormal && buttons[0].TextureNormal == buttons[2].TextureNormal) { return true; }
            if (buttons[0].TextureNormal == buttons[3].TextureNormal && buttons[0].TextureNormal == buttons[6].TextureNormal) { return true; }
            if (buttons[0].TextureNormal == buttons[4].TextureNormal && buttons[0].TextureNormal == buttons[8].TextureNormal) { return true; }
        }
        if (buttons[4].Occupied())
        {
            if (buttons[4].TextureNormal == buttons[2].TextureNormal && buttons[4].TextureNormal == buttons[6].TextureNormal) { return true; }
            if (buttons[4].TextureNormal == buttons[1].TextureNormal && buttons[4].TextureNormal == buttons[7].TextureNormal) { return true; }
            if (buttons[4].TextureNormal == buttons[3].TextureNormal && buttons[4].TextureNormal == buttons[5].TextureNormal) { return true; }
        }
        if (buttons[8].Occupied())
        {
            if (buttons[8].TextureNormal == buttons[2].TextureNormal && buttons[8].TextureNormal == buttons[5].TextureNormal) { return true; }
            if (buttons[8].TextureNormal == buttons[6].TextureNormal && buttons[8].TextureNormal == buttons[7].TextureNormal) { return true; }
        }
        return false;
    }
    private void MenuButtonClick()
    {
        GetTree().ChangeSceneToFile("res://Menu/Menu.tscn");
    }
    private void RetryButtonClick()
    {
        GetTree().ChangeSceneToFile("res://Game/Game.tscn");
    }
    private bool gameOver = false, turn= true; //true - X, false - O 
    private int positionX=0, positionY=0;
    private int tileSize=100, turnCounter=0;
    private Vector2 gridPosition=new Vector2((1152-370)/2, (648 - 370) / 2);
    private Texture2D normal = GD.Load<Texture2D>("res://Game/Resources/normal.png") , Xtext = GD.Load<Texture2D>("res://Game/Resources/X.png"), Ytext = GD.Load<Texture2D>("res://Game/Resources/O.png");
    private List<TileButton> buttons = new(0);
    private Button[] controlButtons = new Button[2];
    private TextureRect grid = new();
    private Label turnLabel;
    private Options _options = GD.Load<Options>("res://Options/DefOpt.tres");
    private Ai ai;
}
