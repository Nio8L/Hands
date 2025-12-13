using Unity.VisualScripting;
using UnityEngine;

public class TileDisplay : MonoBehaviour
{
    public TileType displayedType;

    public void SetType(TileType type)
    {
        displayedType = type;
        GetComponent<SpriteRenderer>().color = displayedType.color;
    }

    private void OnMouseDown() {
        LevelEditor.instance.SetDisplayType(displayedType);
    }

}
