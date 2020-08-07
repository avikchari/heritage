using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPuzzle1 : MonoBehaviour
{
    public AudioClip puzzle1Clip_Base;
    public AudioClip puzzle1Clip_Erhu;
    public AudioClip puzzle1Clip_Bansuri;
    public AudioClip puzzle1Clip_Ari;
    public float[] volumesOfClips;


    // Start is called before the first frame update
    void Start()
    {
        AudioManager audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        audioManager.PlayTrack(puzzle1Clip_Base, true, 0, volumesOfClips[0]);
    }

    public void MomPuzzleSolved()
    {
        AudioManager audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        audioManager.PlayTrack(puzzle1Clip_Erhu, true, 1, volumesOfClips[1]);
    }

    public void DadPuzzleSolved()
    {
        AudioManager audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        audioManager.PlayTrack(puzzle1Clip_Bansuri, true, 2, volumesOfClips[2]);
    }

    public void AriPuzzleSolved()
    {
        AudioManager audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        audioManager.PlayTrack(puzzle1Clip_Ari, true, 3, volumesOfClips[3]);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
