    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConversationMaster : MonoBehaviour
{
    public GameObject otherSpeech;
    public GameObject playerSpeech;
    public GameObject playerSpeechChild;
    private Animator otherAnimator;
    private Animator playerAnimator;
    private Animator playerChildAnimator;
    public int maxPuzzleStage;
    [Header("Pls do not touch all these variables on runtime")]
    public bool isPlayerTurnToSpeak = false;
    public int otherStage = 0;
    private int playerStage = 2;
    public int puzzleStage = 0;
    private bool endOfConversation = false;
    [Header("How long before play next animation stage")]

    public Vector3[] otherDurations;
    private float otherTimer = 0.0f;
    private float playerTimer = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        otherAnimator = otherSpeech.GetComponent<Animator>();
        playerAnimator = playerSpeech.GetComponent<Animator>();
        playerChildAnimator = playerSpeechChild.GetComponent<Animator>();
        //TriggerNextAnimationState(otherAnimator, 2);
        otherTimer = otherDurations[0].x;
    }

    // Update is called once per frame
    void Update()
    {
        if (endOfConversation) return;

        //progress the other person speech bubble animation
        if(otherTimer > 0.0f)
        {
            otherTimer -= Time.deltaTime;
            if(otherTimer <= 0.0f)
            {
                TriggerNextAnimationState(otherAnimator, otherStage++);
                AdvanceOtherAnimationStage();
            }
        }
        
        if(isPlayerTurnToSpeak)
        {
            playerSpeech.GetComponent<SpriteMask>().enabled = true;
            if (puzzleStage > 0)
            {
                playerSpeechChild = GetComponent<PuzzleMaster_2a_Conversation>().Set_NextMask(puzzleStage);
                playerSpeechChild.SetActive(true);
                playerChildAnimator = playerSpeechChild.GetComponent<Animator>();
            }

            //Debug.Log(playerStage);
            //first time
            if (playerStage == 2)
            {
                TriggerNextAnimationState(playerAnimator, playerStage);
                TriggerNextAnimationState(playerChildAnimator, playerStage++);
                AdvancePlayerAnimationStage();      //3 ->0
            }
        }
        if (!isPlayerTurnToSpeak)
        {
            if (playerStage == 1)
            {
                //wait until player talking done
                if (playerTimer > 0.0f)
                {
                    playerTimer -= Time.deltaTime;
                    if (playerTimer <= 0.0f)
                    {
                        playerTimer = 0.0f;
                        //Disappear from screen
                        TriggerNextAnimationState(playerAnimator, playerStage);
                        TriggerNextAnimationState(playerChildAnimator, playerStage++);
                        AdvancePlayerAnimationStage();
                    }
                }
            }
        }
    }

    public void PlayerFinish()
    {
        TriggerNextAnimationState(otherAnimator, otherStage++);
        playerTimer = otherDurations[puzzleStage].z;
        TriggerNextAnimationState(playerAnimator, playerStage);
        TriggerNextAnimationState(playerChildAnimator, playerStage++);
        if (puzzleStage >= maxPuzzleStage)
        {
            //End of story act
            endOfConversation = true;
            return;
        }

        AdvanceOtherAnimationStage();
        AdvancePlayerAnimationStage();
    }

    private void TriggerNextAnimationState(Animator animator, int stageNumber)
    {
        if(stageNumber == 0)
        {
            animator.SetTrigger("CanListen");
        }
        else if (stageNumber == 1)
        {
            animator.SetTrigger("CanMoveUp");
        }
        else if (stageNumber == 2)
        {
            if (animator == otherAnimator && puzzleStage + 1 >= maxPuzzleStage)
            {
                return;
            }
            animator.SetTrigger("CanReappear");
        }
    }

    private void AdvanceOtherAnimationStage()
    {
        if(otherStage == 1)
        {
            //disable old puzzle piece for player
            if(puzzleStage >0)
            {
                playerSpeechChild.transform.GetChild(0).gameObject.SetActive(false);
                playerSpeechChild.transform.position = new Vector2(0, 100);     //send out of screen
            }


            otherTimer = otherDurations[puzzleStage].y;
            isPlayerTurnToSpeak = true;
        }
        else if(otherStage == 2)
        {
            isPlayerTurnToSpeak = false;
            otherTimer = otherDurations[puzzleStage].z;
        }
        else if(otherStage == 3)
        {
            otherTimer = otherDurations[puzzleStage].x;
            otherStage = 0;
            ++puzzleStage;
            if(puzzleStage == 4)
            {
                VoiceOverManager voManager = GameObject.Find("AudioManager").GetComponent<VoiceOverManager>();
                voManager.IncrementStage();
            }


            if (puzzleStage >= maxPuzzleStage)
            {
                //End of story act
                endOfConversation = true;
                return;
            }
        }
       
    }

    private void AdvancePlayerAnimationStage()
    {
        if (playerStage == 1)
        {
        }
        else if (playerStage == 2)
        {
        }
        else if (playerStage == 3)
        {
            playerStage = 0;
        }

    }
}
