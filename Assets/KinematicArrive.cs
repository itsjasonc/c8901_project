using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KinematicArrive : TargetedKinematicMovement
{
    // At each step the character tries to reach its target in
    // this duration. This means it moves more slowly when nearby
    public float timeToTarget;
    // If the character is closer than this to the target, it will
    // not attempt to move
    public float radius;

    // Start is called before the first frame update
    void Start()
    {
        character = GetComponent<Location>();
        character.position = transform.position;
        character.orientation = transform.rotation.eulerAngles.z * Mathf.Deg2Rad;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public override SteeringOutput getSteering(SteeringOutput input)
    {
        // get the direction
        SteeringOutput output = input;
        output.linear = target;
        output.linear -= character.position;

        // if there is no direction, do nothing
        if (output.linear.sqrMagnitude < radius * radius)
        {
            output.linear = Vector3.zero;
        }
        else
        {
            // we'd like to arrive in timeToTarget seconds
            output.linear *= ((float)1.0 / timeToTarget);

            // if that is too fast, then clip the speed
            if (output.linear.sqrMagnitude > maxSpeed * maxSpeed)
            {
                output.linear.Normalize();
                output.linear *= maxSpeed;
            }
        }

        return output;
    }
}
