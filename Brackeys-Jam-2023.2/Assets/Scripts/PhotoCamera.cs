using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotoCamera : MonoBehaviour
{   
    FOV fov;
    // Start is called before the first frame update
    void Start()
    {
        fov = gameObject.GetComponent<FOV>();
    }

    // Update is called once per frame
    void Update()
    {
       if (Input.GetMouseButtonDown(0) && fov.targetsInRadius!=null)
       {
            Shoot(fov.targetsInRadius);
       } 
    }
    private IEnumerator Atordoar(GameObject peixe)
    {   
        peixe.GetComponent<FishMoviment>().enabled = false;
        peixe.GetComponent<FishHunt>().enabled = false;
        yield return new WaitForSeconds(3f);
        peixe.GetComponent<FishMoviment>().enabled = true;
        peixe.GetComponent<FishHunt>().enabled = true;
    }

    public void Shoot(Collider2D[] peixes)
    {
        float dist = 0;
        GameObject peixe = peixes[0].gameObject;
        foreach (Collider2D mano in peixes)
        {
            if (dist < Vector3.Distance(transform.position, mano.transform.position))
            {
                dist = Vector3.Distance(transform.position, mano.transform.position);
                peixe = mano.gameObject;
            }
        }
        string tag = peixe.tag;
        //Atordoa Peixe
        StartCoroutine(Atordoar(peixe));
        
        //Atualiza catálogo
        Catalogo catalogo = GameObject.FindGameObjectWithTag("Player").GetComponent<Catalogo>();
        if(catalogo.registros.TryGetValue(tag, out int value))
        {   
            value++;
            if (value == catalogo.Checkpoints[tag])
            {   
                catalogo.registros[tag] = value;
                catalogo.upgradePoints++;
                catalogo.Efeito(catalogo.upgradePoints);
            }
        }
    }
}
