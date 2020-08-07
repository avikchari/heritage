using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleMaster_4_Music : PuzzleMaster_Base
{
    public List<PuzzlePieceData> puzzleManagement;
    public bool overallSolved = false;
    public GameObject nextSceneObj;
    public GameObject[] userinterfaces;
    // Start is called before the first frame update
    void Start()
    {
        foreach (PuzzlePieceData data in puzzleManagement)
        {
            //data.puzzlePieceObj.GetComponent<PuzzlePiece_Base>().Reset();
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
        bool letsBeOptimisic = true;
        foreach (PuzzlePieceData data in puzzleManagement)
        {
            if(source == data.puzzlePieceObj)
            {
                data.successState = successState;
            }

            //Puzzle is solved only if all pieces are solved
            if (letsBeOptimisic && data.successState == false) letsBeOptimisic = false;
        }

        if (letsBeOptimisic)
        {
            userinterfaces[0].SetActive(false);
            userinterfaces[1].SetActive(false);
            userinterfaces[2].SetActive(false);
            userinterfaces[3].SetActive(false);
            overallSolved = true;
            nextSceneObj.SetActive(true);
            Debug.Log("Yay You finish the puzzle");
        }
    }

}
