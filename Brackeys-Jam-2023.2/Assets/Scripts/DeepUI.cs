using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DeepUI : MonoBehaviour
{
    [SerializeField] private GameObject _submarine;
    [SerializeField] private TextMeshProUGUI _text;
    private float _submarineY;
    private float DeepLimit;

    private void Start()
    {
        DeepLimit = _submarine.GetComponent<MovementSubmarine>().deepLimit;
    }


    // Update is called once per frame
    void Update()
    {
        Debug.Log(DeepLimit);
        _submarineY = - _submarine.transform.position.y;
        _text.SetText(_submarineY.ToString("0."));
        if (_submarineY >  (DeepLimit - 0.5f))
        {
            _text.color = Color.red;
        }
        else
        {
            _text.color = Color.white;
        }
    }
}
