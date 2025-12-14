using Unity.VisualScripting;
using UnityEngine;

public class Level : MonoBehaviour
{
    public static Level instance;
    public Tile[,] tiles;
    public GameObject tilePrefab;
    public int width, height;

    private void Awake() {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
    }

    public void Load()
    {
        tiles = new Tile[width, height];
        for (int i = 0; i < transform.childCount; i++)
        {
            int x = i / height;
            int y = i % height;
            tiles[x, y] = transform.GetChild(i).transform.GetComponent<Tile>();
        }

        HandManager.instance.Generate(width, height);
    }

    public void Generate(int width, int height)
    {
        tiles = new Tile[width, height];
        this.width = width;
        this.height = height;

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                GameObject newTile = Instantiate(tilePrefab, new(x + x * 0.2f, y + y * 0.2f), Quaternion.identity);
                newTile.transform.parent = transform;
                tiles[x, y] = newTile.GetComponent<Tile>();
                tiles[x, y].x = x;
                tiles[x, y].y = y;
            }
        }
    }
}
