using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

namespace PathGeneration
{
    public class TileGenerator : MonoBehaviour
    {
        [SerializeField] private List<GameObject> tilePrefabs;
        [SerializeField] private int maxTilesCount;
        [SerializeField] private List<ITile> currentTiles = new();
        [SerializeField] private Transform tileSpawnPoint;

        private IPathGenerationRule pathGenerationRule;

        public List<ITile> CurrentTiles { get { return currentTiles; } }

        void Awake()
        {
            pathGenerationRule = new PathGenerationRule();
        }

        void Start()
        {
            for (int i = 0; i < maxTilesCount; i++)
            {
                GenerateTile();
            }
            Debug.Log(tilePrefabs[2].transform.localEulerAngles.y);
            Debug.Log(tilePrefabs[2].transform.localPosition.x);
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
                                                         tileSpawnPoint);

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