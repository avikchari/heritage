using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleMaster_2a_Conversation : PuzzleMaster_Base
{
    public List<PuzzlePieceData> puzzleManagement;
    public bool overallSolved = false;
    public GameObject nextSceneObj;
    public int activeConversation = 0;
    public enum shapesType { rectangle, star, triangle, circle, pentagon };
    public shapesType[] correctShape;
    public GameObject[] possibleShapes;

    [Header("ChangeColor if wrong choice")]
    public Color neutralSpeech;
    public Color correctSpeech;
    public Color failSpeech;
    public GameObject outerBubble;

    // Start is called before the first frame update
    void Start()
    {
        foreach (PuzzlePieceData data in puzzleManagement)
        {
            data.successState = false;
        }
        foreach (GameObject shapes in possibleShapes)
        {
            foreach (Transform child in shapes.GetComponentInChildren<Transform>())
            {
                child.gameObject.GetComponent<PuzzlePiece_Base>().Reset();
            }
        }
        overallSolved = false;
        nextSceneObj.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public Vector2 GetMaskPosition()
    {
        return puzzleManagement[activeConversation].puzzlePieceObj.transform.position;
    }

    public void SetPlayerShapeAsChild(GameObject shape)
    {
        shape.transform.SetParent(puzzleManagement[activeConversation].puzzlePieceObj.transform);
        puzzleManagement[activeConversation].puzzlePieceObj.GetComponent<SpriteRenderer>().enabled = false;
        outerBubble.GetComponent<SpriteMask>().enabled = false;
    }

    public shapesType GetCurrentPuzzleShape()
    {
        return correctShape[activeConversation];
    }

    private void SpeechBubbleSuccessFeedback(bool successState)
    {
        GetComponent<ConversationMaster>().PlayerFinish();
        //disable animator since its affecting color
        //outerBubble.GetComponent<Animator>().enabled = false;
        //puzzleManagement[activeConversation].puzzlePieceObj.GetComponent<Animator>().enabled = false;
        
        if (successState)
        {
            outerBubble.GetComponent<SpriteRenderer>().color = correctSpeech;
            puzzleManagement[activeConversation].puzzlePieceObj.GetComponent<SpriteRenderer>().color = correctSpeech;
        }
        else
        {
            outerBubble.GetComponent<SpriteRenderer>().color = failSpeech;
            puzzleManagement[activeConversation].puzzlePieceObj.GetComponent<SpriteRenderer>().color = failSpeech;
        }
        
        //outerBubble.GetComponent<Animator>().enabled = true;
        //puzzleManagement[activeConversation].puzzlePieceObj.GetComponent<Animator>().enabled = true;
    }
    public GameObject Set_NextMask(int puzzleStage)
    {
        puzzleManagement[puzzleStage].puzzlePieceObj.SetActive(true);
        outerBubble.GetComponent<SpriteRenderer>().color = neutralSpeech;
        puzzleManagement[activeConversation].puzzlePieceObj.GetComponent<SpriteRenderer>().color = neutralSpeech;

        possibleShapes[activeConversation].SetActive(true);


        return puzzleManagement[puzzleStage].puzzlePieceObj;
    }
    private void AdvanceNextConversation()
    {
        possibleShapes[activeConversation].SetActive(false);
        ++activeConversation;
    }

    public override void RecievePuzzle_Succeed(bool successState, GameObject source)
    {
        //conversation will continue regardlss of fail or succeed
        SpeechBubbleSuccessFeedback(successState);
        AdvanceNextConversation();

        if (puzzleManagement.Count <= activeConversation)
        {
            overallSolved = true;
            nextSceneObj.SetActive(true);
            Debug.Log("Yay You finish the puzzle");
        }
    }
}
