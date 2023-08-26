using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishHunt : MonoBehaviour
{
    public float moveSpeed = 2f; // Velocidade de movimento do peixe
    public float detectionRadius = 2f; // Distância de detecção de presas
    public LayerMask preyLayer; // Camada das presas

    private FishMoviment fishMoviment; // Referência para o componente FishMoviment

    private bool hunting = false; // Flag para controlar se está caçando
    private Transform targetPrey; // Referência para a presa atual


    private void Start()
    {
        fishMoviment = GetComponent<FishMoviment>();
    }

    private void Update()
    {
        if (!hunting)
        {
            CheckForPrey();
            MoveFish();
            //CheckForPrey();
        }
        else
        {
            HuntPrey();
        }
    }

    private void MoveFish()
    {
        fishMoviment.Swim();
    }

    private void CheckForPrey()
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, detectionRadius, preyLayer);
        Debug.Log(hitColliders.Length);
        foreach (Collider2D col in hitColliders)
        {
            if (col.gameObject != gameObject)
            {
                targetPrey = col.transform;
                hunting = true;
                break; // Para de verificar após encontrar uma presa
            }
        }
    }

    private void HuntPrey()
    {
        if (targetPrey == null)
        {
            hunting = false;
            return;
        }

        Vector2 direction = (targetPrey.position - transform.position).normalized;
        transform.Translate(direction * moveSpeed * Time.deltaTime);

        if (Vector2.Distance(transform.position, targetPrey.position) < 0.5f)
        {
            Destroy(targetPrey.gameObject);
            targetPrey = null;
            hunting = false;
        }
    }
}
