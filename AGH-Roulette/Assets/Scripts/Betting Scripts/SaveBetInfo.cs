﻿using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class SaveBetInfo : MonoBehaviour
{
    private GetButtonNum gBN;
    public List<string> betNums;
    public List<int> winNum;
    string btnName;

    private void Awake()
    {
        betNums = new List<string>();
    }

    public void SaveBetType()
    {
        winNum = new List<int>();
        string str;

        btnName = EventSystem.current.currentSelectedGameObject.name;

        Button btn = GameObject.Find(btnName).GetComponent<Button>();

        //Gets the texttmp component of the button and saves it to a string

        TMP_Text[] text = btn.GetComponentsInChildren<TMP_Text>();

        //The first child is a number and the last is the bet type
        str = text[text.Length - 1].text;

        //Remove New Lines in the string
        string type = Regex.Replace(str,"\n"," ");

        int.TryParse(type, out int single);

        //If the tryparse doesn't fail then single will be = to a number meaning it was a single number bet
        if (single != 0 || type == "0")
        {
            type = "Single Bet";
        }

        //The only strings containing "Bet" are inside bets
        if (type.Contains("Bet"))
        {
            WinningNumbers(type);
        }

        else
        {
            WinningNumbersOutside(type);
        }
    }

    //Gets the list of numbers the player can win on according to the number they chose and the type of bet
    //and saves it in winNum list
    private void WinningNumbers(string betType)
    {
        gBN = FindObjectOfType<GetButtonNum>();

        int num;

        //Zero does not have a zoom screen so it never uses the GetButtonNum script, so any errors must mean the bet on number was zero
        try
        {
            num = gBN.GetNumber();
        }

        catch
        {
            num = 0;
        }

        winNum.Add(num);

        if (betType == "Split Bet")
        {
            switch (btnName)
            {
                case "Top Middle Number":
                    if (num < 3)
                    {
                        winNum.Add(0);
                    }

                    else
                    {
                        winNum.Add(num - 3);
                    }
                    break;

                case "Middle Left Number":
                    winNum.Add(num - 1);
                    break;

                case "Middle Right Number":
                    winNum.Add(num + 1);
                    break;
                case "Bottom Middle Number":
                    winNum.Add(num + 3);
                    break;
            }
        }

        else if (betType == "Corner Bet")
        {
            switch (btnName)
            {
                case "Top Left Number":
                    winNum.Add(num - 1);
                    winNum.Add(num - 4);
                    winNum.Add(num - 3);
                    break;

                case "Top Right Number":
                    winNum.Add(num - 3);
                    winNum.Add(num - 2);
                    winNum.Add(num + 1);
                    break;

                case "Bottom Left Number":
                    winNum.Add(num + 2);
                    winNum.Add(num + 3);
                    winNum.Add(num - 1);
                    break;
                case "Bottom Right Number":
                    winNum.Add(num + 1);
                    winNum.Add(num + 3);
                    winNum.Add(num + 4);
                    break;
            }
        }

        else if (betType == "Street Bet")
        {
            winNum.Add(num + 1);
            winNum.Add(num + 2);
        }

        else if (betType == "Trio Bet")
        {
            if (num == 1)
            {
                winNum.Add(0);
                winNum.Add(2);
            }

            if (num == 2)
            {
                if (btnName == "Top Left Number")
                {
                    winNum.Add(0);
                    winNum.Add(1);
                }

                else 
                {
                    winNum.Add(0);
                    winNum.Add(3);
                }
            }

            if (num == 3)
            {
                winNum.Add(0);
                winNum.Add(2);
            }
        }

        else if (betType == "Six Line Bet")
        {
            if (btnName == "Bottom Left Number")
            {
                for (int i = 1; i < 6; i++)
                {
                    winNum.Add(num + i);
                }
            }

            else
            {
                winNum.Add(num - 3);
                winNum.Add(num - 2);
                winNum.Add(num - 1);
                winNum.Add(num + 1);
                winNum.Add(num + 2);
            }
        }

        else if (betType == "Basket Bet")
        {
            for (int i = 0; i < 4; i++)
            {
                if (i != 1)
                {
                    winNum.Add(i);
                }
            }
        }
        SaveNums(betType);
    }

    //Gets all the winning numbers from the bet type
    private void WinningNumbersOutside(string betType)
    {
        int calc = 0;

        if (btnName == "1st_Column" || btnName == "2nd_Column" || btnName == "3rd_Column")
        {
            switch (btnName)
            {
                case "1st_Column":
                    calc = -2;
                    break;

                case "2nd_Column":
                    calc = -1;
                    break;

                case "3rd_Column":
                    calc = 0;
                    break;
            }

            for (int i = 0; i < 12; i++) 
            {
                calc += 3;
                winNum.Add(calc);
            }
        }

        else if (btnName == "1st_Third" || btnName == "2nd_Third" || btnName == "3rd_Third")
        {
            switch (btnName)
            {
                case "1st_Third":
                    calc = 1;
                    break;

                case "2nd_Third":
                    calc = 13;
                    break;

                case "3rd_Third":
                    calc = 25;
                    break;
            }

            for (int i = calc; i < calc + 12; i++)
            {
                winNum.Add(i);
            }
        }

        else if (btnName == "Evens" || btnName == "Odds")
        {
            switch (btnName)
            {
                case "Evens":
                    calc = 2;
                    break;

                case "Odds":
                    calc = 1;
                    break;
            }

            for (int i = calc; i < 36; i+=2)
            {
                winNum.Add(i);
            }
        }

        else if (btnName == "Reds" || btnName == "Blacks")
        {
            switch (btnName)
            {
                case "Blacks":
                    calc = 2;
                    break;

                case "Reds":
                    calc = 1;
                    break;
            }

            for (int i = calc; i <= 35; i +=2)
            {
                winNum.Add(i);

                if (i == 10 || i == 28 || i == 18)
                {
                    i--;
                }

                else if (i == 17 || i == 9 || i == 27)
                {
                    i++;
                }
            }
        }

        else if (btnName == "1_To_18" || btnName == "19_To_36")
        {
            switch (btnName)
            {
                case "1_To_18":
                    calc = 1;
                    break;

                case "19_To_36":
                    calc = 19;
                    break;
            }

            for (int i = calc; i < calc + 18; i++)
            {
                winNum.Add(i);
            }
        }
        SaveNums(betType);
    }

    public void SaveNums(string betType)
    {
        betNums.Add(betType);

        foreach (int i in winNum)
        {
            betNums.Add(i.ToString());
        }
    }

    public List<string> GetSavedNums()
    {
        return betNums;
    }
}
