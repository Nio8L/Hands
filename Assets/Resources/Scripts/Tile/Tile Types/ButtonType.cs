using UnityEngine;

[CreateAssetMenu(menuName = "TileType/Button")]
public class ButtonType : TileType
{
    public int targetX, targetY;
    public TileType openDoor;

    public override void Effect(HandScript hand)
    {
        Level.instance.tiles[targetX, targetY].SetType(openDoor);
        SoundManager.instance.Play("Press");
    }

    public override void Uneffect(HandScript hand)
    {
        Level.instance.tiles[targetX, targetY].Default();
        SoundManager.instance.Play("Press");
    }
}
