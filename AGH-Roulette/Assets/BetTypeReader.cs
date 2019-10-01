﻿using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;
using System.Collections;
using TMPro;
using System.Collections.Generic;

public class BetTypeReader : MonoBehaviour
{
    public AudioSource source;
    public AudioClip clip1;
    public AudioClip clip2;
    public AudioClip clip3;
    public AudioClip clip4;
    public Button btn;
    NumberReaderScript nRS;
    ReadBetNums rBN;
    public float targetTime;
    List<int> numbers;
    bool readType;
    string betType;
    string btnName;

    public void Start()
    {
        nRS = FindObjectOfType<NumberReaderScript>();
        rBN = FindObjectOfType<ReadBetNums>();
    }

    public void CallTimer()
    {
        readType = false;
        StartCoroutine(StartCountdown(0.2f));
    }

    //Starts a countdown to check if the button is still in focus to determine whether the sound is played or not
    public IEnumerator StartCountdown(float f)
    {
        yield return new WaitForSeconds(f);

        if (btn.name == "Zoomed Button")
        {
            string num = btn.GetComponentInChildren<TMP_Text>().text;
            int i = int.Parse(num);
            nRS.SetNumber(i);
            nRS.ReadNumber();
        }

        else if (!readType)
        {
            BetType();
            readType = true;
        }

        else
        {
            Button bNum = GameObject.Find("Zoomed Button").GetComponent<Button>();
            string num = bNum.GetComponentInChildren<TMP_Text>().text;
            int n = int.Parse(num);
            numbers = ReadNums(n);

            rBN.SetNumberList(numbers);
        }
    }

    private void BetType()
    {
        AudioClip myClip;
        source.panStereo = 0f;

        myClip = clip1;

        betType = btn.GetComponentInChildren<Text>().text;

        betType = Regex.Replace(betType, "\n", " ");
        btnName = btn.name;

        if (btnName.Contains("Left"))
        {
            source.panStereo = -0.6f;
        }

        else
        {
            source.panStereo = 0.6f;
        }

        if (btnName == "TopLeftButton" || btnName == "TopRightButton" || btnName == "BottomLeftButton" || btnName == "BottomRightButton")
        {
            switch (betType)
            {
                case "Corner Bet":
                    myClip = clip1;
                    break;

                case "Six Line Bet":
                    myClip = clip2;
                    break;

                case "Basket Bet":
                    myClip = clip4;
                    break;

                case "Trio Bet":
                    myClip = clip3;
                    break;
            }
        }

        else if (btnName == "TopMiddleButton" || btnName == "MiddleLeftButton" || btnName == "MiddleRightButton" || btnName == "BottomMiddleButton")
        {
            switch (betType)
            {
                case "Split Bet":
                    myClip = clip1;
                    break;

                case "Street Bet":
                    myClip = clip2;
                    break;
            }
        }

        //Will have single bet audio to use here
        else if (btnName == "Zoomed Button")
        {
            myClip = clip1;
        }

        source.clip = myClip;
        source.Play();
        CallTimer();
    }

    private List<int> ReadNums(int num)
    {
        List<int> betNums = new List<int>();

        betNums.Add(num);

        if (betType == "Split Bet")
        {
            switch (btnName)
            {
                case "TopMiddleButton":
                    if (num < 3)
                    {
                        betNums.Add(0);
                    }

                    else
                    {
                        betNums.Add(num - 3);
                    }
                    break;

                case "MiddleLeftButton":
                    betNums.Add(num - 1);
                    break;

                case "MiddleRightButton":
                    betNums.Add(num + 1);
                    break;
                case "BottomMiddleButton":
                    betNums.Add(num + 3);
                    break;
            }
        }

        else if (betType == "Corner Bet")
        {
            switch (btnName)
            {
                case "TopLeftButton":
                    betNums.Add(num - 1);
                    betNums.Add(num - 4);
                    betNums.Add(num - 3);
                    break;

                case "TopRightButton":
                    betNums.Add(num - 3);
                    betNums.Add(num - 2);
                    betNums.Add(num + 1);
                    break;

                case "BottomLeftButton":
                    betNums.Add(num + 2);
                    betNums.Add(num + 3);
                    betNums.Add(num - 1);
                    break;
                case "BottomRightButton":
                    betNums.Add(num + 1);
                    betNums.Add(num + 3);
                    betNums.Add(num + 4);
                    break;
            }
        }

        else if (betType == "Street Bet")
        {
            betNums.Add(num + 1);
            betNums.Add(num + 2);
        }

        else if (betType == "Trio Bet")
        {
            if (num == 1)
            {
                betNums.Add(0);
                betNums.Add(2);
            }

            if (num == 2)
            {
                if (btnName == "TopLeftButton")
                {
                    betNums.Add(0);
                    betNums.Add(1);
                }

                else
                {
                    betNums.Add(0);
                    betNums.Add(3);
                }
            }

            if (num == 3)
            {
                betNums.Add(0);
                betNums.Add(2);
            }
        }

        else if (betType == "Six Line Bet")
        {
            if (btnName == "TopLeftButton")
            {
                for (int i = 1; i < 6; i++)
                {
                    betNums.Add(num + i);
                }
            }

            else
            {
                betNums.Add(num - 3);
                betNums.Add(num - 2);
                betNums.Add(num - 1);
                betNums.Add(num + 1);
                betNums.Add(num + 2);
            }
        }

        else if (betType == "Basket Bet")
        {
            for (int i = 0; i < 4; i++)
            {
                if (i != 1)
                {
                    betNums.Add(i);
                }
            }
        }

        return betNums;
    }
}

