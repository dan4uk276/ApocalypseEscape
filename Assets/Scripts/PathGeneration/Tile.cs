using UnityEngine;

namespace PathGeneration
{
    public enum TileType
    {
        StraightTile = 0,
        RightTurnTile = 1,
        LeftTurnTile = 2
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

        void FixedUpdate()
        {
            Move();
        }

        public void Move()
        {
            transform.Translate(Vector3.back * speed * Time.fixedDeltaTime, Space.World);
        }
    }
}
