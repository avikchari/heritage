using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioEndPuzzle1 : MonoBehaviour
{

    public float fadeOutTime;
    // Start is called before the first frame update
    void Start()
    {
        GameObject.Find("AudioManager").GetComponent<AudioManager>().StartFadeOut(0, fadeOutTime);
        GameObject.Find("AudioManager").GetComponent<AudioManager>().StartFadeOut(1, fadeOutTime);
        GameObject.Find("AudioManager").GetComponent<AudioManager>().StartFadeOut(2, fadeOutTime);
        GameObject.Find("AudioManager").GetComponent<AudioManager>().StartFadeOut(3, fadeOutTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
