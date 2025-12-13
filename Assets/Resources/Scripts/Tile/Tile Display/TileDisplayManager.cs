using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TileDisplayManager : MonoBehaviour
{
    public GameObject displayPrefab;

    private void Start() {
        List<TileType> tileTypes = Resources.LoadAll<TileType>("Tiles").ToList();

        for (int i = 0; i < tileTypes.Count; i++)
        {
            GameObject newDisplay = Instantiate(displayPrefab, new (i + i * 0.5f, transform.position.y, 0), Quaternion.identity);
            newDisplay.transform.parent = transform;
            newDisplay.GetComponent<TileDisplay>().SetType(tileTypes[i]);
        }
    }
}
