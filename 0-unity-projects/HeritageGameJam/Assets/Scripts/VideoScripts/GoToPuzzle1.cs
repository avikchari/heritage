using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GoToPuzzle1 : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject start;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        UnityEngine.Video.VideoPlayer player = start.GetComponent<UnityEngine.Video.VideoPlayer>();
        if(player.isPrepared)
        {
            if (!player.isPlaying)
            {
                GetComponent<GoNextScene>().GoNextLevel();
                //start.SetActive(false);
            }
        }

    }
}
