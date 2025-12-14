using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class LevelAssembler : MonoBehaviour
{
    public GameObject levelPrefab;
    public GameObject handPrefab;

    public int startX1, startY1, startX2, startY2;
    public Color color1, color2;

    private void Start() {
        Instantiate(levelPrefab).GetComponent<LevelEditor>();
        Level.instance.Load();
        
        HandScript hand = Instantiate(handPrefab).GetComponent<HandScript>();
        hand.Setup(1);
        hand.UpdatePosition(startX1, startY1);

        HandScript hand2 = Instantiate(handPrefab).GetComponent<HandScript>();
        hand2.Setup(0);
        hand2.UpdatePosition(startX2, startY2);
    }
}
