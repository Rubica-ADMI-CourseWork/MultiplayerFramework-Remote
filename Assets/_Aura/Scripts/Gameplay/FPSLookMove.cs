using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles FPS movement and Looking with Keyboard and mouse
/// </summary>
[RequireComponent(typeof(CharacterController))]
public class FPSLookMove : MonoBehaviour
{
    [Header("Movement related Fields")]
    [SerializeField]float moveSpeed;

    //caches of movement input in world then local space
    Vector2 moveDirection;
    Vector3 moveDirectionLocal;

    //reference to Character Controller Component
    CharacterController characterController;

    [Header("Look related Fields")]
    [SerializeField] Transform eyePosition;
    [SerializeField] float lookSideWaysSensitivity;
    [SerializeField] float lookSensitivityMultiplier;
    [SerializeField]float lookUpDownSensitivity;
    [SerializeField, Range(-40f, -50f)] float maxLookUp = -45f;
    [SerializeField, Range(40f, 50f)] float maxLookDown = 45f;

    //caches of head and body rotation
    float bodyTurnValue;
    float lookUpDownValue;

    //state flags
    bool inAir = false;
    [SerializeField] float jumpForce = 20f;
    [SerializeField] float gravityModifier = 4f;

    public bool isGrounded { get; private set; }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        characterController = GetComponent<CharacterController>();
    }
    private void Update()
    {
        Look();

        Move();
    }

    private void Look()
    {
        //body rotation
        float moseXAxisInput = Input.GetAxisRaw("Mouse X") * lookSideWaysSensitivity;
        bodyTurnValue += moseXAxisInput * lookSensitivityMultiplier * Time.deltaTime;
        Quaternion bodyTurnRotation = Quaternion.Euler(new Vector3(0f, bodyTurnValue, 0f));
        transform.rotation = bodyTurnRotation;

        //head rotation
        float mouseYAxisInput = Input.GetAxisRaw("Mouse Y") * lookUpDownSensitivity;
        lookUpDownValue -= mouseYAxisInput * lookSensitivityMultiplier * Time.deltaTime;
        lookUpDownValue = Mathf.Clamp(lookUpDownValue, maxLookUp, maxLookDown);

        Quaternion headTurnRotation = Quaternion.Euler(new Vector3(lookUpDownValue, 0f, 0f));
        eyePosition.localRotation = headTurnRotation;
    }

    private void Move()
    {
        float xAxisInput = Input.GetAxisRaw("Horizontal");
        float yAxisInput = Input.GetAxisRaw("Vertical");

        moveDirection = new Vector2(xAxisInput, yAxisInput);
        Vector3 forwardMovement = transform.forward * moveDirection.y;
        Vector3 sideToSideMovement = transform.right * moveDirection.x;

        float yVal = moveDirectionLocal.y;

        moveDirectionLocal = (forwardMovement + sideToSideMovement).normalized * moveSpeed;
        moveDirectionLocal.y = yVal;

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            moveDirectionLocal.y = jumpForce; 
        }

        moveDirectionLocal.y += Physics.gravity.y*gravityModifier * Time.deltaTime;

        Debug.Log(moveDirectionLocal);

        if (characterController.isGrounded)
        {
            moveDirectionLocal.y = 0;
        }

        characterController.Move(moveDirectionLocal * Time.deltaTime);
    }

    public void SetIsGrounded(bool isGrounded)
    {
        this.isGrounded = isGrounded;
    }
}
