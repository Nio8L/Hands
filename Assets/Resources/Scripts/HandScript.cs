using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HandScript : MonoBehaviour
{
    public float speed = 5f;

    public int x = 0, y = 0;

    public List<Tile> handSegment;
    public int maxLength;
    public string dir;
    int controller;
    Color handColor;
    public int movesLeft;

    MoveCounter moveCounter;

    public void Setup(Color color, int controller)
    {
        handColor = color;
        this.controller = controller;
        maxLength = 3;

        if (controller == 1)
        {
            moveCounter = GameObject.Find("MoveCounterWASD").GetComponent<MoveCounter>();
        }
        else
        {
            moveCounter = GameObject.Find("MoveCounterArrows").GetComponent<MoveCounter>();
        }

        UpdateMoves(maxLength);
    }

    public int GetDirectionIndex()
{
    switch (dir)
    {
        case "up": return 0;
        case "right": return 1;
        case "down": return 2;
        case "left": return 3;
        default: return 0;
    }
}


    void Update()
    {
        if (controller == 1)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                Move(x, y + 1);
                transform.rotation = Quaternion.Euler(0, 0, 90);
                dir = "up";
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                Move(x, y - 1);
                transform.rotation = Quaternion.Euler(0, 0, 270);
                dir = "down";
            }
            if (Input.GetKeyDown(KeyCode.A))
            {
                Move(x - 1, y);
                transform.rotation = Quaternion.Euler(0, 0, 180);
                dir = "left";
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                Move(x + 1, y);
                transform.rotation = Quaternion.Euler(0, 0, 0);
                dir = "right";
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                Move(x, y + 1);
                transform.rotation = Quaternion.Euler(0, 0, 90);
                dir = "up";
            }
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                Move(x, y - 1);
                transform.rotation = Quaternion.Euler(0, 0, 270);
                dir = "down";
            }
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                Move(x - 1, y);
                transform.rotation = Quaternion.Euler(0, 0, 180);
                dir = "left";
            }
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                Move(x + 1, y);
                transform.rotation = Quaternion.Euler(0, 0, 0);
                dir = "right";
            }
        }
    }

    void Move(int x, int y)
    {
        if ((0 <= x && x < Level.instance.width) && (0 <= y && y < Level.instance.height))
        {
            Tile targetTile = Level.instance.tiles[x, y];
            Tile originTile = Level.instance.tiles[this.x, this.y];

            // RETRACT
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
                    if (handSegment.Count < maxLength) UpdateMoves(movesLeft + 1);

                    targetTile.Default();
                    UpdatePosition(x, y);
                    originTile.Deactivate(this);
                }
            }
            // EXTEND
            else if (targetTile.type.walkable && movesLeft > 0)
            {
                handSegment.Add(originTile);
                UpdateMoves(movesLeft - 1);
                originTile.HandOn(handColor);

                UpdatePosition(x, y);

                targetTile.Activate(this);
                originTile.Deactivate(this);
            }
        }
    }

    public void ForceMove(int length)
    {
        int dx = 0;
        int dy = 0;
        int direction = GetDirectionIndex();

        switch (direction)
        {
            case 0: dy = 1; break;
            case 1: dx = 1; break;
            case 2: dy = -1; break;
            case 3: dx = -1; break;
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
            UpdateMoves(maxLength);
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

    void UpdateMoves(int remaining)
    {
        movesLeft = remaining;
        moveCounter.UpdateCounter(movesLeft);
    }
}
