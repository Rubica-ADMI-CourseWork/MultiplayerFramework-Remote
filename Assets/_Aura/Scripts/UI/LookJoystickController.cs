using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class LookJoystickController : MonoBehaviour,IDragHandler,IPointerDownHandler,IPointerUpHandler
{
    //reference to the RectTransform of the knob
    RectTransform knobTransform;

    //percentage movement beyond which we accept input in any particular axis
    [SerializeField] float dragThreshold = 0.6f;

    //the distance that the knob moves about on the screen
    [SerializeField] int dragMovementDistance;

    //a modifier for drag distance to convert movement in pixels
    [SerializeField] int dragOffsetDistance = 100;

    public bool acceptDiagonalInput = false;

    public event Action<Vector2> OnMove;

    private void Awake()
    {
        knobTransform = GetComponent<RectTransform>();
    }
    #region Interface Implementations

    public void OnPointerDown(PointerEventData eventData)
    {

    }

    public void OnPointerUp(PointerEventData eventData)
    {
        //when you lift up the finger, return knob to original anchored position
        knobTransform.anchoredPosition = Vector2.zero;

        //pass a zero value input to whoever is listening
        OnMove?.Invoke(Vector3.zero);
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 offset;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            knobTransform,
            eventData.position,
            null,
            out offset
            );
        offset = Vector2.ClampMagnitude(offset, dragOffsetDistance) / dragOffsetDistance;
        knobTransform.anchoredPosition = offset * dragMovementDistance;
        Vector2 InputVector = CalculateMovementVector(offset);

        OnMove?.Invoke(InputVector);

        Debug.Log(InputVector);
    }

    private Vector2 CalculateMovementVector(Vector2 offset)
    {
        //if we are not accepting diagonal input then use dragThreshold to clamp input
        if (acceptDiagonalInput == false)
        {
            float X = Mathf.Abs(offset.x) > dragThreshold ? offset.x : 0;
            float y = Mathf.Abs(offset.y) > dragThreshold ? offset.y : 0;

            return new Vector2(X, y);
        }
        else
        {
            return offset;
        }

    }

    #endregion
}
