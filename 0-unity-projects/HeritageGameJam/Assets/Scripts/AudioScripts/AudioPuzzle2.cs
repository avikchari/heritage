using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPuzzle2 : MonoBehaviour
{
    public AudioClip puzzle1Clip_1;
    public AudioClip puzzle1Clip_2;
    public AudioClip puzzle1Clip_3;
    public AudioClip[] chatComfirm;
    public float[] volumesOfClips;
    public float[] playAfterDelay;
    private bool hasplayed1 = false;
    private bool hasplayed2 = false;
    private float timer;
    // Start is called before the first frame update
    void Start()
    {
        AudioManager audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        audioManager.PlayTrack(puzzle1Clip_1, true, 0, volumesOfClips[0]);
        timer = 0.0f;
        hasplayed1 = false;
        hasplayed2 = false;
    }

    public void ConversationPart2()
    {
        AudioManager audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        audioManager.PlayTrack(puzzle1Clip_2, false, 1, volumesOfClips[1]);
        audioManager.StartFadeIn(1, volumesOfClips[1], 4.0f);
    }

    public void ConversationPart3()
    {
        AudioManager audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        audioManager.PlayTrack(puzzle1Clip_3, false, 2, volumesOfClips[2]);
        audioManager.StartFadeIn(2, volumesOfClips[2], 4.0f);
    }

    public void ConversationComfirm()
    {
        int randomIndex = Mathf.FloorToInt(Random.Range(0, chatComfirm.Length));

        AudioManager audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        audioManager.PlayTrack(chatComfirm[randomIndex], false, 5, 1.0f);
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(playAfterDelay[1] < timer && !hasplayed1)
        {
            hasplayed1 = true;
            ConversationPart2();
        }
        else if (playAfterDelay[2] < timer && !hasplayed2)
        {
            hasplayed2 = true;
            ConversationPart3();
        }
    }
}
