﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadBetNums : MonoBehaviour
{
    public NumberReader numberReader;
    public int NumToRead;

    AudioSource audioSource;
    public AudioSource betType;
    AudioClip[] clips;
    List<AudioClip> clipList;
    public int currentClip;
    public int lastClip;
    public bool reading;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        //Used in tutorial when narrating
        if (!audioSource.isPlaying && reading)
        {
            reading = false;
        }

        if (lastClip > 0)
        {
            IterateClipList(clipList);
        }
    }

    //Sets the clips to be read
    public void SetNumberList(List<int> num)
    {
        if (!reading)
        {
            clipList = new List<AudioClip>();

            foreach (int i in num)
            {
                clips = numberReader.GetNumberAudio(i);
                clipList.Add(clips[0]);

                //If number is over 20 there will be 2 clips
                if (i > 20)
                {
                    if (i != 30)
                    {
                        clipList.Add(clips[1]);
                    }
                }
            }

            lastClip = clipList.Count;
            currentClip = 0;
        }
    }

    //Reads through the number list
    private void IterateClipList(List<AudioClip> clips)
    {
        while ((currentClip < lastClip) && (!audioSource.isPlaying))
        {
            if (!betType.isPlaying)
            {
                Debug.Log("Playing clip " + (currentClip + 1) + " of " + lastClip + " which is " + clips[currentClip].name);
                audioSource.clip = clips[currentClip];
                audioSource.Play();

                currentClip++;
            }

            else if (currentClip > 0)
            {
                lastClip = 0;
            }
        }
    }

    //Stops the number reader from playing
    public void StopReading()
    {
        currentClip = lastClip;
    }
}
