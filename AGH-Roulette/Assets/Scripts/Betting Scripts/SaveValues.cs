﻿using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class SaveValues : MonoBehaviour
{
    public Text bal;
    public Text amount;

    //Saves the balance and the bet amounts into a text file
    public void WriteToFile()
    {
        string path = "/balandamount.txt";
        StreamWriter writer = new StreamWriter(Application.persistentDataPath + path, true);
        string balance = bal.text;
        string bet = amount.text;

        writer.WriteLine(balance);
        writer.WriteLine(bet);
        writer.Close();
    }
}
