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
    }
}
