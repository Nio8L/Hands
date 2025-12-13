using UnityEngine;

[CreateAssetMenu(menuName = "TileType/Default")]
public class TileType : ScriptableObject {
    public Color color;
    public bool walkable;

    public virtual void Effect(HandScript hand)
    {
        //Effect after being walked on.
    }

    public virtual void Uneffect(HandScript hand)
    {
        //Effect after being unwaled on.
    }
}
