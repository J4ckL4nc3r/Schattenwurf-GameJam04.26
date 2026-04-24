using UnityEngine;

public class MapTilingBehavior : MonoBehaviour
{
    [SerializeField] private float tileSize = 1f;
    [SerializeField] private GameObject[] tilePrefabs;
    private GameObject[] _aktiveTiles;

    private GameObject _cam;
    private bool _isLeft;
    private Vector3 _lastChunkID, _curChunkID;

    public void Start()
    {
        _cam = Camera.main.gameObject;
    }

    public void Update()
    {
        if (_cam == null) return;

        _curChunkID = (_cam.transform.position / tileSize);

        if (_curChunkID.x > _lastChunkID.x) _isLeft = false;
        else if (_curChunkID.x < _lastChunkID.x) _isLeft = true;

        _lastChunkID = _curChunkID;

    }

    private void DeleteOldTile()
    {

    }

    private void SpawnNewTile(Vector3 position)
    {
        int prefabIndex = Random.Range(0, tilePrefabs.Length);
        GameObject tile = Instantiate(tilePrefabs[prefabIndex], position, Quaternion.identity);
        tile.transform.parent = transform;
    }
}
