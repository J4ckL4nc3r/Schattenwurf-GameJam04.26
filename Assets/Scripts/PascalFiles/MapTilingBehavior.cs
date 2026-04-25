using NUnit.Framework;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MapTilingBehavior : MonoBehaviour
{
    [SerializeField] private float tileSize = 1f;
    [SerializeField] private GameObject[] tilePrefabs;

    private GameObject[] _activeTiles = new GameObject[5];
    private GameObject _cam;
    private bool _isLeft;
    private Vector3 _lastChunkID, _curChunkID;

    public void Start()
    {
        _cam = Camera.main.gameObject;
        SpawnDefault();
    }

    private void SpawnDefault()
    {
        for(int tileID = -2; tileID <= 2; tileID++)
        {
            int prefabIndex = tileID == 0
                ?  prefabIndex = 0
                :  prefabIndex = Random.Range(0, tilePrefabs.Length);
            SpawnNewTile(tileID, prefabIndex);
        }
    }

    public void Update()
    {
        if (_cam == null) return;

        _curChunkID = (_cam.transform.position / tileSize);

        if (_curChunkID.x > _lastChunkID.x) _isLeft = false;
        else if (_curChunkID.x < _lastChunkID.x) _isLeft = true;

        _lastChunkID = _curChunkID;
    }

    private void DeleteOldTile(int tileID)
    {

    }

    private void SpawnNewTile(int tileID, int tilePrefabID)
    {
        GameObject tile = Instantiate(tilePrefabs[tilePrefabID], new Vector3(_curChunkID.x + (tileID * tileSize), transform.position.y, 0), transform.rotation);
        tile.transform.parent = transform;
        _activeTiles[tileID+2] = tile;
    }
}
