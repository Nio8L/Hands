using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class LevelAssembler : MonoBehaviour
{
    public GameObject levelPrefab;
    public GameObject handPrefab;

    public int startX1, startY1, startX2, startY2;

    private void Start() {
        Instantiate(levelPrefab).GetComponent<LevelEditor>();
        Level.instance.Load();
        
        HandScript hand = Instantiate(handPrefab).GetComponent<HandScript>();
        hand.UpdatePosition(startX1, startY1);
    }
}
