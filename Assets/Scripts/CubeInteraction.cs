using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CubeInteraction : MonoBehaviour
{
    [SerializeField] private Splitter _splitter;
    [SerializeField] private Blaster _blaster;
    [SerializeField] private KeyCode _splitKey = KeyCode.Mouse0;

    private List<Cube> _cubes;
    private Camera _camera;

    private void Awake()
    {
        _camera = Camera.main;
        _cubes = FindObjectsOfType<Cube>().ToList();
    }

    private void Update()
    {
        if (Input.GetKeyDown(_splitKey))
        {
            if (Physics.Raycast(_camera.ScreenPointToRay(Input.mousePosition), out RaycastHit hit))
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
        if (Random.value < cube.ChanceToSplit)
        {
            Cube[] cubes = _splitter.SplitCube(cube);
            _cubes.AddRange(cubes);
            _blaster.Blast(cubes);
        }
        else
        {
            _blaster.Blast(cube, _cubes);
        }

        _cubes.Remove(cube);
        Destroy(cube.gameObject);
    }
}
