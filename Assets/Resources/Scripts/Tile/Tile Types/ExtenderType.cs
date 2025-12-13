using Unity.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "TileType/Extender")]
public class ExtenderType : TileType
{
    public int length;
    public int direction;

    public override void Effect(HandScript hand)
    {
        Debug.Log("step on");
       
        hand.ForceMove(direction, length);
    }

    public override void Uneffect(HandScript hand)
    {
        Debug.Log("step off");
    }
}
