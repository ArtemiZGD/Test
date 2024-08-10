using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]
public class CounterDisplay : MonoBehaviour
{
    private TMP_Text _text;

    public void SetCounter(int value)
    {
        _text.text = value.ToString();
    }

    private void Awake()
    {
        _text = GetComponent<TMP_Text>();
    }
}
