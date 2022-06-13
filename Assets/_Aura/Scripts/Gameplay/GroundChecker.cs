using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Casts a ray downwards,checking for ground hit
/// if ground hit, tells player controller script that character
/// isGrounded
/// </summary>
public class GroundChecker : MonoBehaviour
{
    [SerializeField] PlayerController playerController;
    [SerializeField] float rayCastDistance = 1f;
    [SerializeField] LayerMask ground;
    private void Update()
    {
        Ray ray = new Ray(transform.position, Vector3.down);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit, rayCastDistance, ground))
        {
            playerController.SetIsGrounded(true);
        }
        else
        {
            playerController.SetIsGrounded(false);
        }
    }
}
