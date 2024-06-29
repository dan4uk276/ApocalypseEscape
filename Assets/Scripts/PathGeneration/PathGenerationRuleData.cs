using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PathGeneration
{
    public class PathGenerationRuleData
    {
        public float TileLength { get; private set; } = 10f;
        public float TileWidth { get; private set; } = 20f;
        public float TileRotationAfterRightTurn { get; private set; } = 90f;
        public float TileRotationAfterLeftTurn { get; private set; } = -90f;
    }
}