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

    private CharacterController characterController;
    private Vector2 inputVector;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    void FixedUpdate()
    {
        var inputMagnitude = inputVector.sqrMagnitude;

        if (inputMagnitude > inputDeadband) {
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
}
