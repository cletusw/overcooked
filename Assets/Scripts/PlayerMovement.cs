using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float movementSpeed = 5f;

    private CharacterController characterController;
    private Vector2 inputVector;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    void FixedUpdate()
    {
        characterController.SimpleMove(
            new Vector3(inputVector.x, 0, inputVector.y) * movementSpeed);
    }

    void OnMove(InputValue input)
    {
        inputVector = input.Get<Vector2>();
    }
}
