using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece_ShapeMatch : PuzzlePiece_Base
{
    public GameObject puzzleMaster;

    [Header("Exact Match values")]
    [SerializeField]
    public bool isLockedAfterCorrect = true;
    private bool isPlayerSelecting = false;
    public PuzzleMaster_2a_Conversation.shapesType pieceShape;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    
    // Update is called once per frame
    void Update()
    {
    }

    public void PlayerIsSelecting(bool state)
    {
        isPlayerSelecting = state;
    }
    
    public override void Reset()
    {
        GetComponent<BoxCollider2D>().enabled = true;
        //InformMaster_Succeed(false);
    }

    public void CheckIfDroppedSucceed()
    {
        Vector2 maskPosition = puzzleMaster.GetComponent<PuzzleMaster_2a_Conversation>().GetMaskPosition();
        if (Vector2.Distance(transform.position, maskPosition) < 0.2f)
        {
            transform.position = maskPosition;
            //make puzzle shape move along with speech bubble
            puzzleMaster.GetComponent<PuzzleMaster_2a_Conversation>().SetPlayerShapeAsChild(this.gameObject);
            if(isLockedAfterCorrect)
            {
                GetComponent<BoxCollider2D>().enabled = false;
            }

            CheckIfCorrectShape();
        }
    }

    void CheckIfCorrectShape()
    {
        PuzzleMaster_2a_Conversation.shapesType correctShape = puzzleMaster.GetComponent<PuzzleMaster_2a_Conversation>().GetCurrentPuzzleShape();
        if(pieceShape == correctShape)
        {
            InformMaster_Succeed(true);
        }
        else
        {
            InformMaster_Succeed(false);
        }
    }

    void InformMaster_Succeed(bool successState)
    {
        //calls virtual functino on puzzle manager
        puzzleMaster.GetComponent<PuzzleMaster_Base>().RecievePuzzle_Succeed(successState, this.gameObject);
    }
}
