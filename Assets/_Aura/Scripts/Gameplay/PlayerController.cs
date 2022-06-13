using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ControlMode
{
    KeyboardMouse,
    Joystick
}

public class PlayerController : MonoBehaviour
{
    [Header("Control option")]
    public ControlMode controlOption;

    [Header("Look related fields.")]
    [SerializeField] float LookSensitivity;
    [SerializeField] Transform playerHead;
    [SerializeField] Transform eyePosition;
    [SerializeField] float minHeadRot;
    [SerializeField] float maxHeadRot;
    Transform playerEyes;

    [Header("Move related fields")]
    [SerializeField] float moveSpeed;

    //cache of movement direction aquired from keyboard input
    Vector3 moveDirection, moveDirectionLocal;
    //reference to character controller component on the Player Gameobject
    CharacterController characterController;

    //Joystick input related Code
    [Header("Reference to Look Joystick")]
    LookJoystickController lookJoystick;
    bool isLookingUp;
    bool isLookingDown;
    bool isTurning;
    float turnValue = 1;

    //Cache for rotation input coming from the mouse
    Vector2 rotationInput;

    //seperate cache of input in the Y axis coming from the mouse
    float headRotValue;

    public bool IsGrounded { get; private set; }

    private void Awake()
    {
        lookJoystick = FindObjectOfType<LookJoystickController>();
        characterController = GetComponent<CharacterController>();
       
        //playerEyes = transform.Find("Eyes");
    }
    private void Start()
    {
        //listen for input from joystick to control look functionality
        lookJoystick.OnMove += SetLookUpDown;
        lookJoystick.OnMove += SetLookSideways;
    }

    private void Update()
    {
        switch (controlOption)
        {
            case ControlMode.KeyboardMouse:
                break;
            case ControlMode.Joystick:
                break;
        }

        //get input from mouse
        rotationInput = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y")) * LookSensitivity;

        //LookWithJoystick();

        LookSideways();
        LookUpDown();

        //get input from Keyboard and cache it in the moveDirection variable.
        float xValue = Input.GetAxisRaw("Horizontal") ;
        float zValue = Input.GetAxisRaw("Vertical") ;

        moveDirection = new Vector3(xValue, 0f, zValue);
       
        float yVel = moveDirectionLocal.y;
        moveDirectionLocal = ((transform.forward * moveDirection.z) + (transform.right * moveDirection.x)).normalized * moveSpeed;
        moveDirectionLocal.y = yVel;
      
      
        if (IsGrounded)
        {
            Debug.Log("Grounded!");
            moveDirectionLocal.y = 0f;
        }
         
        moveDirectionLocal.y += Physics.gravity.y * Time.deltaTime;
        
        //add this input to the position of the game object every frame
        characterController.Move( moveDirectionLocal * Time.deltaTime);
    }

    public void SetIsGrounded(bool trueFalse)
    {
        IsGrounded = trueFalse;
    }
    private void LateUpdate()
    {
        //playerEyes.position = eyePosition.position;
        //playerEyes.rotation = eyePosition.rotation;
    }
    private void LookUpDown()
    {
        //look up and down
        headRotValue -= rotationInput.y;//add on to the value every frame
        headRotValue = Mathf.Clamp(headRotValue, minHeadRot, maxHeadRot);//clamp it so we dont flip over
        Quaternion headRotation = Quaternion.Euler(new Vector3(headRotValue, 0f, 0f));//create a quaternion
        playerHead.localRotation = headRotation;//set the playerHead rotation every frame to the current headRotation Quaternion
    }
    private void LookSideways()
    {
        float xRotVal = transform.rotation.eulerAngles.x;
        float yRotVal = transform.rotation.eulerAngles.y + rotationInput.x;
        float zRotVal = transform.rotation.eulerAngles.z;
        transform.rotation = Quaternion.Euler(xRotVal, yRotVal, zRotVal);
    }
    private void LookWithJoystick()
    {
        if (isLookingUp)
        {
            headRotValue -= 0.5f * LookSensitivity;
        }
        if (isLookingDown)
        {
            headRotValue -= -0.5f * LookSensitivity;
        }

        if (isTurning)
        {
            transform.Rotate(Vector3.up * turnValue * 10f * Time.deltaTime);
        }
    }

    #region Listener Methods for Joystick Input
    private void SetLookUpDown(Vector2 compositeValue)
    {
        if (compositeValue.y < 0f)
        {
            isLookingUp = false;
            isLookingDown = true;
        }
        else if (compositeValue.y > 0f)
        {
            isLookingDown = false;
            isLookingUp = true;
        }
        else
        {
            isLookingDown = false;
            isLookingUp = false;
        }
    }

    private void SetLookSideways(Vector2 compositeValue)
    {
        if (compositeValue.x != 0f)
        {
            isTurning = true;
            if (compositeValue.x > 0.5f)
            {
                turnValue = 1;
            }
            else if (compositeValue.x < -0.5f)
            {
                turnValue = -1;
            }

        }
        else
        {
            isTurning = false;
            turnValue = 0;
        }

    }
    #endregion
}
