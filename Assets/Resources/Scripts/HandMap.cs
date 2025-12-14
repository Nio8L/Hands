using UnityEngine;

public class HandMap : MonoBehaviour
{
    public static HandMap instance;
    public HandMap[,] handMap;
    public GameObject tilePrefab;
    public int width, height;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
    }

    public void Load()
    {
        handMap = new HandMap[width, height];
        for (int i = 0; i < transform.childCount; i++)
        {
            int x = i / height;
            int y = i % height;
            handMap[x, y] = transform.GetChild(i).transform.GetComponent<HandMap>();
        }
    }

    public void Generate(int width, int height)
    {
        handMap = new HandMap[width, height];
        this.width = width;
        this.height = height;

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                GameObject newPart = Instantiate(tilePrefab, new(x + x * 0.2f, y + y * 0.2f), Quaternion.identity);
                newPart.transform.parent = transform;
                handMap[x, y] = newPart.GetComponent<HandMap>();
                handMap[x, y].width = x;
                handMap[x, y].height = y;
            }
        }
    }
}
