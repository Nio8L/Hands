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
    public Sprite sprite;

    public int x, y;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
            spriteRenderer = gameObject.AddComponent<SpriteRenderer>();

    }

    private void Start()
    {
        defaultType = Instantiate(type);
        type = Instantiate(type);
        ApplySprite(type.sprite);
    }


    private void OnMouseDown() {
        if (LevelEditor.instance == null) return;

        TileType tileType = LevelEditor.instance.GetEditingType();
        
        SetType(tileType);
    }

    public void SetType(TileType tileType)
    {
        if(tileType == null) return;

        type = tileType;
        spriteRenderer.sprite = tileType.sprite;
    }

    public void ApplySprite(Sprite newSprite)
    {
        if (newSprite == null) return;

        spriteRenderer.sprite = newSprite;

        Vector2 spriteSize = newSprite.rect.size;
        float scaleX = 128f / spriteSize.x;
        float scaleY = 128f / spriteSize.y;
        transform.localScale = new Vector3(scaleX, scaleY, 1f);
    }

    public void Default()
    {
        SetType(defaultType);
    }

     public void HandOn()
    {
         spriteRenderer.sprite = type.sprite;
    }

    public void Activate(HandScript hand)
    {
        type.Effect(hand);
    }

    public void Deactivate(HandScript hand)
    {
        if (hand.handSegment.Contains(this))
        {
            return;
        }

        type.Uneffect(hand);
    }

}
