using UnityEngine;

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
}
