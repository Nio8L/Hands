using System.Collections.Generic;
using System.Linq;
using Unity.Collections;
using UnityEngine;

public class LevelEditor : MonoBehaviour
{
    public static LevelEditor instance;

    public GameObject levelPrefab;
    Level level;

    public int width;
    public int height;

    TileType editingType;

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
        level = Instantiate(levelPrefab).GetComponent<Level>();
        level.Generate(width, height);
    }

    public void SetDisplayType(TileType type)
    {
        editingType = type;
    }

    public TileType GetEditingType()
    {
        return editingType;
    }
}
