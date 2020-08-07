﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece_ExactMatch : PuzzlePiece_Base
{
    public GameObject developerTools;
    public GameObject puzzleMaster;

    [Header("Exact Match values")]
    [SerializeField]
    public Vector2 startPos;
    [SerializeField]
    public Vector2 correctPos;
    public float snappingLeyway;
    public bool isLockedAfterCorrect = true;
    private bool isPlayerSelecting = false;
    [Header("0 = Mom, 1 = Dad, 2 = Ari")]
    public int musicTrack;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    
    // Update is called once per frame
    void Update()
    {
        if (developerTools.activeSelf && isPlayerSelecting)
        {
            if (Input.GetKey(KeyCode.S))
            {
                Debug.Log(this.gameObject.name + " Start Position: " + transform.position);
            }
            else if (Input.GetKey(KeyCode.F))
            {
                Debug.Log(this.gameObject.name + " Final Position: " + transform.position);
            }
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
        if (Vector2.Distance(transform.position, correctPos) < snappingLeyway)
        {
            if (isLockedAfterCorrect)
            {
                GetComponent<BoxCollider2D>().enabled = false;
            }
            transform.position = correctPos;
            InformMaster_Succeed(true);
            if (musicTrack == 0) puzzleMaster.GetComponent<AudioPuzzle1>().MomPuzzleSolved();
            if (musicTrack == 1)puzzleMaster.GetComponent<AudioPuzzle1>().DadPuzzleSolved();
            if (musicTrack == 2) puzzleMaster.GetComponent<AudioPuzzle1>().AriPuzzleSolved();
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

    public void DragSoundProximity()
    {
        float originalDist = Vector2.Distance(startPos, correctPos);
        float currDist = Vector2.Distance(transform.position, correctPos);
        if (originalDist - currDist > 0)
        {
            float percentage = currDist / originalDist;
            AudioManager audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
            audioManager.AdjustFxLowPassFilter(1.0f - percentage, 1);
        }


    }
}
