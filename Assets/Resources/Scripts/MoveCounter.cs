using TMPro;
using UnityEngine;

public class MoveCounter : MonoBehaviour
{
    public TextMeshProUGUI counter;

    private void Awake() {
        counter = GetComponent<TextMeshProUGUI>();
    }

    public void UpdateCounter(int movesLeft)
    {
        counter.text = "Moves left: " + movesLeft;
    }
}
