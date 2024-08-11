using UnityEngine;

public class CubeInteraction : MonoBehaviour
{
    [Header("Cube Split Settings")]
    [SerializeField] private int _minSpawnCubes = 2;
    [SerializeField] private int _maxSpawnCubes = 6;
    [SerializeField] private float _sizeSplitMultiplier = 0.5f;
    [SerializeField] private float _chanceToSplitMultiplier = 0.5f;
    [SerializeField] private float _blastForce = 10;
    [Header("Cube Split References")]
    [SerializeField] private Cube _cubePrefab;
    [SerializeField] private Transform _cubeParent;
    [SerializeField] private KeyCode _splitKey = KeyCode.Mouse0;

    private void Update()
    {
        if (Input.GetKeyDown(_splitKey))
        {
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit))
            {
                if (hit.collider.TryGetComponent(out Cube cube))
                {
                    SplitCube(cube);
                }
            }
        }
    }

    private void SplitCube(Cube cube)
    {
        if (Random.value > cube.ChanceToSplit)
        {
            Destroy(cube.gameObject);
            return;
        }

        int cubesToSpawn = Random.Range(_minSpawnCubes, _maxSpawnCubes + 1);
        Vector3 cubePosition = cube.transform.position;
        Vector3 newCubeSize = cube.transform.localScale * _sizeSplitMultiplier;
        
        for (int i = 0; i < cubesToSpawn; i++)
        {
            Cube newCube = Instantiate(_cubePrefab, cubePosition, Quaternion.identity, _cubeParent);
            newCube.transform.localScale = newCubeSize;
            newCube.ChanceToSplit = _chanceToSplitMultiplier * cube.ChanceToSplit;
            newCube.Blast(Random.insideUnitSphere * _blastForce);
        }

        Destroy(cube.gameObject);
    }
}
