using System;
using System.Collections.Generic;
using System.Linq;
using Unity.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class Tile : MonoBehaviour
{
    public TileType type;
    private SpriteRenderer spriteRenderer;

    public TileType defaultType;

    public int x, y;

    private void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start() {
        defaultType = Instantiate(type);
        type = Instantiate(type);
    }

    public void ChangeColor()
    {
        spriteRenderer.color = type.color;
    }

    private void OnMouseDown() {
        if (LevelEditor.instance == null) return;

        TileType tileType = LevelEditor.instance.GetEditingType();
        
        SetType(tileType);
    }

    public void SetType(TileType tileType)
    {
        if(tileType == null) return;

        type = Instantiate(tileType);
        ChangeColor();
    }

    public void Default()
    {
        SetType(defaultType);
    }

    public void HandOn(Color color)
    {
        spriteRenderer.color = color;
    }

    public void Activate(HandScript hand)
    {
        type.Effect(hand);
    }

    public void Deactivate(HandScript hand)
    {
        if (hand.handSegment.Contains(this))
        {
            Debug.Log("has hand segment");
            return;
        }

        type.Uneffect(hand);
    }

}
