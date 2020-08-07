using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoiceOverManager : MonoBehaviour
{
    public AudioClip[] voiceOverClips;

    public int stage;
    // Start is called before the first frame update
    void Start()
    {
        stage = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void IncrementStage()
    {
        ++stage;
        switch(stage)
        {
            case 1:
                //What are you...
                GetComponent<AudioManager>().PlayTrack(voiceOverClips[0], false, 7, 1);
                break;
            case 3:
                //I still dont know what im allowed to claim...
                GetComponent<AudioManager>().PlayTrack(voiceOverClips[1], false, 7, 1);
                break;
            case 4:
                //My Parents
                GetComponent<AudioManager>().PlayTrack(voiceOverClips[2], false, 7, 1);
                GetComponent<AudioManager>().AdjustFxLowPassFilter(1f, 2);
                break;
            case 5:
                //Pick one side
                GetComponent<AudioManager>().PlayTrack(voiceOverClips[3], false, 7, 1);
                break;
            case 6:
                //Im glad im here
                GetComponent<AudioManager>().PlayTrack(voiceOverClips[4], false, 7, 1);
                break;
            case 7:
                //Imposter?
                GetComponent<AudioManager>().PlayTrack(voiceOverClips[5], false, 7, 1);
                break;
            default:
                break;
        }
    }
}
