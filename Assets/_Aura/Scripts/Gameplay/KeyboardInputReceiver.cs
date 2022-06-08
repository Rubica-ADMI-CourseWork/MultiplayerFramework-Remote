using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class KeyboardInputReceiver : MonoBehaviour
{
    public bool GetKeyboardInput = false;
    Vector2 inputVector;

    public event Action<Vector2> OnKeyboardMovement;

    // Update is called once per frame
    void Update()
    {
        if (GetKeyboardInput == false) return;
        float horizontalInput = Input.GetAxis("Horizontal");
        float frontBackInput = Input.GetAxis("Vertical");

        inputVector = new Vector2(horizontalInput, frontBackInput);
        OnKeyboardMovement?.Invoke(inputVector);
    }


}
