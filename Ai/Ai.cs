using Godot;
using System;
using System.Collections.Generic;

public partial class Ai
{
    public int ChooseTile(TileButton[] buttons)
    {
        int choice;

        //fr\\
        if (buttons[0].TextureNormal == buttons[1].TextureNormal && buttons[0].Occupied() && !buttons[2].Occupied()) choice = 2;
        else if (buttons[0].TextureNormal == buttons[2].TextureNormal && buttons[0].Occupied() && !buttons[1].Occupied()) choice = 1;
        else if (buttons[1].TextureNormal == buttons[2].TextureNormal && buttons[1].Occupied() && !buttons[0].Occupied()) choice = 0;
        //sr\\
        else if (buttons[3].TextureNormal == buttons[4].TextureNormal && buttons[3].Occupied() && !buttons[5].Occupied()) choice = 5;
        else if (buttons[3].TextureNormal == buttons[5].TextureNormal && buttons[3].Occupied() && !buttons[4].Occupied()) choice = 4;
        else if (buttons[4].TextureNormal == buttons[5].TextureNormal && buttons[4].Occupied() && !buttons[3].Occupied()) choice = 3;
        //tr\\
        else if (buttons[6].TextureNormal == buttons[7].TextureNormal && buttons[6].Occupied() && !buttons[8].Occupied()) choice = 8;
        else if (buttons[6].TextureNormal == buttons[8].TextureNormal && buttons[6].Occupied() && !buttons[7].Occupied()) choice = 7;
        else if (buttons[7].TextureNormal == buttons[8].TextureNormal && buttons[7].Occupied() && !buttons[6].Occupied()) choice = 6;

        //fc\\
        else if (buttons[0].TextureNormal == buttons[3].TextureNormal && buttons[0].Occupied() && !buttons[6].Occupied()) choice = 6;
        else if (buttons[0].TextureNormal == buttons[6].TextureNormal && buttons[0].Occupied() && !buttons[3].Occupied()) choice = 3;
        else if (buttons[3].TextureNormal == buttons[6].TextureNormal && buttons[3].Occupied() && !buttons[0].Occupied()) choice = 0;
        //sc\\
        else if (buttons[1].TextureNormal == buttons[4].TextureNormal && buttons[1].Occupied() && !buttons[7].Occupied()) choice = 7;
        else if (buttons[1].TextureNormal == buttons[7].TextureNormal && buttons[1].Occupied() && !buttons[4].Occupied()) choice = 4;
        else if (buttons[4].TextureNormal == buttons[7].TextureNormal && buttons[4].Occupied() && !buttons[1].Occupied()) choice = 1;
        //tc\\
        else if (buttons[2].TextureNormal == buttons[5].TextureNormal && buttons[2].Occupied() && !buttons[8].Occupied()) choice = 8;
        else if (buttons[2].TextureNormal == buttons[8].TextureNormal && buttons[2].Occupied() && !buttons[5].Occupied()) choice = 5;
        else if (buttons[5].TextureNormal == buttons[8].TextureNormal && buttons[5].Occupied() && !buttons[2].Occupied()) choice = 2;

        //ltr\\
        else if (buttons[2].TextureNormal == buttons[4].TextureNormal && buttons[2].Occupied() && !buttons[6].Occupied()) choice = 6;
        else if (buttons[2].TextureNormal == buttons[6].TextureNormal && buttons[2].Occupied() && !buttons[4].Occupied()) choice = 4;
        else if (buttons[4].TextureNormal == buttons[6].TextureNormal && buttons[4].Occupied() && !buttons[2].Occupied()) choice = 2;
        //rtl\\
        else if (buttons[0].TextureNormal == buttons[4].TextureNormal && buttons[0].Occupied() && !buttons[8].Occupied()) choice = 8;
        else if (buttons[0].TextureNormal == buttons[8].TextureNormal && buttons[0].Occupied() && !buttons[4].Occupied()) choice = 4;
        else if (buttons[4].TextureNormal == buttons[8].TextureNormal && buttons[4].Occupied() && !buttons[0].Occupied()) choice = 0;
        else
        {
            for (int i = 0; i < buttons.Length; i++)
            {
                if (!buttons[i].Occupied())
                {
                    possibleChoices.Add(i);
                }
            }
            Random random = new Random();
            int temp = random.Next(possibleChoices.Count);
            choice = possibleChoices[temp];
            possibleChoices.Clear();
            possibleChoices.Capacity = 0;
        }
        return choice;
    }
    public List<int> possibleChoices = new(0);
}
