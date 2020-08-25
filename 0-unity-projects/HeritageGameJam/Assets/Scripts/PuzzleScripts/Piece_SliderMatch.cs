using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Piece_SliderMatch : PuzzlePiece_Base
{
    public GameObject puzzleMaster;

    [Header("0.0f to 1.0f")]
    public float correctSlider;
    public GameObject targetBlur;
    public Material blurMaterial;

    [SerializeField]
    public bool isLockedAfterCorrect = true;
    private bool isPlayerSelecting = false;
    private float initalBlur;
    [Header("1,2,3,4")]
    public int musicalTrack;
    private float startingOffset = -9.0f;

    // Start is called before the first frame update
    void Start()
    {
        initalBlur = blurMaterial.GetFloat("_Size");
        OnSliderChanged();
    }
    
    // Update is called once per frame
    public void OnSliderChanged()
    {
        float sliderValue = GetComponent<Slider>().value;
        float offsetFromValue = sliderValue - correctSlider;

        if(Mathf.Abs(startingOffset) > 2.0f)
        {
            startingOffset = Mathf.Abs(offsetFromValue);
        }


        float mapValue = offsetFromValue * (20.0f);
        blurMaterial.SetFloat("_Size", mapValue);

        float linearVolume = 1.0f - (Mathf.Abs(offsetFromValue) / startingOffset);         //gets closes to 1.0f volume 
        linearVolume = (linearVolume < 0.0f ? 0.0f : linearVolume);


        puzzleMaster.GetComponent<AudioPuzzle4>().AdjustVolume(musicalTrack, linearVolume);

        if (Mathf.Abs(offsetFromValue) < 0.05f)
        {
            //targetBlur.SetActive(false);
            InformMaster_Succeed(true);
        }
        else
        {
            InformMaster_Succeed(false);
        }


    }

    public void ForceSetCorrectValue()
    {
        blurMaterial.SetFloat("_Size", 0);
    }

    public void PlayerIsSelecting(bool state)
    {
        isPlayerSelecting = state;
    }
    
    public override void Reset()
    {
        //GetComponent<BoxCollider2D>().enabled = true;
        //InformMaster_Succeed(false);
    }

    void InformMaster_Succeed(bool successState)
    {
        //calls virtual functino on puzzle manager
        puzzleMaster.GetComponent<PuzzleMaster_Base>().RecievePuzzle_Succeed(successState, this.gameObject);
    }
}
