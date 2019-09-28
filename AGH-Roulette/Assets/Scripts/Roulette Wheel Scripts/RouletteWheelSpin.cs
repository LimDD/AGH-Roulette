﻿using UnityEngine.UI;
using UnityEngine;

public class RouletteWheelSpin : MonoBehaviour
{
    //Result of the bet compared to the roulette wheel value
    public bool winner;

    //Result after the wheel spins
    public int rouletteValue;
        
    //Wheel rotation values
    public float xAngle, yAngle, zAngle;
    public int spinTimer;
    public bool wheelSpinning;

    //Result Text
    public Text resulttext_component;
    public string resulttext;

    bool check;

    SpinResult sR;


    public Button back;

    // Start is called before the first frame update
    void Start()
    {
        sR = FindObjectOfType<SpinResult>();

        winner = false;
        check = false;

        //Result after the wheel spins
        //rouletteValue = 23;
        rouletteValue = Random.Range(0, 36);

        //Wheel rotation values
        xAngle = 0;
        yAngle = 0;
        zAngle = 0;
        spinTimer = 0;
        wheelSpinning = true;
        check = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (wheelSpinning)
        {
            if(spinTimer < 60)
            {
                //Wheel no spinning rotation code
                transform.Rotate(xAngle, yAngle, zAngle, Space.Self);

                spinTimer += 1;
                zAngle -= 0.05f;
            }
            else if(spinTimer == 60 || spinTimer < 240)
            {
                //Wheel no spinning rotation code
                this.transform.Rotate(xAngle, yAngle, zAngle, Space.Self);

                spinTimer += 1;
            }
            else if(spinTimer == 240 || spinTimer < 300)
            {
                //Wheel no spinning rotation code
                this.transform.Rotate(xAngle, yAngle, zAngle, Space.Self);

                spinTimer += 1;
                zAngle += 0.04f;

                if(spinTimer == 300)
                {
                    wheelSpinning = false;
                }
            }
        }
        
        if(!wheelSpinning)
        {
            zAngle = 0.0f;
            transform.Rotate(xAngle, yAngle, zAngle, Space.Self);

            if (!check)
            {
                check = true;
                sR.CheckIfWinner();
            }
        }
    }

    //Is called when you win a game and the roulette value is the same as your bet
    //Changed int winning_bet to roulette_value
    public void Winner(int roulette_value)
    {
        //resulttext_component.text = "You won the round with " + winning_bet + "!!!";
        resulttext_component.text = "The ball landed on " + roulette_value + "! You have won!!";
        ShowButtons();
    }

    //Is called when you lose a game and the roulette value is not the same as your bet
    public void Loser(int roulette_value)
    {
        resulttext_component.text = "The ball landed on " + roulette_value + " meaning you lost this round...";
        ShowButtons();
    }

    public void ShowButtons()
    {
        back.interactable = true;
    }
}

