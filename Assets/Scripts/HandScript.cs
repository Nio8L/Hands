using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class HandScript : MonoBehaviour
{
    public float speed = 5f;
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            transform.rotation = Quaternion.Euler(0, 0, 90);
            transform.position = new Vector2(transform.position.x, transform.position.y + speed);

        }
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            transform.rotation = Quaternion.Euler(0, 0, 270);
            transform.position = new Vector2(transform.position.x, transform.position.y - speed);
        }
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            transform.rotation = Quaternion.Euler(0, 0, 180);
            transform.position = new Vector2(transform.position.x - speed, transform.position.y);
        }
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
            transform.position = new Vector2(transform.position.x + speed, transform.position.y);
        }
    }
}
