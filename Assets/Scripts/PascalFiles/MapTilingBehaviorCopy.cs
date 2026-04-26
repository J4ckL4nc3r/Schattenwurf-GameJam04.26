using NUnit.Framework;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MapTilingBehaviorCopy : MonoBehaviour
{
    [SerializeField] private float tileSize = 30f;
    [SerializeField] private GameObject[] tilePrefabs;
    [SerializeField] private GameObject player;

    [SerializeField] private GameObject toggleObj;

    private GameObject[] _activeTiles = new GameObject[5];

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

    public void SpawnNewTile(int tileID, int tilePrefabID, int _curChunkID)
    {
        GameObject tile = Instantiate(tilePrefabs[tilePrefabID], new Vector3((_curChunkID + tileID) * tileSize, transform.position.y, 0), transform.rotation);
        tile.transform.parent = toggleObj==null? transform: toggleObj.transform;
        _activeTiles[tileID+2] = tile;
    }
}
