using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public interface IActionInput 
{
    //has  properties to cache values coming in from input devices
    Vector2 MovementValue { get;}

    //has events to allow parties interested in inputs to listen
    event Action<Vector2> OnMovement;
}
