using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace PathGeneration
{
    public class TileGenerator : MonoBehaviour
    {
        [SerializeField] private List<GameObject> tilePrefabs;
        [SerializeField] private int maxTilesCount;
        [SerializeField] private List<ITile> currentTiles = new();
        [SerializeField] private Transform tilesSpawnPoint;

        private readonly IPathGenerationRule pathGenerationRule = new PathGenerationRule();

        public List<ITile> CurrentTiles { get { return currentTiles; } }

        void Start()
        {
            for (int i = 0; i < maxTilesCount; i++)
            {
                GenerateTile();
            }  
        }

        private void GenerateTile()
        {
            ITile lastTileInList = currentTiles.LastOrDefault();

            ITile pendingTile = pathGenerationRule.DecideNextTile(tilePrefabs, CurrentTiles);

            Vector3 newTilePosition = pathGenerationRule.GetNextTilePosition(lastTileInList, pendingTile);
            Quaternion newTileRotation = pathGenerationRule.GetNextTileRotation(lastTileInList, pendingTile);

            GameObject pendingTileObject = tilePrefabs[(int)pendingTile.TileType];
            
            GameObject generatedTileObject = Instantiate(pendingTileObject,
                                                         newTilePosition,
                                                         newTileRotation,
                                                         tilesSpawnPoint);

            ITile generatedTile = generatedTileObject.GetComponent<ITile>();

            currentTiles.Add(generatedTile);
        }

        private void OnTriggerEnter(Collider other)
        {
            ITile removedTile = other.GetComponent<ITile>();
            currentTiles.Remove(removedTile);
            Destroy(other.gameObject);

            GenerateTile();

        }
    }
}