using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float walkSpeed = 5f;
    [SerializeField]
    private float rotateSpeed = 0.5f;
    [SerializeField]
    private float inputDeadband = 0.05f;
    [SerializeField]
    private float pushPower = 2f;

    private CharacterController characterController;
    private Vector2 inputVector;

    private int collisionCounter = 0;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    void FixedUpdate()
    {
        var inputMagnitude = inputVector.sqrMagnitude;

        if (inputMagnitude > inputDeadband)
        {
            var move = new Vector3(inputVector.x, 0, inputVector.y);

            transform.rotation = Quaternion.Slerp(
                transform.rotation, Quaternion.LookRotation(move), rotateSpeed);

            characterController.SimpleMove(move * walkSpeed);
        }
    }

    void OnMove(InputValue input)
    {
        inputVector = input.Get<Vector2>();
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Rigidbody other = hit.collider.attachedRigidbody;

        // no rigidbody
        if (other == null || other.isKinematic)
        {
            return;
        }

        // We dont want to push objects below us
        if (hit.moveDirection.y < -0.3f)
        {
            return;
        }

        // Apply the push
        other.AddForceAtPosition(hit.controller.velocity * pushPower, hit.point);
        Debug.DrawRay(hit.point, hit.controller.velocity * pushPower, Color.red, 3f);
        Debug.Log("collision! " + collisionCounter++);
    }
}
