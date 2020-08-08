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
    public bool shdStopOnceTransition = false;
}


public class AudioManager : MonoBehaviour
{
    public AudioClip mouseClick;
    public Vector2 lowPassFilterRange;
    public AudioSource[] allAudioSources;
    private int numOfAudioSource;
    private AudioControlBlock[] allACB;

    private void Start()
    {
        numOfAudioSource = allAudioSources.Length;
        allACB = new AudioControlBlock[numOfAudioSource];
        for (int i = 0; i < numOfAudioSource; ++i)
        {
            allACB[i] = new AudioControlBlock();
        }
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            bool playDefault = true;
            GameObject overwriteMouse = GameObject.Find("MouseAudioManager");           
            if (overwriteMouse != null)
            {
                AudioMouseDrag audioMouseScript = overwriteMouse.GetComponent<AudioMouseDrag>();
                //play default mouse click only if player did not hover over a puzzle piece
                if (audioMouseScript.mouseClickSelection != -1)
                {
                    audioMouseScript.PlayPuzzleMouseClick(audioMouseScript.mouseClickSelection);
                    playDefault = false;
                }
            }

            if(playDefault)
            {
                PlayMouseClick();
            }
            
        }

        for (int i = 0; i < numOfAudioSource; ++i)
        {
            if (allACB[i].currTransitionState == AudioControlBlock.audioTransitionState.FADEIN)
            {
                allAudioSources[i].volume += allACB[i].rateOfChange * Time.deltaTime;
                if (allAudioSources[i].volume >= allACB[i].targetVolume)
                {
                    allAudioSources[i].volume = allACB[i].targetVolume;
                    allACB[i].transitionTimeLeft = 0.0f;
                    allACB[i].currTransitionState = AudioControlBlock.audioTransitionState.NONE;
                }
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

                    if(allACB[i].shdStopOnceTransition) allAudioSources[i].Stop();
                    allAudioSources[i].volume = allACB[i].originalVolume;
                }
                allACB[i].transitionTimeLeft -= Time.deltaTime;
            }
        }
    }

    public void StartFadeOut(int specificSource = -1, float timing = 2.0f, bool shdStop = true)
    {
        if (specificSource >= 0)
        {
            allACB[specificSource].currTransitionState = AudioControlBlock.audioTransitionState.FADEOUT;
            allACB[specificSource].transitionTimeLeft = timing;
            allACB[specificSource].targetVolume = 0.1f;
            allACB[specificSource].shdStopOnceTransition = shdStop;
            allACB[specificSource].originalVolume = allAudioSources[specificSource].volume;
            allACB[specificSource].rateOfChange = ((allAudioSources[specificSource].volume - 0.1f) / timing);
        }
    }

    public void StartFadeIn(int specificSource = -1, float volume =1.0f, float timing = 2.0f)
    {
        if (specificSource >= 0)
        {
            allACB[specificSource].currTransitionState = AudioControlBlock.audioTransitionState.FADEIN;
            allACB[specificSource].transitionTimeLeft = timing;
            allACB[specificSource].targetVolume = volume;
            allACB[specificSource].originalVolume = allAudioSources[specificSource].volume;
            allAudioSources[specificSource].volume = 0.0f;
            allACB[specificSource].rateOfChange = volume / timing;
        }
    }

    public void PlayTrack(AudioClip clip, bool islooping, int specificSource = -1, float volume = 1.0f)
    {
        if (specificSource >= 0)
        {
            allAudioSources[specificSource].clip = clip;
            allAudioSources[specificSource].loop = islooping;
            allAudioSources[specificSource].volume = volume;
            allAudioSources[specificSource].Play();

            allACB[specificSource].transitionTimeLeft = 0.0f;
            allACB[specificSource].currTransitionState = AudioControlBlock.audioTransitionState.NONE;
        }
    }

    public bool CheckIfPlaying(int specificSource = -1)
    {
        if (specificSource >= 0)
        {
            return allAudioSources[specificSource].isPlaying;
        }
        return false;
    }

    //percentage 0 to 1
    public void AdjustFxLowPassFilter(float percentage, int specificSource = 0)
    {
        float lowest = lowPassFilterRange.x;
        float highest = lowPassFilterRange.y;

        allAudioSources[5 + specificSource].GetComponent<AudioLowPassFilter>().cutoffFrequency = ((highest - lowest) * percentage) + lowest;
    }

    public void AdjustVolume(float percentage, int specificSource = 0)
    {
        allAudioSources[specificSource].volume = percentage;
    }

    public void PlayMouseClick()
    {
        AudioManager audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        audioManager.PlayTrack(mouseClick, false, 5, 1.0f);
        audioManager.AdjustFxLowPassFilter(1.0f);
    }
}