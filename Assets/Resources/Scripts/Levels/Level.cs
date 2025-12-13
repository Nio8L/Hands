using UnityEngine;

public class Level : MonoBehaviour
{
    public static Level instance;
    public Tile[,] tiles;
    public GameObject tilePrefab;

    private void Awake() {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
    }

    public void Generate(int width, int height)
    {
        tiles = new Tile[width, height];

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
