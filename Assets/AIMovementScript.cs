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
    public float MAX_DISTANCE_BEFORE_FIRING;

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
#if DEBUG
        heading.x = Mathf.Cos(transform.rotation.eulerAngles.z * Mathf.Deg2Rad);
        heading.y = Mathf.Sin(transform.rotation.eulerAngles.z * Mathf.Deg2Rad);
        Vector3 headingLine = new Vector3(heading.x, heading.y, 0);
        Debug.DrawLine(transform.position, transform.position + (headingLine * 2), Color.red);
#endif
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
    }
}
