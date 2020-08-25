using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPuzzle3 : MonoBehaviour
{
    public AudioClip puzzle3Clip1;
    public AudioClip puzzle3Clip2;
    //public AudioClip successClip;
    public float[] volumesOfClips;

    public float fadeOutTime;
    public float fadeInTime;
    float timer_EnableBaseLoop;
    private float lengthClip2 = 0.0f;
    private float timeUntilClip2Finish = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        AudioManager audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        audioManager.PlayTrack(puzzle3Clip1, true, 8, 0.0f);
        audioManager.StartFadeIn(8, volumesOfClips[0], 1.0f);
        timer_EnableBaseLoop = 0.0f;

        audioManager.StartFadeOut(0, 3.0f);
        audioManager.StartFadeOut(1, 3.0f);
        audioManager.StartFadeOut(2, 3.0f);

        lengthClip2 = puzzle3Clip2.length;
    }

    void Update()
    {
        if(timer_EnableBaseLoop > 0.0f)
        {
            timer_EnableBaseLoop -= Time.deltaTime;
            if (timer_EnableBaseLoop <= 0.0f)
            {
                AudioManager audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
                audioManager.StartFadeIn(8, 1.0f, fadeInTime, 0.25f);
                timer_EnableBaseLoop = 0.0f;
            }
        }

        if(timeUntilClip2Finish > 0.0f)
        {
            timeUntilClip2Finish -= Time.deltaTime;
            if (timeUntilClip2Finish <= 0.0f) timeUntilClip2Finish = 0.0f;
        }
        
    }

    public void EndOfKitePuzzle()
    {
        timer_EnableBaseLoop = 0.0f;
    }

    public void KiteMatchAudio()
    {
        AudioManager audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        if (!audioManager.CheckIfPlaying(3))   //isPlaying is working just that the clip is abit long //timeUntilClip2Finish <=0.0f
        {
            Debug.Log("KiteMatchAudio");
            audioManager.PlayTrack(puzzle3Clip2, false, 3, volumesOfClips[1]);
            timeUntilClip2Finish = lengthClip2;

            //fadeout main track
            timer_EnableBaseLoop = fadeOutTime;
            audioManager.StartFadeOut(8, fadeOutTime, false, 0.25f, 0.25f);
        }
    }

    public void KiteMatchRepeat()
    {
        KiteMatchAudio();

        //AudioManager audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        //audioManager.PlayTrack(successClip, false, 5, volumesOfClips[2]);
    }
}
