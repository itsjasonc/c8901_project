using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    private Vector2 screenBounds;
    private float objectWidth;
    private float objectHeight;
    public float TIME_TO_DEATH = 2000.0f;
    public float timeLeft;
    private new BoxCollider2D collider;

    // Start is called before the first frame update
    void Start()
    {
        collider = GetComponent<BoxCollider2D>();
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        objectWidth = transform.GetComponent<SpriteRenderer>().bounds.size.x / 2;
        objectHeight = transform.GetComponent<SpriteRenderer>().bounds.size.y / 2;
        timeLeft = TIME_TO_DEATH;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 position = transform.position;

        if (position.x < screenBounds.x + objectWidth || position.x > screenBounds.x * -1 + objectWidth)
        {
            Object.Destroy(gameObject);
        }
        if (position.y < screenBounds.y + objectHeight || position.y > screenBounds.y * -1 + objectHeight * 3)
        {
            Object.Destroy(gameObject);
        }
    }

    private void FixedUpdate()
    {
        timeLeft -= Time.fixedDeltaTime * 100;
        if (timeLeft <= 0)
        {
            Object.Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.tag.Equals("Projectile"))
        {
            Object.Destroy(gameObject);
        }
        else
        {
            Physics2D.IgnoreCollision(collision.collider, collider);
        }
    }
}
