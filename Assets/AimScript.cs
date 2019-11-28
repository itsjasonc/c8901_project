using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimScript : MonoBehaviour
{
    public GameObject projectile;
    public float timeLeftBeforeFiring = 50.0f;
    public float lastFired = 0.0f;
    public float PROJECTILE_SPEED = 10.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 screenPoint = Camera.main.WorldToScreenPoint(transform.position);
        Vector2 direction = (Vector2)(Input.mousePosition - screenPoint);
        direction.Normalize();
        Vector3 direction3D = new Vector3(direction.x, direction.y, 0);

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        if (lastFired > 0)
        {
            lastFired -= Time.deltaTime * 100;
        }

        if (lastFired <= 0 && (Input.GetAxisRaw("Fire1") > 0 || Input.GetAxisRaw("Fire1") < 0))
        {
            GameObject bullet = Instantiate(projectile, transform.position + (direction3D * 1.5f), transform.rotation);
            bullet.GetComponent<Rigidbody2D>().AddForce(direction * PROJECTILE_SPEED, ForceMode2D.Impulse);
            lastFired = timeLeftBeforeFiring;
        }
    }
}
