using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PathGeneration
{
    public enum TileType
    {
        None = - 1,
        RoadTile = 0,
        RightRoadTurnTile = 1,
        LeftRoadTurnTile = 2
    }

    public class Tile : MonoBehaviour, ITile
    {
        [SerializeField] private float speed;
        [SerializeField] private float tileLength;
        [SerializeField] private TileType tileType;
        [SerializeField] private Vector3 roadStartPosition;
        [SerializeField] private Vector3 roadEndPosition;

        public TileType TileType => tileType;
        public float TileLength => tileLength;
        public Transform Transform => transform;

        public Vector3 RoadStartPosition => roadStartPosition;

        public Vector3 RoadEndPosition => roadEndPosition;

        void Start()
        {
            speed = 10f;
        }

        void FixedUpdate()
        {
            Move();
        }

        public void Move()
        {
            transform.Translate(Vector3.back * speed * Time.fixedDeltaTime, Space.World);
        }

        public Vector3 GetNextGenerationPosition()
        {
            return Vector3.zero;
        }
    }
}
