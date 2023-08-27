using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DeepUI : MonoBehaviour
{
    [SerializeField] private GameObject _submarine;
    [SerializeField] private TextMeshProUGUI _text;
    private float _submarineY;


    // Update is called once per frame
    void Update()
    {
        _submarineY = - _submarine.transform.position.y;
        _text.SetText(_submarineY.ToString("0."));
    }
}
