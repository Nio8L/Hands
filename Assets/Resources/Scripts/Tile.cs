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

    private void Start() {
        ChangeColor();
    }

    public virtual void ChangeColor()
    {
        spriteRenderer.color = type.color;
    }

    private void OnMouseDown() {
        type = LevelEditor.instance.GetTileType();
        ChangeColor();
    }

}
