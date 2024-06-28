using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace PathGeneration
{
    public interface ITile
    {
        Transform Transform { get; }
        TileType TileType { get; }
        float TileLength { get; }
        Vector3 RoadStartPosition { get; }
        Vector3 RoadEndPosition { get; }

        void Move();
        Vector3 GetNextGenerationPosition();
    }
}
