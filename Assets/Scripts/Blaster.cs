using System.Collections.Generic;
using UnityEngine;

public class Blaster : MonoBehaviour
{
    [SerializeField] private float _blastForce = 10;
    [SerializeField] private float _blastRadius = 1;
    
    public void Blast(Cube[] cubes)
    {
        foreach (Cube cube in cubes)
        {
            cube.Rigidbody.AddForce(Random.insideUnitSphere * _blastForce, ForceMode.Impulse);
        }
    }

    public void Blast(Cube blastedCube, List<Cube> cubes)
    {
        Vector3 blastPosition = blastedCube.transform.position;
        CalculateBlastForce(blastedCube, out float blastRadius, out float blastForce);

        foreach (Cube cube in cubes)
        {
            ProcessBlast(cube, blastPosition, blastRadius, blastForce);
        }
    }

    private void CalculateBlastForce(Cube cube, out float radius, out float blastForce)
    {
        float averageScale = (cube.transform.localScale.x + cube.transform.localScale.y + cube.transform.localScale.z) / 3;
        radius = _blastRadius / averageScale;
        blastForce = _blastForce / averageScale;
    }

    private void ProcessBlast(Cube cube, Vector3 blastPosition, float radius, float blastForce)
    {
        Vector3 direction = cube.transform.position - blastPosition;
        float distance = direction.magnitude;

        if (distance < radius)
        {
            float force = blastForce * (1 - distance / radius);

            cube.Rigidbody.AddForce(direction.normalized * force, ForceMode.Impulse);
        }
    }
}
