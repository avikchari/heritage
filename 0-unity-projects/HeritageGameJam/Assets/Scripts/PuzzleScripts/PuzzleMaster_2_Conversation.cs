using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleMaster_2_Conversation : PuzzleMaster_Base
{
    public int numOfPlayerConversations;
    public List<PuzzlePieceData> puzzleManagement;
    public bool overallSolved = false;
    public GameObject nextSceneObj;

    // Start is called before the first frame update
    void Start()
    {

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
            overallSolved = true;
            nextSceneObj.SetActive(true);
            Debug.Log("Yay You finish the puzzle");
        }
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
    }
}
