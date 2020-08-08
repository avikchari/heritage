using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleMaster_3_Kite : PuzzleMaster_Base
{
    public List<PuzzlePieceData> puzzleManagement;
    public bool overallSolved = false;
    public GameObject nextSceneObj;
    public int extendGameplay = 1;         //sorry grace... need to extend to match voice lines
    private int extendCounter = 0;



    // Start is called before the first frame update
    void Start()
    {
        foreach (PuzzlePieceData data in puzzleManagement)
        {
            data.puzzlePieceObj.GetComponent<PuzzlePiece_Base>().Reset();
        }
        overallSolved = false;
        nextSceneObj.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void RecievePuzzle_Succeed(bool successState, GameObject source)
    {
        if (successState)
        {
            GetComponent<AudioPuzzle3>().KiteMatchRepeat();
            extendCounter++;

            if (extendCounter >= extendGameplay)
            {

                overallSolved = true;
                nextSceneObj.SetActive(true);
                Debug.Log("Yay You finish the puzzle");
            }
            else
            {
                if (extendCounter == extendGameplay / 2)
                {
                    GetComponent<AudioPuzzle3>().KiteMatchAudio();
                }

                //To make the height change more obvious
                int randomKite = extendCounter % 2;//Mathf.FloorToInt(Random.Range(0, 2.0f));
                Vector3 originalPosition = puzzleManagement[randomKite].puzzlePieceObj.transform.position;
                float randomYHeight;
                float maxRange;
                //random y height (0 to -6)
                if (originalPosition.y > -3.0f)     //top half
                {
                    maxRange = -6.0f - originalPosition.y;
                    randomYHeight = Random.Range(maxRange, -2.0f);      //move downwards
                }
                else                                //bottom half
                {
                    maxRange = 0 - originalPosition.y;
                    randomYHeight = Random.Range(2.0f, maxRange);      //move upwards
                }
                float newYHeight = originalPosition.y + randomYHeight;
                Vector3 newPosition = new Vector3(originalPosition.x, newYHeight, originalPosition.z);

                //This method so that the trail renderer will generate within one frame by faking movement
                float incremental = (newPosition.y - originalPosition.y)/20;
                for (int i = 0; i < 20; ++i)
                {
                    puzzleManagement[randomKite].puzzlePieceObj.transform.position += (Vector3.up * incremental);
                }
            }
        }
    }
}
