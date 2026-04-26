using NUnit.Framework;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MapTilingBehavior : MonoBehaviour
{
    [SerializeField] private float tileSize = 30f;
    [SerializeField] private GameObject[] tilePrefabs;
    [SerializeField] private GameObject player;

    [SerializeField] private GameObject toggleObj;
    [SerializeField] private MapTilingBehaviorCopy copyScript;

    private GameObject[] _activeTiles = new GameObject[5];
    private bool _isLeft;
    private int _lastChunkID, _curChunkID;

    public void Start()
    {
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
            copyScript.SpawnNewTile(tileID, prefabIndex, _curChunkID);
        }
    }

    public void Update()
    {
        if (player == null) return;

        _curChunkID = (int)((player.transform.position.x + 15) / tileSize);
        if (_curChunkID > _lastChunkID) _isLeft = false;
        else if (_curChunkID < _lastChunkID) _isLeft = true;

        if(_curChunkID != _lastChunkID)
        {
            if(!_isLeft)
            {
                DeleteLeftTile();
                copyScript.DeleteLeftTile();
                int prefabIndex = Random.Range(0, tilePrefabs.Length);
                SpawnNewTile(2, prefabIndex);
                copyScript.SpawnNewTile(2, prefabIndex, _curChunkID);
            }
            else
            {
                DeleteRightTile();
                copyScript.DeleteRightTile();
                int prefabIndex = Random.Range(0, tilePrefabs.Length);
                SpawnNewTile(-2, prefabIndex);
                copyScript.SpawnNewTile(-2, prefabIndex, _curChunkID);
            }
        }

        _lastChunkID = _curChunkID;
    }

    public void DeleteLeftTile()
    {
        GameObject.Destroy(_activeTiles[0]);
        _activeTiles[0] = _activeTiles[1];
        _activeTiles[1] = _activeTiles[2];
        _activeTiles[2] = _activeTiles[3];
        _activeTiles[3] = _activeTiles[4];

    }
    public void DeleteRightTile()
    {
        GameObject.Destroy(_activeTiles[4]);
        _activeTiles[4] = _activeTiles[3];
        _activeTiles[3] = _activeTiles[2];
        _activeTiles[2] = _activeTiles[1];
        _activeTiles[1] = _activeTiles[0];

    }

    private void SpawnNewTile(int tileID, int tilePrefabID)
    {
        GameObject tile = Instantiate(tilePrefabs[tilePrefabID], new Vector3((_curChunkID + tileID) * tileSize, transform.position.y, 0), transform.rotation);
        tile.transform.parent = toggleObj==null? transform: toggleObj.transform;
        _activeTiles[tileID+2] = tile;
    }
}
