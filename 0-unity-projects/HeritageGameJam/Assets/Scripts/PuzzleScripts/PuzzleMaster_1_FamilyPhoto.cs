using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleMaster_1_FamilyPhoto : PuzzleMaster_Base
{
    public Vector2 photoDimensions;
    public Vector2 photoPivot;
    public List<PuzzlePieceData> puzzleManagement;
    public bool overallSolved = false;
    public GameObject nextSceneObj;

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
        Gizmos.DrawWireCube(photoPivot, photoDimensions);
        Gizmos.DrawLine(photoPivot - (photoDimensions * 0.5f), photoPivot + (photoDimensions * 0.5f));
    }
}
