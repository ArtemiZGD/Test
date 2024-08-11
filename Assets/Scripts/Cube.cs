using UnityEngine;

[RequireComponent(typeof(Renderer))]
[RequireComponent(typeof(Rigidbody))]
public class Cube : MonoBehaviour
{
    private Renderer _renderer;
    private Rigidbody _rigidbody;
    private float _chanceToSplit = 1;

    public Rigidbody Rigidbody => _rigidbody;

    public float ChanceToSplit
    {
        get => _chanceToSplit;
        set => _chanceToSplit = Mathf.Clamp(value, 0, 1);
    }

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        _renderer.material.color = Random.ColorHSV();
    }
}
