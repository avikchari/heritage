using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioControlBlock
{
    public enum audioTransitionState { FADEIN, NORMAL, FADEOUT, NONE };
    public audioTransitionState currTransitionState = audioTransitionState.NONE;
    public float transitionTimeLeft = 0.0f;
    public float targetVolume = 0.0f;
    public float rateOfChange = 0.0f;
    public float originalVolume = 0.0f;
}


public class AudioManager : MonoBehaviour
{
    public AudioSource[] allAudioSources;
    private int numOfAudioSource;
    private AudioControlBlock[] allACB;

    private void Start()
    {
        numOfAudioSource = allAudioSources.Length;
        allACB = new AudioControlBlock[numOfAudioSource];
        for(int i = 0; i < numOfAudioSource; ++i)
        {
            allACB[i] = new AudioControlBlock();
        }
    }

    private void Update()
    {
        for(int i = 0; i < numOfAudioSource; ++i)
        {
            if(allACB[i].currTransitionState == AudioControlBlock.audioTransitionState.FADEIN)
            {
                allACB[i].transitionTimeLeft -= Time.deltaTime;

            }
            else if (allACB[i].currTransitionState == AudioControlBlock.audioTransitionState.FADEOUT)
            {
                allAudioSources[i].volume -= allACB[i].rateOfChange * Time.deltaTime;
                if (allAudioSources[i].volume <= allACB[i].targetVolume)
                {
                    allAudioSources[i].volume = allACB[i].targetVolume;
                    allACB[i].transitionTimeLeft = 0.0f;
                    allACB[i].currTransitionState = AudioControlBlock.audioTransitionState.NONE;

                    allAudioSources[i].Stop();
                    allAudioSources[i].volume = allACB[i].originalVolume;
                }
                allACB[i].transitionTimeLeft -= Time.deltaTime;
            }
        }
    }

    //Specify -1 to fade all 
    public void StartFadeOut(int specificSource = -1, float timing = 2.0f)
    {
        if(specificSource >= 0)
        {
            allACB[specificSource].currTransitionState = AudioControlBlock.audioTransitionState.FADEOUT;
            allACB[specificSource].transitionTimeLeft = timing;
            allACB[specificSource].targetVolume = 0.1f;
            allACB[specificSource].originalVolume = allAudioSources[specificSource].volume;
            allACB[specificSource].rateOfChange = ((allAudioSources[specificSource].volume - 0.1f)/timing);
        }
        else
        {

        }
    }
}
