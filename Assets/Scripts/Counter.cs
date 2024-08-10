using System.Collections;
using UnityEngine;

public class Counter : MonoBehaviour
{
    [SerializeField] private CounterDisplay _display;
    [SerializeField] private float _countDelay = 0.5f;
    [SerializeField] private KeyCode _toggleCountingKey = KeyCode.Mouse0;

    private int _value = 0;
    private bool _isCounting = true;

    private void Start()
    {
        _display.SetCounter(_value);
        StartCoroutine(Count());
    }

    private void Update()
    {
        if (Input.GetKeyDown(_toggleCountingKey))
        {
            SetCounting(!_isCounting);
        }
    }

    private void SetCounting(bool value)
    {
        if (_isCounting == value)
        {
            return;
        }

        _isCounting = value;

        if (_isCounting)
        {
            StartCoroutine(Count());
        }
        else
        {
            StopAllCoroutines();
        }
    }

    private IEnumerator Count()
    {
        var wait = new WaitForSeconds(_countDelay);

        while (true)
        {
            yield return wait;
            Increment();
        }
    }

    private void Increment()
    {
        _value++;
        _display.SetCounter(_value);
    }
}
