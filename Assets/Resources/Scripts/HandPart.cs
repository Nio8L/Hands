using System;
using System.Collections.Generic;
using System.Linq;
using Unity.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class HandPart : MonoBehaviour
{
    public Sprite handBeginning;
    public Sprite handPart;
    public Sprite handCurve;
    private SpriteRenderer spriteRenderer;

    public Sprite defaultPart;
    public Sprite part;

    public int x, y;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        defaultPart = Instantiate(handBeginning);
    }

    private void OnMouseDown()
    {
        PlaceHand(handPart);
    }

    public void PlaceHand(Sprite sprite)
    {
        if (sprite == null) return;

        part = sprite;
    }

    public void Default()
    {
        PlaceHand(defaultPart);
    }

}