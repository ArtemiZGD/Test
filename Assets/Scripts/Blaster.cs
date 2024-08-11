using UnityEngine;

public class Blaster : MonoBehaviour
{
    [SerializeField] private float _blastForce = 10;
    
    public void Blast(Cube[] cubes)
    {
        foreach (Cube cube in cubes)
        {
            cube.Rigidbody.AddForce(Random.insideUnitSphere * _blastForce, ForceMode.Impulse);
        }
    }
}
