﻿using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class RemoveBets : MonoBehaviour
{
    public void CancelledBet()
    {
        string path = "/winningNumbers.txt";

        //Clears the file
        StreamWriter writer = new StreamWriter(Application.persistentDataPath + path, true);
        writer.WriteLine("Delete");
        writer.Close();
    }
}
