using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class HandScript : MonoBehaviour
{
    public float speed = 5f;

    public int x = 0, y = 0;

    public List<Tile> handSegment;
    public int maxLength;
    int controller;
    public int movesLeft;

    MoveCounter moveCounter;

    public int direction = -1;

    public Sprite head;
    public Sprite head2;

    public void Setup(int controller)
    {
        this.controller = controller;
        maxLength = 3;

        if (controller == 1)
        {
            moveCounter = GameObject.Find("MoveCounterWASD").GetComponent<MoveCounter>();
            GetComponent<SpriteRenderer>().sprite = head;
        }
        else
        {
            moveCounter = GameObject.Find("MoveCounterArrows").GetComponent<MoveCounter>();
            GetComponent<SpriteRenderer>().sprite = head2;
        }

        UpdateMoves(maxLength);
    }

    private void Start() {
        HandManager.instance.SetHand(this);
    }

    void Update()
    {
        if (controller == 1)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                Move(0);
                transform.rotation = Quaternion.Euler(0, 0, 90);
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                Move(2);
                transform.rotation = Quaternion.Euler(0, 0, 270);
            }
            if (Input.GetKeyDown(KeyCode.A))
            {
                Move(3);
                transform.rotation = Quaternion.Euler(0, 0, 180);
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                Move(1);
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                Move(0);
                transform.rotation = Quaternion.Euler(0, 0, 90);
            }
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                Move(2);
                transform.rotation = Quaternion.Euler(0, 0, 270);
            }
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                Move(3);
                transform.rotation = Quaternion.Euler(0, 0, 180);
            }
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                Move(1);
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }
        }
    }

    void Move(int direction)
    {
        int dx = 0;
        int dy = 0;

        switch (direction)
        {
            case 0: dy = 1; break;
            case 1: dx = 1; break;
            case 2: dy = -1; break;
            case 3: dx = -1; break;
        }

        int tilesX = x + dx;
        int tilesY = y + dy;

        if ((0 <= tilesX && tilesX < Level.instance.width) && (0 <= tilesY && tilesY < Level.instance.height))
        {
            Tile targetTile = Level.instance.tiles[tilesX, tilesY];
            Tile originTile = Level.instance.tiles[x, y];

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
                    this.direction = direction;
                    
                    handSegment.Remove(targetTile);
                    if (handSegment.Count < maxLength) UpdateMoves(movesLeft + 1);

                    UpdatePosition(tilesX, tilesY);
                    originTile.Deactivate(this);
                    
                    HandManager.instance.RebuildHand(this);
                }
            }
            // EXTEND
            else if (targetTile.type.walkable && movesLeft > 0)
            {
                this.direction = direction;
                
                handSegment.Add(originTile);
                UpdateMoves(movesLeft - 1);
                originTile.HandOn();

                UpdatePosition(tilesX, tilesY);

                targetTile.Activate(this);

                HandManager.instance.RebuildHand(this);
            }
        }
    }

    public void ForceMove(int length)
    {
        int dx = 0;
        int dy = 0;

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
            
                originTile.HandOn();

                UpdatePosition(x + dx, y + dy);

                targetTile.Activate(this);

                HandManager.instance.RebuildHand(this);
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

            HandManager.instance.RebuildHand(this);
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
