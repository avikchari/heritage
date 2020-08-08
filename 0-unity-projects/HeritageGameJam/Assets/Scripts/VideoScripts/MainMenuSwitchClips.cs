using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenuSwitchClips : MonoBehaviour
{
    //public UnityEngine.Video.VideoClip start;
    //public UnityEngine.Video.VideoClip loop;
    public GameObject start;
    public GameObject loop;
    private bool canGoNext = false;
    private bool registeredGoNext = false;
    // Start is called before the first frame update
    void Start()
    {

    }
    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if(!canGoNext)
        {
            start.SetActive(true);
            loop.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        UnityEngine.Video.VideoPlayer player = start.GetComponent<UnityEngine.Video.VideoPlayer>();
        if(player.isPrepared)
        {
            if (!player.isPlaying)
            {
                start.SetActive(false);
                //player.Stop();
                //player.clip = loop;
                //player.Play();
                //player.isLooping = true;
                canGoNext = true;
            }
        }

        if(Input.anyKeyDown)
        {
            registeredGoNext = true;
        }
        if( registeredGoNext  && canGoNext)
        {
            loop.SetActive(false);
            start.SetActive(false);
            GetComponent<GoNextScene>().GoNextLevel();
            GameObject.Find("AudioManager").GetComponent<AudioManager>().StartFadeOut(0, 7.0f);
            GameObject.Find("AudioManager").GetComponent<AudioManager>().StartFadeOut(1, 7.0f);
        }

    }
}
