using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class RemoveTilemap : MonoBehaviour
{
    public Tilemap map;

    private void Awake()
    {
        map = FindObjectOfType<Tilemap>();
    }

    // Update is called once per frame
    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            map = GetComponentInParent<Tilemap>();
            Vector3Int tilePosition = Vector3Int.FloorToInt(transform.position);
            map.SetTile(tilePosition, null);
        }
    }
}
