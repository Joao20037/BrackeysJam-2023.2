using System.Collections;
using UnityEngine;

public class FishMoviment : MonoBehaviour
{
    public float moveSpeed = 2f; // Velocidade de movimento do peixe
    public float rotationSpeed = 5f; // Velocidade de rotação do peixe
    public float minWaitTime = 1f; // Tempo mínimo de espera antes de trocar de direção
    public float maxWaitTime = 3f; // Tempo máximo de espera antes de trocar de direção
    public float swimRange = 5f; // Distância máxima que o peixe pode nadar do ponto inicial
    public Sprite[] swimSprites; // Array de sprites de natação

    private Vector3 targetPosition;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        SetRandomTargetPosition();
        StartCoroutine(Swim());
    }

    private void Update()
    {
        RotateToTarget();
    }

    private void SetRandomTargetPosition()
    {
        targetPosition = transform.position + new Vector3(Random.Range(-swimRange, swimRange), Random.Range(-swimRange, swimRange), 0);
    }

    private void RotateToTarget()
    {
        Vector3 direction = targetPosition - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
    }

    public IEnumerator Swim()
    {
        while (true)
        {
            // Troca o sprite para simular a animação de natação
            spriteRenderer.sprite = swimSprites[Random.Range(0, swimSprites.Length)];

            // Move o peixe em direção ao ponto alvo
            while (Vector3.Distance(transform.position, targetPosition) > 0.1f)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
                yield return null;
            }

            // Espera um tempo aleatório antes de escolher um novo ponto alvo
            float waitTime = Random.Range(minWaitTime, maxWaitTime);
            yield return new WaitForSeconds(waitTime);

            SetRandomTargetPosition();
        }
    }
}
