using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class HandScript : MonoBehaviour
{
    public float speed = 5f;

    int x = 0, y = 0;

    public List<Tile> handSegment;
    public TileType handPart;

    private void Start() {
        transform.position = LevelEditor.instance.transform.position;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            Move(x, y + 1);
            transform.rotation = Quaternion.Euler(0, 0, 90);

        }
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            Move(x, y - 1);
            transform.rotation = Quaternion.Euler(0, 0, 270);
        }
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Move(x - 1, y);
            transform.rotation = Quaternion.Euler(0, 0, 180);
        }
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            Move(x + 1, y);
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }

    void Move(int x, int y)
    {
        if ((0 <= x && x < LevelEditor.instance.width) && (0 <= y && y < LevelEditor.instance.height))
        {
            Tile targetTile = LevelEditor.instance.tiles[x, y];
            if (handSegment.Contains(targetTile))
            {
                if (handSegment[^1].x == targetTile.x && handSegment[^1].y == targetTile.y)
                {
                    handSegment.RemoveAt(handSegment.Count - 1);
                    transform.position = targetTile.transform.position;
                    targetTile.Default();
                }
            }
            if (LevelEditor.instance.tiles[x, y].type.walkable)
            {
                transform.position = targetTile.transform.position;
                handSegment.Add(targetTile);
                targetTile.SetType(handPart);
                this.x = x;
                this.y = y;
                Debug.Log("Tile is right now at " + x + " " + y);
            }
        }
    }
}
