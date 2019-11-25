using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KinematicFlee : KinematicSeek
{
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
        SteeringOutput output = input;
        output.linear = character.position;
        output.linear -= target;

        if (output.linear.sqrMagnitude > 0)
        {
            output.linear.Normalize();
            output.linear *= maxSpeed;
        }

        return output;
    }
}
