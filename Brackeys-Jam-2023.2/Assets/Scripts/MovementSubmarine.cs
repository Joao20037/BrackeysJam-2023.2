using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementSubmarine : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] public float deepLimit;
    private Rigidbody2D rb;
    private InputControls inputs;
    private float x_scale;
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
        x_scale = transform.localScale.x;
    }


    // Update is called once per frame
    void Update()
    {
        Move(inputs.SubmarinoControl.Move.ReadValue<Vector2>());

        if (rb.velocity.x < 0)
        {
            if (transform.localScale.x < 0) x_scale = -transform.localScale.x;
            transform.localScale = new Vector3(x_scale, transform.localScale.y, transform.localScale.z);
        }
        else if (rb.velocity.x > 0)
        {
            if (transform.localScale.x > 0) x_scale = -transform.localScale.x;
            transform.localScale = new Vector3(x_scale, transform.localScale.y, transform.localScale.z);

        }


        if (transform.position.y < -deepLimit)
        {
            transform.position = new Vector3(transform.position.x, - (deepLimit), transform.position.z);
        }


    }

    public void UpdateDeepLimit(float newDeepLimit)
    {
        this.deepLimit += newDeepLimit;
    }

    private void Move(Vector2 movementDirection)
    {
        if(movementDirection.y < 0)
        {
           
            if (transform.position.y < - deepLimit)
            {
                return;
            }
            else
            {
                rb.velocity = new Vector3(movementDirection.x, movementDirection.y, 0) * speed;
            }
        }
        else
        {
            rb.velocity = new Vector3(movementDirection.x, movementDirection.y, 0) * speed;
        }
    }
}
