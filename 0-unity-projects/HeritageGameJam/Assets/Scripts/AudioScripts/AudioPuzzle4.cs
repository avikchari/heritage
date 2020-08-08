﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPuzzle4 : MonoBehaviour
{
    public AudioClip puzzle4Clip_0;
    public AudioClip puzzle4Clip_1;
    public AudioClip puzzle4Clip_2;
    public AudioClip puzzle4Clip_3;
    public AudioClip puzzle4Clip_4;
    public AudioClip puzzle4Success;
    public float[] volumesOfClips;

    void Start()
    {
        //hardcode again
        GameObject.Find("PuzzleMaster3").GetComponent<AudioPuzzle3>().EndOfKitePuzzle();

        
        AudioManager audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        audioManager.StartFadeIn(0, volumesOfClips[0], 1.0f);
        audioManager.StartFadeOut(8, volumesOfClips[0]);
        audioManager.PlayTrack(puzzle4Clip_0, true, 0, volumesOfClips[0]);
        Clear_Background();
        Clear_Mom();
        Clear_Dad();
        Clear_Ari();
    }

    public void AdjustVolume(int specific, float volume)
    {
        Debug.Log("Slider Volume = " + volume);

        AudioManager audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        audioManager.AdjustVolume(volume, specific);
    }

    public void Clear_Background()
    {
        AudioManager audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        audioManager.PlayTrack(puzzle4Clip_1, true, 1, volumesOfClips[1]);
    }

    public void Clear_Mom()
    {
        AudioManager audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        audioManager.PlayTrack(puzzle4Clip_2, true, 2, volumesOfClips[2]);
    }

    public void Clear_Dad()
    {
        AudioManager audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        audioManager.PlayTrack(puzzle4Clip_3, true, 3, volumesOfClips[3]);
    }
    public void Clear_Ari()
    {
        AudioManager audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        audioManager.PlayTrack(puzzle4Clip_4, true, 4, volumesOfClips[4]);
    }

    public void FinishedGame()
    {
        AudioManager audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        audioManager.PlayTrack(puzzle4Success, false, 5, volumesOfClips[5]);
    }

}