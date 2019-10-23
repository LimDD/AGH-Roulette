﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class NumberReaderScript : MonoBehaviour
{
    public NumberReader numberReader;

    [Header("Press A after changing the number to read")]
    public int NumToRead;

    AudioSource audioSource;
    AudioClip[] clips;
    public AudioClip coins;
    public int currentClip;
    public int lastClip;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        //Iterating through clips
        IterateClips();
    }

    public void StopReading()
    {
        currentClip = lastClip;
    }

    //IterateClips
    //Iterates through clips in queue and plays them one by one
    private void IterateClips()
    {
        if (EventSystem.current.currentSelectedGameObject != null)
        {
            if (EventSystem.current.currentSelectedGameObject.name == "BalBtn")
            {
                audioSource.PlayOneShot(coins);
                EventSystem.current.SetSelectedGameObject(null);
            }
        }

        while ((currentClip < lastClip) && (!audioSource.isPlaying))
        {
            Debug.Log("Playing clip " + (currentClip + 1) + " of " + lastClip + " which is " + clips[currentClip].name);
            audioSource.clip = clips[currentClip];
            audioSource.Play();

            currentClip++;
        }
    }

    private void IterateClipList(List<AudioClip> clips)
    {
        while ((currentClip < lastClip) && (!audioSource.isPlaying))
        {
            Debug.Log("Playing clip" + currentClip + " of " + lastClip + " which is " + clips[currentClip].name);
            audioSource.clip = clips[currentClip];
            audioSource.Play();

            currentClip++;
        }
    }

    //SetNumber
    //Takes in integer parameter
    //Sets integer parameter i as NumToRead
    public void SetNumber(int i)
    {
        NumToRead = i;
    }

    public void SetNumberList(List<int> num)
    {
        List<AudioClip> clipList = new List<AudioClip>();

        foreach (int i in num)
        {
            clips = numberReader.GetNumberAudio(i);

            clipList.Add(clips[0]);
        }


        lastClip = clipList.Count;
        currentClip = 0;
        IterateClipList(clipList);
    }

    //GetNumber
    //Returns currently set number in NumToRead
    public int GetNumber()
    {
        return NumToRead;
    }

    //ReadNumber
    //Sets currentClip to 0, initiating reading of the currently set number
    public void ReadNumber()
    {
        if (lastClip == currentClip)
        {
            clips = numberReader.GetNumberAudio(NumToRead);
            lastClip = clips.Length;
            currentClip = 0;
        }
    }

    public bool SetColour(int winNum)
    {
        bool colourRed = false;

        List<int> REDNUMBERS = new List<int>()
        {
            1,
            3,
            5,
            7,
            9,
            12,
            14,
            16,
            18,
            19,
            21,
            23,
            25,
            27,
            30,
            32,
            34,
            36
        };

        foreach (int i in REDNUMBERS)
        {
            if (winNum == i)
            {
                colourRed = true;
            }
        }

        return colourRed;
    }
}
