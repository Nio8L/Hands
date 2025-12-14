using Unity.Collections;
using UnityEngine;

public class HandPart : MonoBehaviour
{
    SpriteRenderer spriteRenderer;

    public int x, y;
    public HandScript owner;

    private void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void ChangeSprite(Sprite sprite)
    {
        spriteRenderer.sprite = sprite;
    }

    public void Set(HandScript owner, Sprite sprite, Quaternion rotation)
    {
        this.owner = owner;
        if (x == owner.x && y == owner.y)
        {       
            spriteRenderer.sprite = null;
        }
        else
        { 
            spriteRenderer.sprite = sprite;
        }
        transform.rotation = rotation;
    }

    public void Clear(HandScript owner)
    {
        if (this.owner != owner) return;

        this.owner = null;
        spriteRenderer.sprite = null;
        transform.rotation = Quaternion.identity;
    }
}
