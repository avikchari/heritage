using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioMouseDrag : MonoBehaviour
{
    public AudioClip[] overWriteMouseClick;
    public int mouseClickSelection = -1;

    [Header("0 = Dad ,1 = Mom, 2 = Ari")]
    //When click and hold loop
    public AudioClip[] dragStart;
    //When let go
    public AudioClip[] dragEnd;
    public bool proximityCheck;

    public void SetMouseSelection(int selection = -1)
    {
        mouseClickSelection = selection;
    }

    public void PlayPuzzleMouseClick(int variation = -1)
    {
        if (variation >= 0)
        {
            AudioManager audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
            audioManager.PlayTrack(overWriteMouseClick[variation], false, 5, 1.0f);
            audioManager.AdjustFxLowPassFilter(1.0f, 1);
        }
    }

    public void PlayDragStart(int variation = -1)
    {
        if (variation >= 0)
        {
            AudioManager audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
            //if (!audioManager.CheckIfPlaying())
            //{
            //  Debug.Log("DragSound");
            //}

            audioManager.PlayTrack(dragStart[variation], true, 6, 1.0f);

            if (proximityCheck)
            {
                audioManager.AdjustFxLowPassFilter(0.0f, 1);
            }
            else
            {
                audioManager.AdjustFxLowPassFilter(1.0f, 1);
            }
           
        }
    }

    public void PlayDragEnd(int variation = -1)
    {
        if (variation >= 0)
        {
            AudioManager audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
            audioManager.PlayTrack(dragEnd[variation], false, 6, 1.0f);
            audioManager.AdjustFxLowPassFilter(1.0f, 1);
        }
    }
}
