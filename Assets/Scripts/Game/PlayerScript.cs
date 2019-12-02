using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public int num_wins = 0;
    public int MAX_HEALTH = 10;
    public int health = 10;
    public int LOW_HEALTH = 3;
    public Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Projectile"))
        {
            health--;
            rb.velocity = Vector2.zero;
            GameObject.Find("GameManager").GetComponent<GameManagerScript>().UpdateHealth();
        }
        else if (collision.gameObject.tag.Equals("Health"))
        {
            health += 3;
            if (health > MAX_HEALTH)
            {
                health = MAX_HEALTH;
            }
            GameObject.Find("GameManager").GetComponent<GameManagerScript>().UpdateHealth();
        }
    }
}
