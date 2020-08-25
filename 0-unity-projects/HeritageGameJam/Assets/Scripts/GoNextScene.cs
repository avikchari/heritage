using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoNextScene : MonoBehaviour
{
    public bool incrementVoiceOver = false;
    public bool playClick;
    public void GoNextLevel()
    {
        if(playClick)
        {
            GameObject.Find("AudioManager").GetComponent<AudioManager>().PlayMouseClick();
        }
        GameMaster.GoToNextLevel();
        this.gameObject.SetActive(false);

        if(incrementVoiceOver)
        {
            VoiceOverManager voManager = GameObject.Find("AudioManager").GetComponent<VoiceOverManager>();
            voManager.IncrementStage();
        }
    }
}
