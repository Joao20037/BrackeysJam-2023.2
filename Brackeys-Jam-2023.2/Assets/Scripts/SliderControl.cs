using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderControl : MonoBehaviour
{
    private Catalogo _catalogo;
    private int i;
    [SerializeField] private List<GameObject> list_sliders;
    private void Awake()
    {
        _catalogo = gameObject.GetComponent<Catalogo>();
    }

    private void Start()
    {
        i = 0;
        foreach (var slider in list_sliders)
        {
            slider.GetComponent<Slider>().maxValue = _catalogo.peixes[i].meta;
            i += 1;
        }
        i = 0;
    }


    // Update is called once per frame
    void Update()
    {
        i = 0;
        foreach (var slider in list_sliders)
        {
            slider.GetComponent<Slider>().value = _catalogo.peixes[i].fotografados;
            i += 1;
        }
        i = 0;
    }   
}
