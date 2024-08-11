using UnityEngine;

[RequireComponent(typeof(Renderer))]
[RequireComponent(typeof(Rigidbody))]
public class Cube : MonoBehaviour
{
    public float ChanceToSplit = 1;

    private Renderer _renderer;
    private Rigidbody _rigidbody;

    public void Blast(Vector3 force)
    {
        _rigidbody.AddForce(force, ForceMode.Impulse);
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
