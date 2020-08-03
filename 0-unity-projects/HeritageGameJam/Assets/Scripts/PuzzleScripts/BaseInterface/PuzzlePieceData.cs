using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Just a data struct to observe the state of puzzle pieces
[System.Serializable]
public class PuzzlePieceData
{
    public GameObject puzzlePieceObj;
    public enum puzzleTypeEnum { matchToExactLocation };
    public puzzleTypeEnum puzzleType;
    public bool successState;
}
