using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotoCamera : MonoBehaviour
{
    FOV fov;
    GameObject peixe;
    // Start is called before the first frame update
    void Start()
    {
        fov = gameObject.GetComponent<FOV>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && fov.targetsInRadius.Length > 0)
        {
            Shoot(fov.targetsInRadius);
        }
    }
    private IEnumerator Atordoar(GameObject peixe)
    {
        //peixe.GetComponent<FishMoviment>().enabled = false;
        peixe.GetComponent<FishHunt>().enabled = false;
        yield return new WaitForSeconds(3f);
        //peixe.GetComponent<FishMoviment>().enabled = true;
        peixe.GetComponent<FishHunt>().enabled = true;
    }

    private IEnumerator Flash()
    {
        Color _colorBlue = gameObject.GetComponent<MeshRenderer>().materials[0].color;
        Color _colorWhite = gameObject.GetComponent<MeshRenderer>().materials[1].color;
        gameObject.GetComponent<MeshRenderer>().materials[0].color = _colorWhite;
        yield return new WaitForSeconds(1f);
        gameObject.GetComponent<MeshRenderer>().materials[0].color = _colorBlue;
    }


    public void Shoot(Collider2D[] peixes)
    {
        float dist = 0;
        Debug.Log(peixes.Length);
        foreach (Collider2D mano in peixes)
        {
            if (dist < Vector3.Distance(transform.position, mano.transform.position))
            {
                dist = Vector3.Distance(transform.position, mano.transform.position);
                peixe = mano.gameObject;
                Debug.Log(peixe.name);
            }
        }
        //Atordoa Peixe
        StartCoroutine(Atordoar(peixe));
        StartCoroutine(Flash());

        //Atualiza catálogo
        string tag = peixe.tag;
        Catalogo catalogo = GameObject.FindGameObjectWithTag("Catalogo").GetComponent<Catalogo>();
        foreach (DadosPeixe registro in catalogo.peixes)
        {
            if (registro.nomePeixe == tag)
            {
                registro.fotografados++;
                if (registro.fotografados == registro.meta)
                {
                    catalogo.upgradePoints++;
                    catalogo.Efeito(catalogo.upgradePoints);
                }
                return;
            }
        }
    }
}