using System.Collections.Generic;
using UnityEngine;

namespace PathGeneration
{
    public class PathGenerationRule : IPathGenerationRule
    {

        public ITile DecideNextTile(List<GameObject> tilePrefabs, List<ITile> currentTiles)
        {
            if (currentTiles.Count < 4)
            {
                return tilePrefabs[(int)TileType.StraightTile].GetComponent<ITile>();
            }

            if (AreLastTilesRoad(currentTiles, 4))
            {
                TileType tileType = (TileType)Random.Range((int)TileType.RightTurnTile,
                                                           (int)TileType.LeftTurnTile + 1);

                return tilePrefabs[(int)tileType].GetComponent<ITile>();
            }

            ITile pendingTile = tilePrefabs[(int)TileType.StraightTile].GetComponent<ITile>();

            return pendingTile;
        }


        public Vector3 GetNextTilePosition(ITile lastTileInList, ITile pendingTile)
        {
            if (lastTileInList == null)
            {
                return Vector3.zero;
            }

            Vector3 lastTilePosition = lastTileInList.Transform.position;

            Vector3 nextTileLocalPosition = new(0f, 0f, lastTileInList.TileLength);

            if (lastTileInList.TileType == TileType.RightTurnTile &&
               pendingTile.TileType == TileType.StraightTile)
            {
                nextTileLocalPosition = new Vector3(10f, 0f, 10f);         
            }
            
            if(lastTileInList.TileType == TileType.LeftTurnTile &&
               pendingTile.TileType == TileType.StraightTile)
            {
                nextTileLocalPosition = new Vector3(-10f, 0f, 10f);  
            }

            nextTileLocalPosition = lastTileInList.Transform.TransformDirection(nextTileLocalPosition);

            return lastTilePosition + nextTileLocalPosition;

        }

        public Quaternion GetNextTileRotation(ITile lastTileInList, ITile pendingTile)
        {
            if (lastTileInList == null)
            {
                return Quaternion.identity;
            }

            Quaternion lastTileLocalRotation = lastTileInList.Transform.localRotation;

            if (lastTileInList.TileType == TileType.RightTurnTile)
            {
                lastTileLocalRotation *= Quaternion.Euler(0f, 90f, 0f);
            }

            if (lastTileInList.TileType == TileType.LeftTurnTile)
            {
                lastTileLocalRotation *= Quaternion.Euler(0f, -90f, 0f);
            }

            return lastTileLocalRotation;
        }
        private bool AreLastTilesRoad(List<ITile> currentTiles, int tilesCount)
        {
            if (currentTiles.Count < tilesCount)
            {
                return false;
            }

            int lastCurrentTileIndex = currentTiles.Count - 1;

            for (int i = 0; i < tilesCount; i++)
            {
                if (currentTiles[lastCurrentTileIndex - i].TileType != TileType.StraightTile)
                {
                    return false;
                }
            }

            return true;
        }
    }
}