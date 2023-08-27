using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishHunt : MonoBehaviour
{
    public float moveSpeedHunt = 4f; // Velocidade de movimento do peixe
    public float detectionRadius = 2f; // Distância de detecção de presas
    public LayerMask preyLayer; // Camada das presas
    private FishMoviment fishMoviment; // Referência para o componente FishMoviment

    private bool hunting = false; // Flag para controlar se está caçando
    private Transform targetPrey; // Referência para a presa atual

    public float moveSpeed = 2f; // Velocidade de movimento do peixe
    public float rotationSpeed = 5f; // Velocidade de rotação do peixe
    public float minWaitTime = 1f; // Tempo mínimo de espera antes de trocar de direção
    public float maxWaitTime = 3f; // Tempo máximo de espera antes de trocar de direção
    public float swimRange = 5f; // Distância máxima que o peixe pode nadar do ponto inicial
    public Sprite[] swimSprites; // Array de sprites de natação


    private Coroutine swimCoroutine; // Referência para a coroutine de natação
    public bool podeNadar = true;

    private Vector3 targetPosition;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        fishMoviment = GetComponent<FishMoviment>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        SetRandomTargetPosition();
        swimCoroutine = StartCoroutine(Swim());
    }

    private void Update()
    {
        if (!hunting)
        {
            MoveFish();
            RotateToTarget();

            CheckForPrey();
        }
        else
        {
            HuntPrey();
        }
    }

    private void MoveFish()
    {
        Swim();
    }

    private void CheckForPrey()
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, detectionRadius, preyLayer);
        
        //Debug.Log(hitColliders.Length);
        foreach (Collider2D col in hitColliders)
        {
            if (col.gameObject != gameObject || col.gameObject.tag == "Palhaco" || col.gameObject.tag == "Coringa") 
            {
                targetPrey = col.transform;
                Debug.Log(targetPrey.gameObject);
                hunting = true;
                break; // Para de verificar após encontrar uma presa
            }
        }
    }

    private void HuntPrey()
    {
        //GetComponent<fishMoviment>().Enable = false;
        fishMoviment.podeNadar = false;
        if (targetPrey == null)
        {
            hunting = false;
            return;
        }

        Vector2 direction = (targetPrey.position - transform.position).normalized;
        transform.Translate(direction * moveSpeedHunt * Time.deltaTime);

        if (Vector2.Distance(transform.position, targetPrey.position) < 1f)
        {
            Destroy(targetPrey.gameObject);
            targetPrey = null;
            hunting = false;
            fishMoviment.podeNadar = true;
            return;
        }
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
        while (podeNadar)
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
        swimCoroutine = null;
    }
}
