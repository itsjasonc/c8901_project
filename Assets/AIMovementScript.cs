using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMovementScript : MonoBehaviour
{
    public GameObject target;
    public Vector3 heading; // The current facing direction
    public KinematicSeek seekComponent;
    public KinematicFlee fleeComponent;
    public KinematicArrive arriveComponent;

    public GameObject projectile;
    public float timeLeftBeforeFiring = 50.0f;
    public float lastFired = 0.0f;
    public float PROJECTILE_SPEED = 10.0f;

    // Start is called before the first frame update
    void Start()
    {
        seekComponent = GetComponent<KinematicSeek>();
        fleeComponent = GetComponent<KinematicFlee>();
        arriveComponent = GetComponent<KinematicArrive>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        float duration = Time.deltaTime;
        SteeringOutput steer = new SteeringOutput();
        float angle = 0.0f;

        if (seekComponent != null)
        {
            seekComponent.target = target.transform.position;
            steer = seekComponent.getSteering(steer);

            seekComponent.character.integrate(steer, duration);
            seekComponent.character.setOrientationFromVelocity(steer.linear);
            transform.position = seekComponent.character.position;
            angle = seekComponent.character.orientation;
        }
        else if (fleeComponent != null)
        {
            steer = fleeComponent.getSteering(steer);
            fleeComponent.target = target.transform.position;

            fleeComponent.character.integrate(steer, duration);
            fleeComponent.character.setOrientationFromVelocity(steer.linear);
            transform.position = fleeComponent.character.position;
            angle = fleeComponent.character.orientation;
        }
        else if (arriveComponent != null)
        {
            arriveComponent.target = target.transform.position;
            steer = arriveComponent.getSteering(steer);

            arriveComponent.character.integrate(steer, duration);
            // arriveComponent.character.setOrientationFromVelocity(steer.linear);
            transform.position = arriveComponent.character.position;
            angle = arriveComponent.character.orientation;
        }

        angle *= Mathf.Rad2Deg;

        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

#if DEBUG
        heading.x = Mathf.Cos(transform.rotation.eulerAngles.z * Mathf.Deg2Rad);
        heading.y = Mathf.Sin(transform.rotation.eulerAngles.z * Mathf.Deg2Rad);
        Vector3 headingLine = new Vector3(heading.x, heading.y, 0);
        Debug.DrawLine(transform.position, transform.position + (headingLine * 2), Color.red);
#endif
        if (lastFired > 0)
        {
            lastFired -= Time.deltaTime * 100;
        }

        if (lastFired <= 0 && steer.linear.sqrMagnitude <= 0)
        {
            GameObject bullet = Instantiate(projectile, transform.position + (heading * 1.5f), transform.rotation);
            bullet.GetComponent<Rigidbody2D>().AddForce(heading * PROJECTILE_SPEED, ForceMode2D.Impulse);
            lastFired = timeLeftBeforeFiring;
        }
    }
}
