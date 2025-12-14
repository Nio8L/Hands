using Unity.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "TileType/Extender")]
public class ExtenderType : TileType
{
    public int length;

    public override void Effect(HandScript hand)
    {
        hand.ForceMove(length);
    }

    public override void Uneffect(HandScript hand) { }
}