using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Common "interface" class for puzzle master
public abstract class PuzzleMaster_Base : MonoBehaviour
{
    public abstract void RecievePuzzle_Succeed(bool successState, GameObject source);
}
