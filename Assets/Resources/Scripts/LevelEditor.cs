using System.Collections.Generic;
using System.Linq;
using Unity.Collections;
using UnityEngine;

public class LevelEditor : MonoBehaviour, InterfaceDataPersistance
{
    public static LevelEditor instance;

    public GameObject tilePrefab;
    public Tile[,] tiles;

    public int width;
    public int height;

    TileType editingType;
    public TileType defaultType;

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

        for (int i = 0; i < width; i++)
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
            tiles = new Tile[width, height];
            return;
        }

        foreach (Tile tile in tiles)
        {
            Destroy(tile.gameObject);
        }
        tiles = new Tile[width, height];

    }

    public void SetDisplayType(TileType type)
    {
        editingType = type;
    }

    public TileType GetEditingType()
    {
        return editingType;
    }

    public void loadData(GameData data)
    {
        throw new System.NotImplementedException();
    }

    public void saveData(ref GameData data)
    {
        throw new System.NotImplementedException();
    }
}
