using Unity.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "TileType/Extender")]
public class ExtenderType : TileType
{
    public int length;

    public override void Effect(HandScript hand)
    {
        Debug.Log("step on extender");
        hand.ForceMove(length);
    }

    public override void Uneffect(HandScript hand) { }
}