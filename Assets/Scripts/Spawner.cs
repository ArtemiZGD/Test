using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private int _minSpawnCubes = 2;
    [SerializeField] private int _maxSpawnCubes = 6;
    [SerializeField] private float _sizeSplitMultiplier = 0.5f;
    [SerializeField] private float _chanceToSplitMultiplier = 0.5f;
    [SerializeField] private Cube _cubePrefab;

    public Cube[] Split(Cube cube)
    {
        int cubesToSpawn = Random.Range(_minSpawnCubes, _maxSpawnCubes + 1);
        Vector3 cubePosition = cube.transform.position;
        Vector3 newCubeSize = cube.transform.localScale * _sizeSplitMultiplier;
        Cube[] newCubes = new Cube[cubesToSpawn];
        
        for (int i = 0; i < cubesToSpawn; i++)
        {
            Cube newCube = Instantiate(_cubePrefab, cubePosition, Quaternion.identity, transform);
            newCube.transform.localScale = newCubeSize;
            newCube.ChanceToSplit = _chanceToSplitMultiplier * cube.ChanceToSplit;
            newCubes[i] = newCube;
        }

        return newCubes;
    }
}
