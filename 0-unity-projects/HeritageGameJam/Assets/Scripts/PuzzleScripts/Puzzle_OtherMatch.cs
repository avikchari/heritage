using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle_OtherMatch : PuzzlePiece_Base
{
    public GameObject developerTools;
    public GameObject puzzleMaster;
    public GameObject matchingPieceRef;

    [Header("Other Match values")]
    [SerializeField]
    public Vector2 startPos;
    public float matchupLeyway;
    public bool isLockedAfterCorrect = false;
    private bool isPlayerSelecting = false;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (developerTools.activeSelf && isPlayerSelecting)
        {
            Debug.Log(this.gameObject.name + " Current Position: " + transform.position);
        }
    }

    public void PlayerIsSelecting(bool state)
    {
        isPlayerSelecting = state;
    }

    public override void Reset()
    {
        transform.localPosition = startPos;
        GetComponent<BoxCollider2D>().enabled = true;
        InformMaster_Succeed(false);
    }

    public void CheckIfDroppedSucceed()
    {
        float kiteDiffY = transform.position.y - matchingPieceRef.transform.position.y;
        Debug.Log("kiteDiffY: " + kiteDiffY);
        if (kiteDiffY < matchupLeyway || kiteDiffY > -matchupLeyway)
        {
            if (isLockedAfterCorrect)
            {
                GetComponent<BoxCollider2D>().enabled = false;
            }
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
