using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seek : SteeringBehaviour
{
    public Vector3 target;
    public float maxAcceleration;

    // Start is called before the first frame update
    void Start()
    {
        character = GetComponent<Kinematic>();
        character.position = transform.position;
        character.orientation = transform.rotation.eulerAngles.z * Mathf.Deg2Rad;
    }

    public override SteeringOutput getSteering(SteeringOutput input)
    {
        SteeringOutput output = input;
        output.linear = target;
        output.linear -= character.position;

        if (output.linear.sqrMagnitude > 0)
        {
            output.linear.Normalize();
            output.linear *= maxAcceleration;
        }

        return output;
    }
}
