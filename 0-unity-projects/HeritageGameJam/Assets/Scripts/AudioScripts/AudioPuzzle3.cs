using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPuzzle3 : MonoBehaviour
{
    public AudioClip puzzle3Clip1;
    public AudioClip puzzle3Clip2;
    public AudioClip successClip;
    public float[] volumesOfClips;

    // Start is called before the first frame update
    void Start()
    {
        AudioManager audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        audioManager.PlayTrack(puzzle3Clip1, true, 0, volumesOfClips[0]);
    }

    public void KiteMatchAudio()
    {
        AudioManager audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        audioManager.PlayTrack(puzzle3Clip2, true, 1, volumesOfClips[1]);
    }

    public void KiteMatchRepeat()
    {
        AudioManager audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        audioManager.PlayTrack(successClip, true, 5, volumesOfClips[1]);
    }
}
