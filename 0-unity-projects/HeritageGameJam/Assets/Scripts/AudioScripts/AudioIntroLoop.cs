using UnityEngine;
using System.Collections;
 
public class AudioIntroLoop : MonoBehaviour
{
    public AudioSource Intro;
    public AudioSource Loop;
    public AudioClip introClip;
    public AudioClip loopClip;

    public float introClipTime = 4.8f;

    void Start()
    {
        Loop.loop = true;
        StartCoroutine(playMenuMusic());
    }
 
    IEnumerator playMenuMusic()
    {
        Intro.clip = introClip;
        Intro.Play();
        yield return new WaitForSeconds(introClipTime);
        Loop.clip = loopClip;
        Loop.Play();
    }
}