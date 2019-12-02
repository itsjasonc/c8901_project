using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundariesScript : MonoBehaviour
{
    private Vector2 screenBounds;
    private float objectWidth;
    private float objectHeight;

    // Start is called before the first frame update
    void Start()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        objectWidth = transform.GetComponent<SpriteRenderer>().bounds.size.x / 2;
        objectHeight = transform.GetComponent<SpriteRenderer>().bounds.size.y / 2;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 desiredPosition = transform.position;
        desiredPosition.x = Mathf.Clamp(transform.position.x, screenBounds.x + objectWidth, screenBounds.x * -1 - objectWidth);
        desiredPosition.y = Mathf.Clamp(transform.position.y, screenBounds.y + objectHeight, screenBounds.y * -1 + objectHeight * 3);
        transform.position = desiredPosition;
    }
}
