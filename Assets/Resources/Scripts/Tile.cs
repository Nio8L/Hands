using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class Tile : MonoBehaviour
{
    public TileType type;
    private SpriteRenderer spriteRenderer;

    private void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void ChangeColor()
    {
        spriteRenderer.color = type.color;
    }

    private void OnMouseDown() {
        TileType tileType = LevelEditor.instance.GetEditingType();
        
        SetType(tileType);
    }

    public void SetType(TileType tileType)
    {
        if(tileType == null) return;

        type = tileType;
        ChangeColor();
    }

    public void Default()
    {
        SetType(LevelEditor.instance.defaultType);
    }

}
