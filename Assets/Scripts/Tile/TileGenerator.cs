using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class TileGenerator : MonoBehaviour
{
    [SerializeField] private List<GameObject> tilePrefabs;
    [SerializeField] private int maxTilesCount;

    
    [SerializeField] private List<Tile> tiles = new();

    void Start()
    {
        for (int i = 0; i < maxTilesCount; i++)
        {
            GenerateTile();
        }
    }

    private void GenerateTile()
    {
        if(tiles.Count == 0)
        {
            GenerateInitialTile();
        }
        else
        {
            GenerateNextTile();
        }
    }

    private void GenerateInitialTile()
    {
        int tileIndex = Random.Range(0, tilePrefabs.Count);

        GameObject generatedTileObject = Instantiate(tilePrefabs[tileIndex],
                                                     Vector3.zero,
                                                     Quaternion.identity);

        Tile generatedTile = generatedTileObject.GetComponent<Tile>();

        tiles.Add(generatedTile);
    }

    private void GenerateNextTile()
    {
        int tileIndex = Random.Range(0,tilePrefabs.Count);

        Tile lastTileInList  = tiles.Last();

        Vector3 lastTilePosition = lastTileInList == null ? Vector3.zero : lastTileInList.transform.position;

        Vector3 nextTilePosition = lastTileInList == null ? Vector3.zero : new Vector3(0f, 0f, lastTileInList.TileLength);

        Vector3 newTilePosition = lastTilePosition + nextTilePosition;

        GameObject generatedTileObject = Instantiate(tilePrefabs[tileIndex],
                                                     newTilePosition,
                                                     Quaternion.identity);

        Tile generatedTile = generatedTileObject.GetComponent<Tile>();

        tiles.Add(generatedTile);
    }

    private void OnTriggerEnter(Collider other)
    {
        Tile removedTile = other.GetComponent<Tile>();
        tiles.Remove(removedTile);
        Destroy(other.gameObject);

        /*Tile removedTile = tiles.First();
        tiles.RemoveAt(0);
        Destroy(removedTile.gameObject);*/

        GenerateNextTile();

    }
    /*private void OnTriggerEnter(Collision collision)
    {
        Tile removedTile = tiles[0];
        tiles.RemoveAt(0);
        Destroy(removedTile);

        GenerateNextTile();
    }*/
}
