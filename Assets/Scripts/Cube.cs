using UnityEngine;

[RequireComponent(typeof(Renderer))]
[RequireComponent(typeof(Rigidbody))]
public class Cube : MonoBehaviour
{
    private Renderer _renderer;
    private Rigidbody _rigidbody;
    private bool _isCollided;

    public Rigidbody Rigidbody => _rigidbody;

    public event System.Action<Cube> OnCollision;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        _isCollided = false;
        _renderer.material.color = Color.white;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (_isCollided == false && other.gameObject.TryGetComponent(out Ground _))
        {
            ProcessCollision();
        }
    }

    private void ProcessCollision()
    {
        SetRandomColor();
        _isCollided = true;
        OnCollision?.Invoke(this);
    }

    private void SetRandomColor()
    {
        _renderer.material.color = Random.ColorHSV();
    }
}
