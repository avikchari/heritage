using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //private variables
    private Vector3 _newPositionOffset = Vector3.zero;
    private bool _canJump = false;

    //--------------------------------------------------------------------------------------------------

    //public variables
    [Header("Player Movement")]
    [Range(0.0f, 99.0f)]
    public float walkSpeed = 5;
    //public bool CanJumpStatus //{ get { return _canJump; } }

    

    // Start is called before the first frame update
    void Start()
    {
        _canJump = false;
    }

    // Update is called once per frame
    void Update()
    {
        //basic horizontal movement for now... no friction or acceleration
        PlayerInput();
        MovementLogic();
        PerformMovement();
    }

    void PlayerInput()
    {
        if (Input.GetKey(KeyCode.D))
        {
            _newPositionOffset += Vector3.right * Time.deltaTime * walkSpeed;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            _newPositionOffset += Vector3.left * Time.deltaTime * walkSpeed;
        }

        if(Input.GetKeyDown(KeyCode.Space) && _canJump)
        {

        }
    }

    void MovementLogic()
    {

    }

    void PerformMovement()
    {

        GetComponent<Transform>().position += _newPositionOffset;

        //reset position at end of every loop
        _newPositionOffset = Vector3.zero;
    }
}
