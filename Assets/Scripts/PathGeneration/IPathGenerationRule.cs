using System.Collections.Generic;
using UnityEngine;

namespace PathGeneration
{
    public interface IPathGenerationRule
    {
        //List<ITile> PendingTiles { get; }

        ITile DecideNextTile(List<GameObject> tilePrefabs, List<ITile> currentTiles);
        Vector3 GetNextTilePosition(ITile lastTileInList, ITile pendingTile);
        Quaternion GetNextTileRotation(ITile lastTileInList, ITile pendingTile);
    }
}