using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float tileLength;

    public float TileLength => tileLength;
    void Start()
    {
        speed = 10f;
    }

 
    void FixedUpdate()
    {
        transform.Translate(Vector3.back * speed * Time.fixedDeltaTime);
    }
}
