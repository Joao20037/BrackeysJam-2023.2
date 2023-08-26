using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishBehavior : MonoBehaviour
{
    public float moveSpeed = 2f; // Velocidade de movimento do peixe
    public float raycastDistance = 1.5f; // Distância do raycast
    public float fleeDistance = 2f; // Distância para fugir de outros peixes
    public LayerMask fishLayer; // Camada dos peixes

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(FishBehavioro());
    }

    private IEnumerator FishBehavioro()
    {
        while (true)
        {
            Vector3 fleeDirection = Vector3.zero;
            Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, raycastDistance, fishLayer);

            foreach (Collider2D col in hitColliders)
            {
                if (col.gameObject != gameObject)
                {
                    Vector3 dirToOtherFish = col.transform.position - transform.position;

                    if (col.CompareTag("Predator") && dirToOtherFish.magnitude <= raycastDistance)
                    {
                        fleeDirection += -dirToOtherFish.normalized * (raycastDistance - dirToOtherFish.magnitude);
                    }
                    else if (col.CompareTag("Prey") && dirToOtherFish.magnitude <= raycastDistance)
                    {
                        fleeDirection += dirToOtherFish.normalized * (raycastDistance - dirToOtherFish.magnitude);
                    }
                }
            }

            rb.velocity = fleeDirection.normalized * moveSpeed;

            yield return null;
        }
    }
}
