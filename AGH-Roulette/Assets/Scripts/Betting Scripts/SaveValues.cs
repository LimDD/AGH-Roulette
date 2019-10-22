﻿using System.IO;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;

public class SaveValues : MonoBehaviour
{
    public TMP_Text bal;
    public TMP_Text amount;
    SceneSwitcher sS;
    public GameObject panel;
    public GameObject table;

    //Saves the balance and the bet amounts into a text file
    public void WriteToFile(bool tutorial)
    {
        string balance = bal.text;
        string bet = amount.text;

        balance = Regex.Replace(balance, "Coins: ", "");

        int balNum = int.Parse(balance) - int.Parse(bet);

        balance = "Coins: " + balNum.ToString();

        bal.text = balance;

        string path = "/balandamount.txt";
        StreamWriter writer = new StreamWriter(Application.persistentDataPath + path, true);

        writer.WriteLine(balance);
        writer.WriteLine(bet);
        writer.Close();

        if (balNum == 0 && !tutorial)
        {
            CheckBal(balNum);
        }

        else
        {
            panel.SetActive(true);
        }
    }

    //Skips asking if they would like to make another bet and takes them straight to the wheel
    private void CheckBal(int balNum)
    {
        sS = table.GetComponent<SceneSwitcher>(); 
        table.SetActive(false);
        sS.WheelScene();       
    }
}
