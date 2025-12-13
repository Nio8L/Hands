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
    int controller;

    public void Setup(Color color, int controller)
    {
        handPart = Instantiate(handPart);
        handPart.color = color;
        this.controller = controller;
    }

    void Update()
    {
        if (controller == 1)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                Move(x, y + 1);
                transform.rotation = Quaternion.Euler(0, 0, 90);
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                Move(x, y - 1);
                transform.rotation = Quaternion.Euler(0, 0, 270);
            }
            if (Input.GetKeyDown(KeyCode.A))
            {
                Move(x - 1, y);
                transform.rotation = Quaternion.Euler(0, 0, 180);
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                Move(x + 1, y);
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }
        }else{
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                Move(x, y + 1);
                transform.rotation = Quaternion.Euler(0, 0, 90);
            }
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                Move(x, y - 1);
                transform.rotation = Quaternion.Euler(0, 0, 270);
            }
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                Move(x - 1, y);
                transform.rotation = Quaternion.Euler(0, 0, 180);
            }
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                Move(x + 1, y);
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }
        }
        
    }

    void Move(int x, int y)
    {
        if ((0 <= x && x < Level.instance.width) && (0 <= y && y < Level.instance.height))
        {
            Tile targetTile = Level.instance.tiles[x, y];
            Tile originTile = Level.instance.tiles[this.x, this.y];
            
            if (handSegment.Contains(targetTile))
            {
                if (handSegment[^1].x == targetTile.x && handSegment[^1].y == targetTile.y)
                {
                    handSegment.Remove(targetTile);
                    targetTile.Default();

                    UpdatePosition(x, y);
                }
            }else if (targetTile.type.walkable)
            {
                handSegment.Add(originTile);
                originTile.SetType(handPart);
        
                UpdatePosition(x, y);
            }
        }
    }

    public void UpdatePosition(int x, int y)
    {
        transform.position = Level.instance.tiles[x, y].transform.position;
        this.x = x;
        this.y = y;
    }
}
