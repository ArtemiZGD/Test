using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class Spawner : MonoBehaviour
{
    [Header("Spawner")]
    [SerializeField] private Vector3 _spawnerCenter;
    [SerializeField] private Vector3 _spawnerSize;
    [SerializeField] private Cube _cubePrefab;
    [SerializeField] private float _cubeSpawnRate = 1f;
    [SerializeField] private float _minCubeLifetime = 2f;
    [SerializeField] private float _maxCubeLifetime = 5f;
    [Header("ObjectPool")]
    [SerializeField] private int _defaultCapacity = 10;
    [SerializeField] private int _maxSize = 1000;

    private ObjectPool<Cube> _cubePool;

    private void Awake()
    {
        _cubePool = new ObjectPool<Cube>(
            createFunc: () => SpawnCube(),
            actionOnGet: cube => Initialize(cube),
            actionOnRelease: cube => cube.gameObject.SetActive(false),
            actionOnDestroy: cube => Destroy(cube.gameObject),
            collectionCheck: true,
            defaultCapacity: _defaultCapacity,
            maxSize: _maxSize
        );
    }

    private void Start()
    {
        StartCoroutine(SpawnCubes());
    }

    private IEnumerator SpawnCubes()
    {
        var wait = new WaitForSeconds(1 / _cubeSpawnRate);

        while (true)
        {
            _cubePool.Get();
            yield return wait;
        }
    }

    private Cube SpawnCube()
    {
        return Instantiate(_cubePrefab, Vector3.zero, Quaternion.identity, transform);
    }

    private void Initialize(Cube cube)
    {
        cube.transform.rotation = Quaternion.identity;
        cube.transform.position = GetRandomPosition();
        cube.gameObject.SetActive(true);
        cube.Collided += SetRandomLifetime;
    }

    private void SetRandomLifetime(Cube cube)
    {
        cube.Collided -= SetRandomLifetime;
        StartCoroutine(ReleaseCube(cube, Random.Range(_minCubeLifetime, _maxCubeLifetime)));
    }

    private IEnumerator ReleaseCube(Cube cube, float time)
    {
        yield return new WaitForSeconds(time);
        _cubePool.Release(cube);
    }

    private Vector3 GetRandomPosition()
    {
        return new Vector3(
            Random.Range(_spawnerCenter.x - _spawnerSize.x / 2, _spawnerCenter.x + _spawnerSize.x / 2),
            Random.Range(_spawnerCenter.y - _spawnerSize.y / 2, _spawnerCenter.y + _spawnerSize.y / 2),
            Random.Range(_spawnerCenter.z - _spawnerSize.z / 2, _spawnerCenter.z + _spawnerSize.z / 2)
        );
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(_spawnerCenter, _spawnerSize);
    }
}
