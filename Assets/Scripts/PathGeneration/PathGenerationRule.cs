using System.Collections.Generic;
using UnityEditor.MemoryProfiler;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace PathGeneration
{
    public class PathGenerationRule : IPathGenerationRule
    {
        private const int TilesCountBetweenTurns = 8;

        private readonly PathGenerationRuleData ruleData;

        private readonly Dictionary<TileType, Vector3> positionRuleForNextTileConnection;

        private readonly Dictionary<TileType, Quaternion> rotationRuleForNextTileConnection;

        public PathGenerationRule()
        {
            ruleData = new();

            positionRuleForNextTileConnection = new()
            {
                {TileType.StraightTile,  new Vector3(0f, 0f, ruleData.TileLength)},
                {TileType.RightTurnTile, new Vector3(ruleData.TileLength, 0f, ruleData.TileLength)},
                {TileType.LeftTurnTile,  new Vector3(-ruleData.TileLength, 0f, ruleData.TileLength)}
            };

            rotationRuleForNextTileConnection = new()
            {
                {TileType.StraightTile,  Quaternion.Euler(Vector3.zero)},
                {TileType.RightTurnTile, Quaternion.Euler(0f, ruleData.TileRotationAfterRightTurn, 0f)},
                {TileType.LeftTurnTile,  Quaternion.Euler(0f, ruleData.TileRotationAfterLeftTurn, 0f)}
            };
        }

        public ITile DecideNextTile(List<GameObject> tilePrefabs, List<ITile> currentTiles)
        {
            if (AreLastTilesStraight(currentTiles, TilesCountBetweenTurns))
            {
                TileType tileType = (TileType) Random.Range((int)TileType.RightTurnTile,
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

            Vector3 nextTileLocalPosition = positionRuleForNextTileConnection[lastTileInList.TileType];
           
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

            lastTileLocalRotation *= rotationRuleForNextTileConnection[lastTileInList.TileType];

            return lastTileLocalRotation;
        }
        private bool AreLastTilesStraight(List<ITile> currentTiles, int tilesCount)
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