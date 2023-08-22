using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovementPlayer : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float jumpStrength;
    private Rigidbody2D rb;
    private InputControls inputs;

    private void OnEnable()
    {
        inputs.Enable();
    }

    private void OnDisable()
    {
        inputs.Disable();
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        inputs = new InputControls();
    }

    // Update is called once per frame
    void Update()
    {
        Move(inputs.PlayerControlMovement.Move.ReadValue<Vector2>());
        Jump(inputs.PlayerControlMovement.Jump.ReadValue<float>());
    }

    private void Move(Vector2 movementDirection)
    {
        rb.velocity = new Vector3(movementDirection.x,movementDirection.y, 0) * speed;
    }

    private void Jump(float action)
    {
        if (action == 1)
        {
            rb.AddForce(new Vector2(0, 1) * jumpStrength);
        }
    }

}
