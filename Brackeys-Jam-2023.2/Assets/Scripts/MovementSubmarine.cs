using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementSubmarine : MonoBehaviour
{
    [SerializeField] private float speed;
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
        Move(inputs.SubmarinoControl.Move.ReadValue<Vector2>());
    }

    private void Move(Vector2 movementDirection)
    {
        rb.velocity = new Vector3(movementDirection.x, movementDirection.y, 0) * speed;
    }
}
