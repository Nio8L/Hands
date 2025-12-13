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

    public int x = 0, y = 0;

    public List<Tile> handSegment;
    //public TileType handPart;
    public int maxLength;
    int controller;
    Color handColor;

    public void Setup(Color color, int controller)
    {
        handColor = color;
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
                    if (handSegment.Count > maxLength)
                    {
                        Retract();
                        return;
                    }

                    handSegment.Remove(targetTile);
                    targetTile.ChangeColor();

                    UpdatePosition(x, y);
                    
                    originTile.Deactivate(this);
                }
            }else if (targetTile.type.walkable && handSegment.Count < maxLength)
            {
                handSegment.Add(originTile);
                originTile.HandOn(handColor);

                UpdatePosition(x, y);
                
                targetTile.Activate(this);
            }
        }
    }

    public void ForceMove(int direction, int length)
    {
        int dx = 0;
        int dy = 0; 
     
        switch (direction)
        {
            case 0:
                dy = 1;
                break;
            case 1:
                dx = 1;
                break;
            case 2:
                dy = -1;
                break;
            case 3:
                dx = -1;
                break; 
            default:
                dy = 0;
                break;
        }

        for (int i = 0; i < length; i++)
        {
            Tile targetTile = Level.instance.tiles[x + dx, y + dy];
            Tile originTile = Level.instance.tiles[x, y];
            
            if (targetTile.type.walkable)
            {
                handSegment.Add(originTile);
                originTile.HandOn(handColor);

                UpdatePosition(x + dx, y + dy);
                
                targetTile.Activate(this);
            }
            else
            {
                return;
            }
        }

    }

    public void Retract()
    {
        while (handSegment.Any())
        {
            Tile targetTile = handSegment[^1];
            Tile originTile = Level.instance.tiles[x, y];
            handSegment.Remove(targetTile);
            targetTile.Default();

            UpdatePosition(targetTile.x, targetTile.y);
                    
            originTile.Deactivate(this);
        }
    }

    public void UpdatePosition(int x, int y)
    {
        transform.position = Level.instance.tiles[x, y].transform.position;
        this.x = x;
        this.y = y;
    }
}
