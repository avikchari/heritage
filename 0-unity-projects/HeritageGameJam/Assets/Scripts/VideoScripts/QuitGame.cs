using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class QuitGame : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject start;
    public GameObject uiQuitButton;
    bool canQuit = false;

    void Start()
    {
        canQuit = false;
    }

    // Update is called once per frame
    void Update()
    {
        UnityEngine.Video.VideoPlayer player = start.GetComponent<UnityEngine.Video.VideoPlayer>();
        if(player.isPrepared)
        {
            if (!player.isPlaying)
            {
                uiQuitButton.SetActive(true);
                canQuit = true;
            }
        }

        if(Input.anyKeyDown && canQuit)
        {
            QuitTheGame();
        }
    }


    public void QuitTheGame()
    {
        Application.Quit();
    }
}
