using System;
using UnityEngine;


public class InputManager : MonoBehaviour, IActionInput
{
    //references to all input devices
    public MoveJoystickController moveJoystick;
    public LookJoystickController lookJoystick;
    public KeyboardInputReceiver keyboardInputReceiver;

    //events wired to outside listeners in this case the player controller
    public event Action<Vector2> OnMovement;
    public event Action<Vector2> OnLook;
    public event Action<Vector2> OnRotateBody;

    public Vector2 MovementValue { get; private set; }
    public Vector2 LookValue { get; private set; }

    private void Start()
    {
        moveJoystick.OnMove += Move;
        lookJoystick.OnMove += Look;
        keyboardInputReceiver.OnKeyboardMovement += Move;
    }

    private void Move(Vector2 _moveInput)
    { 
        MovementValue = _moveInput;
        OnMovement?.Invoke(MovementValue);
    }
    
    private void Look(Vector2 _lookInput)
    {   
        LookValue = _lookInput;
        OnLook?.Invoke(LookValue);
        OnRotateBody?.Invoke(LookValue);
    }
}

