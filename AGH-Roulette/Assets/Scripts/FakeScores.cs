﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeScores : MonoBehaviour
{
    public int rounds;
    public int aWon;
    public int aLost;
    public int betNum;
    public int profit;

    // Start is called before the first frame update
    void Start()
    {
        rounds = 5;
        aWon = 100;
        aLost = 245;
        betNum = 13;

    }
}