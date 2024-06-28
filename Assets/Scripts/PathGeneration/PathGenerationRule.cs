using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace PathGeneration
{
    public class PathGenerationRule : IPathGenerationRule
    {
        //private List<ITile> pendingTiles = new();

        //public List<ITile> PendingTiles { get { return pendingTiles; } }
        private TileType lastTurnRoadTileType = TileType.None;

        public ITile DecideNextTile(List<GameObject> tilePrefabs, List<ITile> currentTiles)
        {
            if (currentTiles.Count < 4)
            {
                return tilePrefabs[(int)TileType.RoadTile].GetComponent<ITile>();
            }

            if (AreLastTilesRoad(currentTiles, 4))
            {
                TileType tileType = (TileType)Random.Range((int)TileType.RightRoadTurnTile,
                                                            (int)TileType.LeftRoadTurnTile + 1);
                lastTurnRoadTileType = tileType;

                return tilePrefabs[(int)tileType].GetComponent<ITile>();
            }

            ITile pendingTile = tilePrefabs[(int)TileType.RoadTile].GetComponent<ITile>();

            return pendingTile;
        }


        public Vector3 GetNextTilePosition(ITile lastTileInList, ITile pendingTile)
        {
            if (lastTileInList == null)
            {
                return Vector3.zero;
            }

            Vector3 lastTilePosition = lastTileInList.Transform.position;

            Vector3 nextTileLocalPosition = Vector3.zero;

            if(lastTileInList.TileType == TileType.RightRoadTurnTile &&
               pendingTile.TileType == TileType.RoadTile)
            {
                nextTileLocalPosition = new Vector3(10f, 0f, 10f);
                nextTileLocalPosition = lastTileInList.Transform.TransformDirection(nextTileLocalPosition);
                return lastTilePosition + nextTileLocalPosition;
            }

            if(lastTileInList.TileType == TileType.LeftRoadTurnTile &&
               pendingTile.TileType == TileType.RoadTile)
            {
                nextTileLocalPosition = new Vector3(-10f, 0f, 10f);
                nextTileLocalPosition = lastTileInList.Transform.TransformDirection(nextTileLocalPosition);
                return lastTilePosition + nextTileLocalPosition;
            }

            if(lastTileInList.TileType == TileType.RoadTile && 
                pendingTile.TileType == TileType.RoadTile)
            {
                
                nextTileLocalPosition = new Vector3(0f, 0f, lastTileInList.TileLength);

                nextTileLocalPosition = lastTileInList.Transform.TransformDirection(nextTileLocalPosition);

                return lastTilePosition + nextTileLocalPosition;
            }

            if(lastTileInList.TileType == TileType.RoadTile &&
               pendingTile.TileType == TileType.LeftRoadTurnTile)
            {
                nextTileLocalPosition = new Vector3(0f, 0f, lastTileInList.TileLength);

                nextTileLocalPosition = lastTileInList.Transform.TransformDirection(nextTileLocalPosition);

                return lastTilePosition + nextTileLocalPosition;
            }

            if (lastTileInList.TileType == TileType.RoadTile &&
               pendingTile.TileType == TileType.RightRoadTurnTile)
            {   
                nextTileLocalPosition = new Vector3(0f, 0f, lastTileInList.TileLength);
                nextTileLocalPosition = lastTileInList.Transform.TransformDirection(nextTileLocalPosition);
                return lastTilePosition + nextTileLocalPosition;
            }
            return lastTilePosition + nextTileLocalPosition;
            
        }

        public Quaternion GetNextTileRotation(ITile lastTileInList, ITile pendingTile)
        {
            if (lastTileInList == null)
            {
                return Quaternion.identity;
            }

            if(lastTileInList.TileType == TileType.RoadTile &&
               pendingTile.TileType == TileType.RoadTile) 
            { 
                return lastTileInList.Transform.localRotation;
            }

            if(lastTileInList.TileType == TileType.RoadTile &&
                pendingTile.TileType == TileType.RightRoadTurnTile)
            {
                return lastTileInList.Transform.localRotation;
            }

            if (lastTileInList.TileType == TileType.RoadTile &&
                pendingTile.TileType == TileType.LeftRoadTurnTile)
            {
                return lastTileInList.Transform.localRotation;
            }

            if (lastTileInList.TileType == TileType.RightRoadTurnTile)
            {
                return lastTileInList.Transform.localRotation * Quaternion.Euler(0f, 90f, 0f);
            }

            if (lastTileInList.TileType == TileType.LeftRoadTurnTile)
            {
                return lastTileInList.Transform.localRotation * Quaternion.Euler(0f, -90f, 0f);
            }

            return lastTileInList.Transform.localRotation;
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

                if (currentTiles[lastCurrentTileIndex - i].TileType != TileType.RoadTile)
                {
                    return false;
                }
            }

            return true;
        }
    }
}