using Unity.VisualScripting;
using UnityEngine;

public class TileDisplay : MonoBehaviour
{
    public TileType displayedType;

    public void SetType(TileType type)
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (sr != null && displayedType != null)
        {
            sr.sprite = displayedType.sprite;
        }
    }

    private void OnMouseDown() {
        LevelEditor.instance.SetDisplayType(displayedType);
    }

}
