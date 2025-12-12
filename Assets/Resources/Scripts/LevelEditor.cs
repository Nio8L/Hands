using UnityEngine;

public class LevelEditor : MonoBehaviour
{
    public GameObject tilePrefab;
    public Tile[,] tiles;

    public int widht;
    public int height;
    
    private void OnValidate() {
        if (Application.isPlaying)
        {
            if (widht <= 0)
            {
                widht = 1;
            }

            if (height <= 0)
            {
                height = 1;
            }

            Generate();
        }
    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
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
            Debug.Log("changing to " + tiles[i, 0].type.color);
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
}
