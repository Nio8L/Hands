using System.Collections.Generic;
using System.Linq;
using Unity.Collections;
using UnityEngine;

public class HandManager : MonoBehaviour
{
    public static HandManager instance;

    public GameObject partPrefab;
    HandScript hand1;
    HandScript hand2;

    HandPart[,] parts;

    public Sprite handStart;
    public Sprite handStraight;
    public Sprite handCorner;

    public Sprite handStart2;
    public Sprite handStraight2;
    public Sprite handCorner2;

    private void Awake() {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
    }

    public void SetHand(HandScript hand)
    {
        if (hand1 == null)
        {
            hand1 = hand;
        }
        else
        {
            hand2 = hand;
        }

    }

    public void Generate(int width, int height)
    {
        parts = new HandPart[width, height];
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                GameObject newPart = Instantiate(partPrefab, new(x, y), Quaternion.identity);
                parts[x, y] = newPart.GetComponent<HandPart>();
                parts[x, y].x = x;
                parts[x, y].y = y;
                parts[x, y].transform.parent = transform;
            }
        }
    }

    public void RebuildHand(HandScript hand)
    {
        // 1. Clear tiles owned by THIS hand only
        for (int x = 0; x < parts.GetLength(0); x++)
        {
            for (int y = 0; y < parts.GetLength(1); y++)
            {
                parts[x, y].Clear(hand);
            }
        }

        // 2. Build ordered path: body + head
        List<Vector2Int> path = new();

        foreach (Tile t in hand.handSegment)
            path.Add(new Vector2Int(t.x, t.y));

        path.Add(new Vector2Int(hand.x, hand.y));

        // 3. Draw tiles
        for (int i = 0; i < path.Count; i++)
        {
            Vector2Int curr = path[i];
            Vector2Int prev = i > 0 ? path[i - 1] : curr;
            Vector2Int next = i < path.Count - 1 ? path[i + 1] : curr;

            DrawSegment(hand, curr, prev, next);
        }
    }

    Vector2Int ToVec(Tile t) => new(t.x, t.y);
    Vector2Int ToVec(int x, int y) => new(x, y);

    void DrawSegment(HandScript hand, Vector2Int curr, Vector2Int prev, Vector2Int next)
    {
        HandPart part = parts[curr.x, curr.y];

        Vector2Int a = prev - curr;
        Vector2Int b = next - curr;

        if (curr == prev)
        {
            Vector2Int dir = next - curr;
            if (hand == hand1)
            {
                part.Set(hand, handStart, Quaternion.Euler(0, 0, RotationFromDir(dir)));
            }
            else
            {
                part.Set(hand, handStart2, Quaternion.Euler(0, 0, RotationFromDir(dir)));  
            }
            return;
        }

        if (a.x == -b.x || a.y == -b.y)
        {
            Quaternion rot = (a.x != 0) ? Quaternion.identity : Quaternion.Euler(0, 0, 90);

            if (hand == hand1)
            {       
                part.Set(hand, handStraight, rot);
            }
            else
            {
                part.Set(hand, handStraight2, rot);
            }
            return;
        }

        if (hand == hand1)
        {
            part.Set(hand, handCorner, Quaternion.Euler(0, 0, CornerFromVectors(a, b)));
        }
        else
        {
            part.Set(hand, handCorner2, Quaternion.Euler(0, 0, CornerFromVectors(a, b)));
        }
    }


    int RotationFromDir(Vector2Int dir)
    {
        if (dir == Vector2Int.up) return 90;
        if (dir == Vector2Int.right) return 0;
        if (dir == Vector2Int.down) return 270;
        if (dir == Vector2Int.left) return 180;

        return 0;
    }


    int CornerFromVectors(Vector2Int a, Vector2Int b)
    {
        if ((a == Vector2Int.right && b == Vector2Int.up) ||
            (b == Vector2Int.right && a == Vector2Int.up)) return 180;

        if ((a == Vector2Int.up && b == Vector2Int.left) ||
            (b == Vector2Int.up && a == Vector2Int.left)) return 270;

        if ((a == Vector2Int.left && b == Vector2Int.down) ||
            (b == Vector2Int.left && a == Vector2Int.down)) return 0;

        return 90;
    }


    public void CheckWin()
    {
        if (hand1.x == hand2.x && hand1.y == hand2.y)
        {
            GameObject.Find("Completed").transform.GetChild(0).gameObject.SetActive(true);
        }

        foreach (Tile t in hand1.handSegment)
        {
            if (t.x == hand2.x && t.y == hand2.y)
            {
                GameObject.Find("Completed").transform.GetChild(0).gameObject.SetActive(true);
            }
        }

        foreach (Tile t in hand2.handSegment)
        {
            if (t.x == hand1.x && t.y == hand1.y)
            {
               GameObject.Find("Completed").transform.GetChild(0).gameObject.SetActive(true);
            }
        }   
    }
}
