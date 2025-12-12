using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelEditor : MonoBehaviour
{
    public static LevelEditor instance;

    public GameObject tilePrefab;
    public Tile[,] tiles;

    public int widht;
    public int height;
    
    public int typeIndex;

    private void Awake() {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
    }
    
    void Start()
    {
        Generate();
    }

    void Generate()
    {
        Delete();

        for (int x = 0; x < widht; x++)
        {
            for (int y = 0; y < height; y++)
            {
                GameObject newTile = Instantiate(tilePrefab, new(x + x * 0.2f, y + y * 0.2f), Quaternion.identity);
                tiles[x, y] = newTile.GetComponent<Tile>();
            }
        }

        for (int i = 0; i < widht; i++)
        {
            TileType type = Resources.Load<TileType>("Tiles/Wall");
            tiles[i, 0].type = Instantiate(type);
            tiles[i, 0].ChangeColor();
        }
    }

    void Delete()
    {
        if (tiles == null)
        {
            tiles = new Tile[widht, height];
            return;
        }

        foreach (Tile tile in tiles)
        {
            Destroy(tile.gameObject);
        }
        tiles = new Tile[widht, height];

    }

    public TileType GetTileType()
    {
        List<TileType> allTiles = Resources.LoadAll<TileType>("Tiles").ToList();
        return allTiles[typeIndex];
    }
}
