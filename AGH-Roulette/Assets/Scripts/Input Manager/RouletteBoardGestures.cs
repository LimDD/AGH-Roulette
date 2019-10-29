﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RouletteBoardGestures : MonoBehaviour
{
    SelectButton sB;
    BoardGestureInput bGI;
    Button btn;
    bool selected;

    private void Start()
    {
        sB = FindObjectOfType<SelectButton>();
        bGI = FindObjectOfType<BoardGestureInput>();
        btn = null;
        selected = true;
    }

    public void Gestures(string type)
    {
        //Selects the button when clicked
        if (type != "Click" && type != "back")
        {
            btn = sB.GetButton();
            selected = false;
        }


        if (btn != null && !selected)
        {
            selected = true;
            
            //Access button onClick on double click
            if (type == "DoubleClick")
            {
                EventSystem.current.SetSelectedGameObject(btn.gameObject);
                btn.onClick.Invoke();
            }
        }
    }
}
